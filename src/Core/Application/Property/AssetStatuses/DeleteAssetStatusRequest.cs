using FSH.WebApi.Application.Property.Assets;
using FSH.WebApi.Domain.Property;

namespace FSH.WebApi.Application.Property.AssetStatuses;

public class DeleteAssetStatusRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }

    public DeleteAssetStatusRequest(DefaultIdType id) => Id = id;
}

public class DeleteAssetStatusRequestHandler : IRequestHandler<DeleteAssetStatusRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<AssetStatus> _repository;
    private readonly IReadRepository<Asset> _childRepository;
    private readonly IStringLocalizer _t;

    public DeleteAssetStatusRequestHandler(IRepositoryWithEvents<AssetStatus> repository, IReadRepository<Asset> childRepository, IStringLocalizer<DeleteAssetStatusRequestHandler> localizer)
        => (_repository, _childRepository, _t) = (repository, childRepository, localizer);

    public async Task<DefaultIdType> Handle(DeleteAssetStatusRequest request, CancellationToken cancellationToken)
    {
        if (await _childRepository.AnyAsync(new AssetsByAssetStatusSpec(request.Id), cancellationToken))
        {
            throw new ConflictException(_t["AssetStatus cannot be deleted as it's being used."]);
        }

        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity ?? throw new NotFoundException(_t["AssetStatus {0} Not Found."]);

        await _repository.DeleteAsync(entity, cancellationToken);

        return request.Id;
    }
}
