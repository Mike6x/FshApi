using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Production;

namespace FSH.WebApi.Application.Production.Products;

public class CreateProductRequest : IRequest<DefaultIdType>
{
    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }

    public decimal ListPrice { get; set; }
    public string? ImagePath { get; set; }

    public decimal Weight { get; set; }
    public decimal Length { get; set; }
    public decimal Width { get; set; }
    public decimal Height { get; set; }

    public DefaultIdType BrandId { get; set; }
    public DefaultIdType CategorieId { get; set; }
    public DefaultIdType? SubCategorieId { get; set; }
    public DefaultIdType? VendorId { get; set; }

    public FileUploadRequest? Image { get; set; }
}

public class CreateProductRequestHandler : IRequestHandler<CreateProductRequest, DefaultIdType>
{
    private readonly IRepository<Product> _repository;
    private readonly IFileStorageService _file;

    public CreateProductRequestHandler(IRepository<Product> repository, IFileStorageService file) =>
        (_repository, _file) = (repository, file);

    public async Task<DefaultIdType> Handle(CreateProductRequest request, CancellationToken cancellationToken)
    {
        string productImagePath = await _file.UploadAsync<Product>(request.Image, FileType.Image, cancellationToken);

        var product = new Product(
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
        product.DomainEvents.Add(EntityCreatedEvent.WithEntity(product));

        await _repository.AddAsync(product, cancellationToken);

        return product.Id;
    }
}