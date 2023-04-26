using FSH.WebApi.Domain.Property;

namespace FSH.WebApi.Application.Property.AssetHistorys;

public class UpdateAssetHistoryRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }

    public DefaultIdType AssetId { get; set; }

    public DefaultIdType PreviousQualityStatusId { get; set; }
    public DefaultIdType QualityStatusId { get; set; }

    public DefaultIdType PreviousUsingStatusId { get; set; }
    public DefaultIdType UsingStatusId { get; set; }

    public DefaultIdType EmployeeId { get; set; }

    public string? DoccumentLink { get; set; }
    public string? Note { get; set; }

    public bool DeleteCurrentDoccument { get; set; }
    public FileUploadRequest? Doccument { get; set; }
}

public class UpdateAssetHistoryRequestHandler : IRequestHandler<UpdateAssetHistoryRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<AssetHistory> _repository;
    private readonly IStringLocalizer _t;
    private readonly IFileStorageService _file;

    public UpdateAssetHistoryRequestHandler(IRepositoryWithEvents<AssetHistory> repository, IStringLocalizer<UpdateAssetHistoryRequestHandler> localizer, IFileStorageService file) =>
          (_repository, _t, _file) = (repository, localizer, file);

    public async Task<DefaultIdType> Handle(UpdateAssetHistoryRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);

        // Remove old Doccument if flag is set
        if (request.DeleteCurrentDoccument)
        {
            string? currentDoccumentPath = entity.DoccumentPath;
            if (!string.IsNullOrEmpty(currentDoccumentPath))
            {
                string root = Directory.GetCurrentDirectory();
                _file.Remove(Path.Combine(root, currentDoccumentPath));
            }

            entity = entity.ClearDoccumentPath();
        }

        string? doccumentPath = request.Doccument is not null
            ? await _file.UploadAsync<AssetHistory>(request.Doccument, FileType.Doccument, cancellationToken)
            : null;

        entity.Update(
            request.AssetId,
            request.PreviousQualityStatusId,
            request.QualityStatusId,
            request.PreviousUsingStatusId,
            request.UsingStatusId,
            request.EmployeeId,
            doccumentPath,
            request.DoccumentLink,
            request.Note);

        await _repository.UpdateAsync(entity, cancellationToken);

        return request.Id;
    }
}