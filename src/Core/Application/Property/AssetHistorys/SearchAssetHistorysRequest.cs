using FSH.WebApi.Domain.Property;

namespace FSH.WebApi.Application.Property.AssetHistorys;

public class SearchAssetHistorysRequest : PaginationFilter, IRequest<PaginationResponse<AssetHistoryDto>>
{
    public DefaultIdType? AssetId { get; set; }
}

public class SearchAssetHistorysRequestHandler : IRequestHandler<SearchAssetHistorysRequest, PaginationResponse<AssetHistoryDto>>
{
    private readonly IReadRepository<AssetHistory> _repository;

    public SearchAssetHistorysRequestHandler(IReadRepository<AssetHistory> repository) => _repository = repository;

    public async Task<PaginationResponse<AssetHistoryDto>> Handle(SearchAssetHistorysRequest request, CancellationToken cancellationToken)
    {
        var spec = new SearchAssetHistorysSpecification(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}

public class SearchAssetHistorysSpecification : EntitiesByPaginationFilterSpec<AssetHistory, AssetHistoryDto>
{
    public SearchAssetHistorysSpecification(SearchAssetHistorysRequest request)
        : base(request) =>
            Query

              // .Include(e => e.AssetId)
                .Where(e => e.AssetId.Equals(request.AssetId!.Value), request.AssetId.HasValue)
                .OrderBy(e => e.AssetId, !request.HasOrderBy());
}