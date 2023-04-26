using FSH.WebApi.Domain.Property;

namespace FSH.WebApi.Application.Property.Assets;

public class GetAssetRequest : IRequest<AssetDetailsDto>
{
    public DefaultIdType Id { get; set; }
    public GetAssetRequest(DefaultIdType id) => Id = id;
}

public class GetAssetRequestHandler : IRequestHandler<GetAssetRequest, AssetDetailsDto>
{
    private readonly IRepository<Asset> _repository;
    private readonly IStringLocalizer _t;

    public GetAssetRequestHandler(IRepository<Asset> repository, IStringLocalizer<GetAssetRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<AssetDetailsDto> Handle(GetAssetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<Asset, AssetDetailsDto>)new AssetByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);
}