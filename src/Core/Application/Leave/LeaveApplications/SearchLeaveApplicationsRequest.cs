using FSH.WebApi.Domain.Leave;

namespace FSH.WebApi.Application.Leave.LeaveApplications;

public class SearchLeaveApplicationsRequest : PaginationFilter, IRequest<PaginationResponse<LeaveApplicationDto>>
{
    public DefaultIdType? EmployeeId { get; set; }
    public DefaultIdType? LeaveTypeId { get; set; }
    public RequestStatus? Status { get; set; }
    public DefaultIdType? ApproverId { get; set; }

    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}

public class SearchRequestHandler(IReadRepository<LeaveApplication> repository) : IRequestHandler<SearchLeaveApplicationsRequest, PaginationResponse<LeaveApplicationDto>>
{
    private readonly IReadRepository<LeaveApplication> _repository = repository;

    public async Task<PaginationResponse<LeaveApplicationDto>> Handle(SearchLeaveApplicationsRequest request, CancellationToken cancellationToken)
    {
        var spec = new SearchLeaveApplicationsRequestSpecification(request);

        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}

public class SearchLeaveApplicationsRequestSpecification : EntitiesByPaginationFilterSpec<LeaveApplication, LeaveApplicationDto>
{
    public SearchLeaveApplicationsRequestSpecification(SearchLeaveApplicationsRequest request)
        : base(request) =>
            Query
                .Where(e => e.EmployeeId.Equals(request.EmployeeId!), request.EmployeeId.HasValue)
                .Where(e => e.LeaveTypeId.Equals(request.LeaveTypeId!), request.LeaveTypeId.HasValue)
                .Where(e => e.Status.Equals(request.Status!), request.Status.HasValue)
                .Where(e => e.ApproverId.Equals(request.ApproverId), request.ApproverId.HasValue)

                .Where(e => e.LastModifiedOn >= request.FromDate, request.FromDate.HasValue)
                .Where(e => e.LastModifiedOn <= request.ToDate, request.ToDate.HasValue)
                    .OrderByDescending(e => e.LastModifiedOn, !request.HasOrderBy());
}