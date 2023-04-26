using FSH.WebApi.Domain.Property;

namespace FSH.WebApi.Application.Property.Assets;

public class SearchAssetsRequest : PaginationFilter, IRequest<PaginationResponse<AssetDto>>
{
    public DefaultIdType? CategorieId { get; set; }
}

public class SearchAssetsRequestHandler : IRequestHandler<SearchAssetsRequest, PaginationResponse<AssetDto>>
{
    private readonly IReadRepository<Asset> _repository;

    public SearchAssetsRequestHandler(IReadRepository<Asset> repository) => _repository = repository;

    public async Task<PaginationResponse<AssetDto>> Handle(SearchAssetsRequest request, CancellationToken cancellationToken)
    {
        var spec = new SearchAssetsSpecification(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}

public class SearchAssetsSpecification : EntitiesByPaginationFilterSpec<Asset, AssetDto>
{
    public SearchAssetsSpecification(SearchAssetsRequest request)
        : base(request) =>
            Query
                .Include(e => e.Categorie)
                .Where(e => e.CategorieId.Equals(request.CategorieId!.Value), request.CategorieId.HasValue)
                .OrderBy(e => e.Code, !request.HasOrderBy());
}