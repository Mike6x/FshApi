using FSH.WebApi.Domain.Price;

namespace FSH.WebApi.Application.Price.PricePlans;

public class GetPricePlanRequest : IRequest<PricePlanDetailsDto>
{
    public Guid Id { get; set; }
    public GetPricePlanRequest(Guid id) => Id = id;
}

public class GetPricePlanRequestHandler : IRequestHandler<GetPricePlanRequest, PricePlanDetailsDto>
{
    private readonly IRepository<PricePlan> _repository;
    private readonly IStringLocalizer _t;

    public GetPricePlanRequestHandler(IRepository<PricePlan> repository, IStringLocalizer<GetPricePlanRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<PricePlanDetailsDto> Handle(GetPricePlanRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<PricePlan, PricePlanDetailsDto>)new PricePlanByIdWithPriceGroupSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);
}