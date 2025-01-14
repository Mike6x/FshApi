using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Production;

namespace FSH.WebApi.Application.Production.Products;

public class UpdateProductRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }

    public int? Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool? IsActive { get; set; }

    public decimal? ListPrice { get; set; }

    public decimal? Weight { get; set; }
    public decimal? Length { get; set; }
    public decimal? Width { get; set; }
    public decimal? Height { get; set; }

    public DefaultIdType BrandId { get; set; }
    public DefaultIdType CategorieId { get; set; }
    public DefaultIdType SubCategorieId { get; set; }
    public DefaultIdType VendorId { get; set; }

    public bool DeleteCurrentImage { get; set; }
    public FileUploadRequest? Image { get; set; }
}

public class UpdateProductRequestHandler : IRequestHandler<UpdateProductRequest, DefaultIdType>
{
    private readonly IRepository<Product> _repository;
    private readonly IStringLocalizer _t;
    private readonly IFileStorageService _file;

    public UpdateProductRequestHandler(IRepository<Product> repository, IStringLocalizer<UpdateProductRequestHandler> localizer, IFileStorageService file) =>
        (_repository, _t, _file) = (repository, localizer, file);

    public async Task<DefaultIdType> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = product ?? throw new NotFoundException(_t["Product {0} Not Found.", request.Id]);

        // Remove old image if flag is set
        if (request.DeleteCurrentImage)
        {
            string? currentProductImagePath = product.ImagePath;
            if (!string.IsNullOrEmpty(currentProductImagePath))
            {
                string root = Directory.GetCurrentDirectory();
                _file.Remove(Path.Combine(root, currentProductImagePath));
            }

            product = product.ClearImagePath();
        }

        string? productImagePath = request.Image is not null
            ? await _file.UploadAsync<Product>(request.Image, FileType.Image, cancellationToken)
            : null;

        var updatedProduct = product.Update(
            request.Order,
            request.Code,
            request.Name,
            request.Description,
            request.IsActive,
            request.ListPrice,
            productImagePath,
            request.Weight,
            request.Length,
            request.Width,
            request.Height,
            request.BrandId,
            request.CategorieId,
            request.SubCategorieId,
            request.VendorId);

        // Add Domain Events to be raised after the commit
        product.DomainEvents.Add(EntityUpdatedEvent.WithEntity(product));

        await _repository.UpdateAsync(updatedProduct, cancellationToken);

        return request.Id;
    }
}