using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Application.Settings.CronJobs;

public class SearchCronJobsRequest : PaginationFilter, IRequest<PaginationResponse<CronJobDto>>
{
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}

public class SearchCronJobsRequestHandler(IReadRepository<CronJob> repository)
    : IRequestHandler<SearchCronJobsRequest, PaginationResponse<CronJobDto>>
{
    private readonly IReadRepository<CronJob> _repository = repository;

    public async Task<PaginationResponse<CronJobDto>> Handle(SearchCronJobsRequest request, CancellationToken cancellationToken)
    {
        var spec = new SearchCronJobsSpecification(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}

public class SearchCronJobsSpecification : EntitiesByPaginationFilterSpec<CronJob, CronJobDto>
{
    public SearchCronJobsSpecification(SearchCronJobsRequest request)
        : base(request) =>
            Query
                .Where(e => e.FromDate >= request.FromDate, request.FromDate.HasValue)
                .Where(e => e.ToDate <= request.ToDate, request.ToDate.HasValue)
                    .OrderByDescending(e => e.LastModifiedOn);
}