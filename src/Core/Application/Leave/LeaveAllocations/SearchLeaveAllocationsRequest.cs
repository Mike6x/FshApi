using FSH.WebApi.Domain.Leave;

namespace FSH.WebApi.Application.Leave.LeaveAllocations;

public class SearchLeaveAllocationsRequest : PaginationFilter, IRequest<PaginationResponse<LeaveAllocationDto>>
{
    public bool? IsActive { get; set; }
    public int? Period { get; set; }
    public DefaultIdType? EmployeeId { get; set; }
    public DefaultIdType? LeaveAllocationTypeId { get; set; }

    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public bool? IsLocked { get; set; }
}

public class SearchRequestHandler(IReadRepository<LeaveAllocation> repository) : IRequestHandler<SearchLeaveAllocationsRequest, PaginationResponse<LeaveAllocationDto>>
{
    private readonly IReadRepository<LeaveAllocation> _repository = repository;

    public async Task<PaginationResponse<LeaveAllocationDto>> Handle(SearchLeaveAllocationsRequest request, CancellationToken cancellationToken)
    {
        var spec = new SearchLeaveAllocationsRequestSpecification(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}

public class SearchLeaveAllocationsRequestSpecification : EntitiesByPaginationFilterSpec<LeaveAllocation, LeaveAllocationDto>
{
    public SearchLeaveAllocationsRequestSpecification(SearchLeaveAllocationsRequest request)
        : base(request) =>
            Query
                .Where(e => e.IsActive.Equals(request.IsActive!), request.IsActive.HasValue)
                .Where(e => e.IsLocked.Equals(request.IsLocked!), request.IsLocked.HasValue)
                .Where(e => e.Period.Equals(request.Period!), request.Period.HasValue)
                .Where(e => e.EmployeeId.Equals(request.EmployeeId!), request.EmployeeId.HasValue)
                .Where(e => e.LeaveAllocationTypeId.Equals(request.LeaveAllocationTypeId!), request.LeaveAllocationTypeId.HasValue)

                .Where(e => e.LastModifiedOn >= request.FromDate, request.FromDate.HasValue)
                .Where(e => e.LastModifiedOn <= request.ToDate, request.ToDate.HasValue)
                    .OrderByDescending(e => e.LastModifiedOn, !request.HasOrderBy());
}