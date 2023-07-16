using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Application.Settings.BackgroundJobs;

public class SearchBackgroundJobsRequest : PaginationFilter, IRequest<PaginationResponse<BackgroundJobDto>>
{
    public BackgroundJobType? Type { get; set; }

    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}

public class SearchBackgroundJobsRequestHandler(IReadRepository<BackgroundJob> repository) : IRequestHandler<SearchBackgroundJobsRequest, PaginationResponse<BackgroundJobDto>>
{
    private readonly IReadRepository<BackgroundJob> _repository = repository;

    public async Task<PaginationResponse<BackgroundJobDto>> Handle(SearchBackgroundJobsRequest request, CancellationToken cancellationToken)
    {
        var spec = new SearchBackgroundJobsSpecification(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}

public class SearchBackgroundJobsSpecification : EntitiesByPaginationFilterSpec<BackgroundJob, BackgroundJobDto>
{
    public SearchBackgroundJobsSpecification(SearchBackgroundJobsRequest request)
        : base(request) =>
            Query
                .Where(e => e.Type.Equals(request.Type!.Value), request.Type.HasValue && !request.Type.Equals(BackgroundJobType.All))
                .Where(e => e.FromDate >= request.FromDate, request.FromDate.HasValue)
                .Where(e => e.ToDate <= request.ToDate, request.ToDate.HasValue)
                    .OrderByDescending(e => e.LastModifiedOn, !request.HasOrderBy());
}