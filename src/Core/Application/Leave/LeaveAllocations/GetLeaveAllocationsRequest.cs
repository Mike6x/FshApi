using FSH.WebApi.Domain.Leave;

namespace FSH.WebApi.Application.Leave.LeaveAllocations;

public class GetLeaveAllocationsRequest : BaseFilter, IRequest<List<LeaveAllocationDto>>
{
    public bool? IsActive { get; set; }
    public int? Period { get; set; }
    public DefaultIdType? EmployeeId { get; set; }
    public DefaultIdType? LeaveAllocationTypeId { get; set; }

    public bool? IsLocked { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}

public class GetListRequestHandler(IRepository<LeaveAllocation> repository) : IRequestHandler<GetLeaveAllocationsRequest, List<LeaveAllocationDto>>
{
    private readonly IRepository<LeaveAllocation> _repository = repository;

    public async Task<List<LeaveAllocationDto>> Handle(GetLeaveAllocationsRequest request, CancellationToken cancellationToken)
    {
        var spec = new GetLeaveAllocationsSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);
        return list ?? new List<LeaveAllocationDto>();
    }
}

public class GetLeaveAllocationsSpecification : EntitiesByBaseFilterSpec<LeaveAllocation, LeaveAllocationDto>
{
    public GetLeaveAllocationsSpecification(GetLeaveAllocationsRequest request)
        : base(request) =>
            Query
                .Where(e => e.IsActive.Equals(request.IsActive!), request.IsActive.HasValue)
                .Where(e => e.IsLocked.Equals(request.IsLocked!), request.IsLocked.HasValue)
                .Where(e => e.Period.Equals(request.Period!), request.Period.HasValue)
                .Where(e => e.EmployeeId.Equals(request.EmployeeId!), request.EmployeeId.HasValue)
                .Where(e => e.LeaveAllocationTypeId.Equals(request.LeaveAllocationTypeId!), request.LeaveAllocationTypeId.HasValue)

                .Where(e => e.LastModifiedOn >= request.FromDate, request.FromDate.HasValue)
                .Where(e => e.LastModifiedOn <= request.ToDate, request.ToDate.HasValue)
                    .OrderByDescending(e => e.LastModifiedOn);
}