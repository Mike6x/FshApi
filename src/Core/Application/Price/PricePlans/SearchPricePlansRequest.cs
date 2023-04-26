using FSH.WebApi.Domain.Price;

namespace FSH.WebApi.Application.Price.PricePlans;

public class SearchPricePlansRequest : PaginationFilter, IRequest<PaginationResponse<PricePlanDto>>
{
    public Guid? PriceGroupId { get; set; }
}

public class SearchPricePlansRequestHandler : IRequestHandler<SearchPricePlansRequest, PaginationResponse<PricePlanDto>>
{
    private readonly IReadRepository<PricePlan> _repository;

    public SearchPricePlansRequestHandler(IReadRepository<PricePlan> repository) => _repository = repository;

    public async Task<PaginationResponse<PricePlanDto>> Handle(SearchPricePlansRequest request, CancellationToken cancellationToken)
    {
        var spec = new SearchPricePlansSpecification(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}

public class SearchPricePlansSpecification : EntitiesByPaginationFilterSpec<PricePlan, PricePlanDto>
{
    public SearchPricePlansSpecification(SearchPricePlansRequest request)
        : base(request) =>
            Query
                .Include(e => e.PriceGroup)
                .Where(e => e.PriceGroupId.Equals(request.PriceGroupId!.Value), request.PriceGroupId.HasValue)
                .OrderBy(e => e.Order, !request.HasOrderBy());
}
