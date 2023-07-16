namespace FSH.WebApi.Application.Catalog.SubCategories;

public class SearchSubCategoriesRequest : PaginationFilter, IRequest<PaginationResponse<SubCategorieDto>>
{
    public DefaultIdType? CategorieId { get; set; }
    public CatalogType? Type { get; set; }
}

public class SearchSubCategoriesRequestHandler : IRequestHandler<SearchSubCategoriesRequest, PaginationResponse<SubCategorieDto>>
{
    private readonly IReadRepository<SubCategorie> _repository;

    public SearchSubCategoriesRequestHandler(IReadRepository<SubCategorie> repository) => _repository = repository;

    public async Task<PaginationResponse<SubCategorieDto>> Handle(SearchSubCategoriesRequest request, CancellationToken cancellationToken)
    {
        var spec = new SearchSubCategoriesSpecification(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}

public class SearchSubCategoriesSpecification : EntitiesByPaginationFilterSpec<SubCategorie, SubCategorieDto>
{
    public SearchSubCategoriesSpecification(SearchSubCategoriesRequest request)
        : base(request) =>
            Query
                .Include(e => e.Categorie)
                .Where(e => e.CategorieId.Equals(request.CategorieId!.Value), request.CategorieId.HasValue)
                .Where(e => e.Type.Equals(request.Type!.Value), request.Type.HasValue)
                    .OrderBy(e => e.Order, !request.HasOrderBy());
}