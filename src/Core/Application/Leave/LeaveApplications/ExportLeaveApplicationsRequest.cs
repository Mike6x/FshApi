using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Domain.Leave;

namespace FSH.WebApi.Application.Leave.LeaveApplications;

public class ExportLeaveApplicationsRequest : BaseFilter, IRequest<Stream>
{
    public DefaultIdType? EmployeeId { get; set; }
    public DefaultIdType? LeaveTypeId { get; set; }
    public RequestStatus? Status { get; set; }
    public DefaultIdType? ApproverId { get; set; }

    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}

public class ExportRequestHandler(IReadRepository<LeaveApplication> repository, IExcelWriter excelWriter)
    : IRequestHandler<ExportLeaveApplicationsRequest, Stream>
{
    private readonly IReadRepository<LeaveApplication> _repository = repository;
    private readonly IExcelWriter _excelWriter = excelWriter;

    public async Task<Stream> Handle(ExportLeaveApplicationsRequest request, CancellationToken cancellationToken)
    {
        var spec = new ExportLeaveApplicationsSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class ExportLeaveApplicationsSpecification : EntitiesByBaseFilterSpec<LeaveApplication, LeaveApplicationExportDto>
{
    public ExportLeaveApplicationsSpecification(ExportLeaveApplicationsRequest request)
        : base(request) =>
        Query
            .Where(e => e.EmployeeId.Equals(request.EmployeeId!), request.EmployeeId.HasValue)
            .Where(e => e.LeaveTypeId.Equals(request.LeaveTypeId!), request.LeaveTypeId.HasValue)
            .Where(e => e.Status.Equals(request.Status!), request.Status.HasValue)
            .Where(e => e.ApproverId.Equals(request.ApproverId!), request.ApproverId.HasValue)

            .Where(e => e.LastModifiedOn >= request.FromDate, request.FromDate.HasValue)
            .Where(e => e.LastModifiedOn <= request.ToDate, request.ToDate.HasValue)
                .OrderByDescending(e => e.LastModifiedOn);
}