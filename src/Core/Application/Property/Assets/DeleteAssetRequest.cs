using FSH.WebApi.Domain.Property;

namespace FSH.WebApi.Application.Property.Assets;

public class DeleteAssetRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }

    public DeleteAssetRequest(DefaultIdType id) => Id = id;
}

public class DeleteAssetRequestHandler : IRequestHandler<DeleteAssetRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Asset> _repository;
    private readonly IStringLocalizer _t;

    public DeleteAssetRequestHandler(IRepositoryWithEvents<Asset> repository, IStringLocalizer<DeleteAssetRequestHandler> localizer)
            => (_repository, _t) = (repository, localizer);

    public async Task<DefaultIdType> Handle(DeleteAssetRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity ?? throw new NotFoundException(_t["Asset {0} Not Found."]);

        await _repository.DeleteAsync(entity, cancellationToken);

        return request.Id;
    }
}
