using FSH.WebApi.Domain.Property;

namespace FSH.WebApi.Application.Property.AssetHistorys;

public class GetAssetHistoryRequest : IRequest<AssetHistoryDetailsDto>
{
    public DefaultIdType Id { get; set; }
    public GetAssetHistoryRequest(DefaultIdType id) => Id = id;
}

public class GetAssetHistoryRequestHandler : IRequestHandler<GetAssetHistoryRequest, AssetHistoryDetailsDto>
{
    private readonly IRepository<AssetHistory> _repository;
    private readonly IStringLocalizer _t;

    public GetAssetHistoryRequestHandler(IRepository<AssetHistory> repository, IStringLocalizer<GetAssetHistoryRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<AssetHistoryDetailsDto> Handle(GetAssetHistoryRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<AssetHistory, AssetHistoryDetailsDto>)new AssetHistoryByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);
}