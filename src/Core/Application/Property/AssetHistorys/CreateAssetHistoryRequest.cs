using FSH.WebApi.Domain.Property;

namespace FSH.WebApi.Application.Property.AssetHistorys;

public class CreateAssetHistoryRequest : IRequest<DefaultIdType>
{
    public DefaultIdType AssetId { get; set; }

    public DefaultIdType? PreviousQualityStatusId { get; set; }
    public DefaultIdType QualityStatusId { get; set; }

    public DefaultIdType? PreviousUsingStatusId { get; set; }
    public DefaultIdType UsingStatusId { get; set; }

    public DefaultIdType? EmployeeId { get; set; }

    public string? Note { get; set; }
    public string? DoccumentLink { get; set; }

    public FileUploadRequest? Doccument { get; set; }

}

public class CreateAssetHistoryRequestHandler : IRequestHandler<CreateAssetHistoryRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<AssetHistory> _repository;
    private readonly IFileStorageService _file;

    public CreateAssetHistoryRequestHandler(IRepositoryWithEvents<AssetHistory> repository, IFileStorageService file) =>
        (_repository, _file) = (repository, file);

    public async Task<DefaultIdType> Handle(CreateAssetHistoryRequest request, CancellationToken cancellationToken)
    {
        string doccumentPath = await _file.UploadAsync<AssetHistory>(request.Doccument, FileType.Doccument, cancellationToken);

        var entity = new AssetHistory(
            request.AssetId,
            request.PreviousQualityStatusId,
            request.QualityStatusId,
            request.PreviousUsingStatusId,
            request.UsingStatusId,
            request.EmployeeId,
            doccumentPath,
            request.DoccumentLink,
            request.Note);

        await _repository.AddAsync(entity, cancellationToken);

        return entity.Id;
    }
}
