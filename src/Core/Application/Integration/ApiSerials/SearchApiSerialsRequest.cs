using FSH.WebApi.Domain.Integration;

namespace FSH.WebApi.Application.Integration.ApiSerials;

public class SearchApiSerialsRequest : PaginationFilter, IRequest<PaginationResponse<ApiSerialDto>>
{
    public Guid? CronJobId { get; set; }
    public string? PoNumber { get; set; }
    public string? ItemCode { get; set; } // Model, SKU
}

public class SearchApiSerialsRequestHandler(IReadRepository<ApiSerial> repository)
    : IRequestHandler<SearchApiSerialsRequest, PaginationResponse<ApiSerialDto>>
{
    private readonly IReadRepository<ApiSerial> _repository = repository;

    public async Task<PaginationResponse<ApiSerialDto>> Handle(SearchApiSerialsRequest request, CancellationToken cancellationToken)
    {
        var spec = new SearchApiSerialsSpecification(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}

public class SearchApiSerialsSpecification : EntitiesByPaginationFilterSpec<ApiSerial, ApiSerialDto>
{
    public SearchApiSerialsSpecification(SearchApiSerialsRequest request)
        : base(request) =>
            Query
                .Where(e => e.CronJobId.Equals(request.CronJobId!.Value), request.CronJobId.HasValue)
                .Where(e => string.IsNullOrEmpty(request.PoNumber) || e.PoNumber.Equals(request.PoNumber))
                .Where(e => string.IsNullOrEmpty(request.ItemCode) || e.ItemCode.Equals(request.ItemCode))
                    .OrderByDescending(e => e.LastModifiedOn, !request.HasOrderBy());
}