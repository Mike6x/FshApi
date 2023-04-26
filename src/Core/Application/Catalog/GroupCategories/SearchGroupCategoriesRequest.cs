namespace FSH.WebApi.Application.Catalog.GroupCategories;

public class SearchGroupCategoriesRequest : PaginationFilter, IRequest<PaginationResponse<GroupCategorieDto>>
{
    public DefaultIdType? BusinessLineId { get; set; }
    public CatalogType? Type { get; set; }
}

public class SearchGroupCategoriesRequestHandler : IRequestHandler<SearchGroupCategoriesRequest, PaginationResponse<GroupCategorieDto>>
{
    private readonly IReadRepository<GroupCategorie> _repository;

    public SearchGroupCategoriesRequestHandler(IReadRepository<GroupCategorie> repository) => _repository = repository;

    public async Task<PaginationResponse<GroupCategorieDto>> Handle(SearchGroupCategoriesRequest request, CancellationToken cancellationToken)
    {
        var spec = new SearchGroupCategoriesSpecification(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}

public class SearchGroupCategoriesSpecification : EntitiesByPaginationFilterSpec<GroupCategorie, GroupCategorieDto>
{
    public SearchGroupCategoriesSpecification(SearchGroupCategoriesRequest request)
        : base(request) =>
            Query
                .Include(e => e.BusinessLine)
                .OrderBy(e => e.Order, !request.HasOrderBy())
                .Where(e => e.BusinessLineId.Equals(request.BusinessLineId!.Value), request.BusinessLineId.HasValue)
                .Where(e => e.Type.Equals(request.Type!.Value) || e.Type.Equals(CatalogType.General), request.Type.HasValue);
}