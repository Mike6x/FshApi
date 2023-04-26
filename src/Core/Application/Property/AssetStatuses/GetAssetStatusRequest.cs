using FSH.WebApi.Domain.Property;

namespace FSH.WebApi.Application.Property.AssetStatuses;

public class GetAssetStatusRequest : IRequest<AssetStatusDetailsDto>
{
    public DefaultIdType Id { get; set; }
    public GetAssetStatusRequest(DefaultIdType id) => Id = id;
}

public class GetAssetStatusRequestHandler : IRequestHandler<GetAssetStatusRequest, AssetStatusDetailsDto>
{
    private readonly IRepository<AssetStatus> _repository;
    private readonly IStringLocalizer _t;

    public GetAssetStatusRequestHandler(IRepository<AssetStatus> repository, IStringLocalizer<GetAssetStatusRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<AssetStatusDetailsDto> Handle(GetAssetStatusRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<AssetStatus, AssetStatusDetailsDto>)new AssetStatusByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);
}