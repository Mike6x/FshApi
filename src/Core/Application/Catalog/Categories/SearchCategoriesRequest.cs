namespace FSH.WebApi.Application.Catalog.Categories;

public class SearchCategoriesRequest : PaginationFilter, IRequest<PaginationResponse<CategorieDto>>
{
    public DefaultIdType? GroupCategorieId { get; set; }
    public CatalogType? Type { get; set; }
}

public class SearchCategoriesRequestHandler(IReadRepository<Categorie> repository)
    : IRequestHandler<SearchCategoriesRequest, PaginationResponse<CategorieDto>>
{
    private readonly IReadRepository<Categorie> _repository = repository;

    public async Task<PaginationResponse<CategorieDto>> Handle(SearchCategoriesRequest request, CancellationToken cancellationToken)
    {
        var spec = new SearchCategoriesSpecification(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}

public class SearchCategoriesSpecification : EntitiesByPaginationFilterSpec<Categorie, CategorieDto>
{
    public SearchCategoriesSpecification(SearchCategoriesRequest request)
        : base(request) =>
            Query
                .Include(e => e.GroupCategorie)
                    .Where(e => e.GroupCategorieId.Equals(request.GroupCategorieId!.Value), request.GroupCategorieId.HasValue)
                    .Where(e => e.Type.Equals(request.Type!.Value), request.Type.HasValue)

                        // .Where(e => e.Type.Equals(request.Type!.Value) || e.Type.Equals(CatalogType.General), request.Type.HasValue)
                        .OrderBy(e => e.Order, !request.HasOrderBy());
}