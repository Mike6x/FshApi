using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Application.Settings.Dimensions;

public class SearchDimensionsRequest : PaginationFilter, IRequest<PaginationResponse<DimensionDto>>
{
    public string? Type { get;  set; }
    public DefaultIdType? FatherId { get; set; }
    public bool? IsActive { get; set; }
}

public class SearchDimensionsRequestHandler(IReadRepository<Dimension> repository) : IRequestHandler<SearchDimensionsRequest, PaginationResponse<DimensionDto>>
{
    private readonly IReadRepository<Dimension> _repository = repository;

    public async Task<PaginationResponse<DimensionDto>> Handle(SearchDimensionsRequest request, CancellationToken cancellationToken)
    {
        var spec = new SearchDimensionsSpecification(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}

public class SearchDimensionsSpecification : EntitiesByPaginationFilterSpec<Dimension, DimensionDto>
{
    public SearchDimensionsSpecification(SearchDimensionsRequest request)
        : base(request) =>
            Query
                .Where(e => e.IsActive.Equals(request.IsActive!), request.IsActive.HasValue)
                .Where(e => string.IsNullOrEmpty(request.Type) || e.Type.Equals(request.Type))
                .Where(e => e.FatherId.Equals(request.FatherId!.Value), request.FatherId.HasValue)
                    .OrderBy(e => e.Order, !request.HasOrderBy());
}