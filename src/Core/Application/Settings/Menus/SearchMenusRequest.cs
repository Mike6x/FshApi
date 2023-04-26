using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Application.Settings.Menus;

public class SearchMenusRequest : PaginationFilter, IRequest<PaginationResponse<MenuDto>>
{
}

public class SearchMenusRequestHandler : IRequestHandler<SearchMenusRequest, PaginationResponse<MenuDto>>
{
    private readonly IReadRepository<Menu> _repository;

    public SearchMenusRequestHandler(IReadRepository<Menu> repository) => _repository = repository;

    public async Task<PaginationResponse<MenuDto>> Handle(SearchMenusRequest request, CancellationToken cancellationToken)
    {
        var spec = new SearchMenusRequestSpecification(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}

public class SearchMenusRequestSpecification : EntitiesByPaginationFilterSpec<Menu, MenuDto>
{
    public SearchMenusRequestSpecification(SearchMenusRequest request)
        : base(request) =>
                Query.OrderBy(c => c.Order, !request.HasOrderBy());
}
