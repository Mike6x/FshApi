using FSH.WebApi.Domain.Property;

namespace FSH.WebApi.Application.Property.AssetStatuses;

public class SearchAssetStatusesRequest : PaginationFilter, IRequest<PaginationResponse<AssetStatusDto>>
{
    public AssetStatusType? Type { get;  set; }
}

public class SearchAssetStatusesRequestHandler : IRequestHandler<SearchAssetStatusesRequest, PaginationResponse<AssetStatusDto>>
{
    private readonly IReadRepository<AssetStatus> _repository;

    public SearchAssetStatusesRequestHandler(IReadRepository<AssetStatus> repository) => _repository = repository;

    public async Task<PaginationResponse<AssetStatusDto>> Handle(SearchAssetStatusesRequest request, CancellationToken cancellationToken)
    {
        var spec = new SearchAssetStatusesSpecification(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}

public class SearchAssetStatusesSpecification : EntitiesByPaginationFilterSpec<AssetStatus, AssetStatusDto>
{
    public SearchAssetStatusesSpecification(SearchAssetStatusesRequest request)
        : base(request) =>
            Query
                .OrderBy(e => e.Name, !request.HasOrderBy())
                .Where(e => e.Type.Equals(request.Type!.Value), request.Type.HasValue);
}