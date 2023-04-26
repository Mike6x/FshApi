using FSH.WebApi.Domain.Property;
using System.Runtime.CompilerServices;

namespace FSH.WebApi.Application.Property.Assets;

public class UpdateAssetRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }

    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }

    public string? Model { get; set; }
    public string? Serial { get; set; }
    public string? Barcode { get; set; }

    public DateTime? DateOfPurchase { get; set; }
    public DateTime? DateOfManufacture { get; set; }

    // public DateTime? YearOfValuation { get; set; }
    public int WarrantyInMonth { get; set; }
    public int DepreciationInMonth { get; set; }

    public int Quantity { get; set; } = 1;
    public double UnitPrice { get; set; } = 0;
    public DefaultIdType VendorId { get; set; }

    public DefaultIdType CategorieId { get; set; }
    public DefaultIdType SubCategorieId { get; set; }

    public DefaultIdType QualityStatusId { get; set; }
    public string? Location { get; set; }
    public DefaultIdType EmployeeId { get; set; }
    public DefaultIdType UsingStatusId { get; set; }

    public bool DeleteCurrentImage { get; set; }
    public FileUploadRequest? Image { get; set; }

    // History infomation

    public string? DoccumentLink { get; set; }
    public string? Note { get; set; }

    public bool DeleteCurrentDoccument { get; set; }
    public FileUploadRequest? Doccument { get; set; }
}

public class UpdateAssetRequestHandler : IRequestHandler<UpdateAssetRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Asset> _repository;
    private readonly IRepositoryWithEvents<AssetHistory> _historyRepo;
    private readonly IStringLocalizer _t;
    private readonly IFileStorageService _file;

    public UpdateAssetRequestHandler(IRepositoryWithEvents<Asset> repository, IRepositoryWithEvents<AssetHistory> historyRepo, IStringLocalizer<UpdateAssetRequestHandler> localizer, IFileStorageService file) =>
          (_repository, _historyRepo, _t, _file) = (repository, historyRepo, localizer, file);

    public async Task<DefaultIdType> Handle(UpdateAssetRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);

        // Create History infomation

        string doccumentPath = await _file.UploadAsync<AssetHistory>(request.Doccument, FileType.Doccument, cancellationToken);

        bool isNotCreateHistory =
            (request.QualityStatusId == entity.QualityStatusId) &&
            (request.UsingStatusId == entity.UsingStatusId) &&
            (request.EmployeeId == (entity.EmployeeId ?? DefaultIdType.Empty) ) &&
            string.IsNullOrEmpty(doccumentPath) &&
            string.IsNullOrEmpty(request.DoccumentLink) &&
            string.IsNullOrEmpty(request.Note);

        // Remove old image if flag is set
        if (request.DeleteCurrentImage)
        {
            string? currentImagePath = entity.ImagePath;
            if (!string.IsNullOrEmpty(currentImagePath))
            {
                string root = Directory.GetCurrentDirectory();
                _file.Remove(Path.Combine(root, currentImagePath));
            }

            entity = entity.ClearImagePath();
        }

        string? imagePath = request.Image is not null
            ? await _file.UploadAsync<Asset>(request.Image, FileType.Image, cancellationToken)
            : null;

        entity.Update(
            request.Code,
            request.Name,
            request.Description,
            request.Model,
            request.Serial,
            imagePath,
            request.Barcode,
            request.DateOfPurchase,
            request.DateOfManufacture,
            request.WarrantyInMonth,
            request.DepreciationInMonth,
            request.Quantity,
            request.UnitPrice,
            request.VendorId,
            request.CategorieId,
            request.SubCategorieId,
            request.QualityStatusId,
            request.Location,
            request.EmployeeId,
            request.UsingStatusId);

        await _repository.UpdateAsync(entity, cancellationToken);

        if(!isNotCreateHistory)
        {
            var historyEntity = new AssetHistory(
                request.Id,
                entity.QualityStatusId,
                request.QualityStatusId,
                entity.UsingStatusId,
                request.UsingStatusId,
                request.EmployeeId,
                doccumentPath,
                request.DoccumentLink,
                request.Note);

            await _historyRepo.AddAsync(historyEntity, cancellationToken);
        }

        return request.Id;
    }
}