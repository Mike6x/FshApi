using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Application.Settings.Dimensions;

public class GetDimensionRequest(DefaultIdType id) : IRequest<DimensionDetailsDto>
{
    public DefaultIdType Id { get; set; } = id;
}

public class GetDimensionRequestHandler : IRequestHandler<GetDimensionRequest, DimensionDetailsDto>
{
    private readonly IRepository<Dimension> _repository;
    private readonly IStringLocalizer _t;

    public GetDimensionRequestHandler(IRepository<Dimension> repository, IStringLocalizer<GetDimensionRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<DimensionDetailsDto> Handle(GetDimensionRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<Dimension, DimensionDetailsDto>)new DimensionByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);
}