using FSH.WebApi.Domain.Price;

namespace FSH.WebApi.Application.Price.PriceGroups;

public class SearchPriceGroupsRequest : PaginationFilter, IRequest<PaginationResponse<PriceGroupDto>>
{
}

public class SearchPriceGroupsRequestHandler : IRequestHandler<SearchPriceGroupsRequest, PaginationResponse<PriceGroupDto>>
{
    private readonly IReadRepository<PriceGroup> _repository;

    public SearchPriceGroupsRequestHandler(IReadRepository<PriceGroup> repository) => _repository = repository;

    public async Task<PaginationResponse<PriceGroupDto>> Handle(SearchPriceGroupsRequest request, CancellationToken cancellationToken)
    {
        var spec = new SearchPriceGroupsSpecification(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}

public class SearchPriceGroupsSpecification : EntitiesByPaginationFilterSpec<PriceGroup, PriceGroupDto>
{
    public SearchPriceGroupsSpecification(SearchPriceGroupsRequest request)
        : base(request) =>
            Query
                .OrderBy(e => e.Order, !request.HasOrderBy());
}
