using FSH.WebApi.Domain.Property;

namespace FSH.WebApi.Application.Property.AssetHistorys;

public class DeleteAssetHistoryRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }

    public DeleteAssetHistoryRequest(DefaultIdType id) => Id = id;
}

public class DeleteAssetHistoryRequestHandler : IRequestHandler<DeleteAssetHistoryRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<AssetHistory> _repository;
    private readonly IStringLocalizer _t;

    public DeleteAssetHistoryRequestHandler(IRepositoryWithEvents<AssetHistory> repository, IStringLocalizer<DeleteAssetHistoryRequestHandler> localizer)
            => (_repository, _t) = (repository, localizer);

    public async Task<DefaultIdType> Handle(DeleteAssetHistoryRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity ?? throw new NotFoundException(_t["AssetHistory {0} Not Found."]);

        await _repository.DeleteAsync(entity, cancellationToken);

        return request.Id;
    }
}
