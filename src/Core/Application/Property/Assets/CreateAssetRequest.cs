using FSH.WebApi.Domain.Property;

namespace FSH.WebApi.Application.Property.Assets;

public class CreateAssetRequest : IRequest<DefaultIdType>
{
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }

    public string? Model { get; set; }
    public string? Serial { get; set; }

    public FileUploadRequest? Image { get; set; }
    public string? Barcode { get; set; }

    public DateTime DateOfPurchase { get; set; }
    public DateTime? DateOfManufacture { get; set; }

    // public DateTime? YearOfValuation { get; set; }
    public int WarrantyInMonth { get; set; }
    public int DepreciationInMonth { get; set; }

    public int Quantity { get; set; }
    public double UnitPrice { get; set; }
    public DefaultIdType VendorId { get; set; }

    public DefaultIdType CategorieId { get; set; }
    public DefaultIdType? SubCategorieId { get; set; }

    public DefaultIdType QualityStatusId { get; set; }
    public string? Location { get; set; }
    public DefaultIdType? EmployeeId { get; set; }
    public DefaultIdType UsingStatusId { get; set; }

    // History infomation

    public string? Note { get; set; }
    public string? DoccumentLink { get; set; }
    public FileUploadRequest? Doccument { get; set; }
}

public class CreateAssetRequestHandler : IRequestHandler<CreateAssetRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Asset> _repository;
    private readonly IRepositoryWithEvents<AssetHistory> _historyRepo;
    private readonly IFileStorageService _file;

    public CreateAssetRequestHandler(IRepositoryWithEvents<Asset> repository, IRepositoryWithEvents<AssetHistory> historyRepo, IFileStorageService file) =>
        (_repository, _historyRepo, _file) = (repository, historyRepo, file);

    public async Task<DefaultIdType> Handle(CreateAssetRequest request, CancellationToken cancellationToken)
    {
        string imagePath = await _file.UploadAsync<Asset>(request.Image, FileType.Image, cancellationToken);

        // Create History infomation
        string doccumentPath = await _file.UploadAsync<AssetHistory>(request.Doccument, FileType.Doccument, cancellationToken);

        var entity = new Asset(
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

        await _repository.AddAsync(entity, cancellationToken);

        var historyEntity = new AssetHistory(
            entity.Id,
            null,
            request.QualityStatusId,
            null,
            request.UsingStatusId,
            request.EmployeeId,
            doccumentPath,
            request.DoccumentLink,
            request.Note);

        await _historyRepo.AddAsync(historyEntity, cancellationToken);

        return entity.Id;
    }
}
