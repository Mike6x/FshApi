using FSH.WebApi.Domain.Leave;

namespace FSH.WebApi.Application.Leave.LeaveApplications;

public class GetLeaveApplicationsRequest : BaseFilter, IRequest<List<LeaveApplicationDto>>
{
    public int? Period { get; set; }

    public DefaultIdType? EmployeeId { get; set; }
    public DefaultIdType? LeaveTypeId { get; set; }
    public RequestStatus? Status { get; set; }
    public DefaultIdType? ApproverId { get; set; }

    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}

public class GetListRequestHandler : IRequestHandler<GetLeaveApplicationsRequest, List<LeaveApplicationDto>>
{
    private readonly IRepository<LeaveApplication> _repository;
    public GetListRequestHandler(IRepository<LeaveApplication> repository)
        => _repository = repository;

    public async Task<List<LeaveApplicationDto>> Handle(GetLeaveApplicationsRequest request, CancellationToken cancellationToken)
    {
        var spec = new GetLeaveApplicationsSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);
        return list ?? new List<LeaveApplicationDto>();
    }
}

public class GetLeaveApplicationsSpecification : EntitiesByBaseFilterSpec<LeaveApplication, LeaveApplicationDto>
{
    public GetLeaveApplicationsSpecification(GetLeaveApplicationsRequest request)
        : base(request) =>
            Query
                .Where(e => e.LeaveAllocation.Period.Equals(request.Period!), request.Period.HasValue)
                .Where(e => e.EmployeeId.Equals(request.EmployeeId!), request.EmployeeId.HasValue)
                .Where(e => e.LeaveTypeId.Equals(request.LeaveTypeId!), request.LeaveTypeId.HasValue)
                .Where(e => e.Status.Equals(request.Status!), request.Status.HasValue)
                .Where(e => e.ApproverId.Equals(request.ApproverId), request.ApproverId.HasValue)

                .Where(e => e.LastModifiedOn >= request.FromDate, request.FromDate.HasValue)
                .Where(e => e.LastModifiedOn <= request.ToDate, request.ToDate.HasValue)
                    .OrderByDescending(e => e.LastModifiedOn);
}