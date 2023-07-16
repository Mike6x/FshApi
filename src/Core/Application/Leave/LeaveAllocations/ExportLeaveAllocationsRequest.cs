using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Domain.Leave;

namespace FSH.WebApi.Application.Leave.LeaveAllocations;

public class ExportLeaveAllocationsRequest : BaseFilter, IRequest<Stream>
{
    public bool? IsActive { get; set; }
    public int? Period { get; set; }
    public DefaultIdType? EmployeeId { get; set; }
    public DefaultIdType? LeaveAllocationTypeId { get; set; }

    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}

public class ExportRequestHandler(IReadRepository<LeaveAllocation> repository, IExcelWriter excelWriter)
    : IRequestHandler<ExportLeaveAllocationsRequest, Stream>
{
    private readonly IReadRepository<LeaveAllocation> _repository = repository;
    private readonly IExcelWriter _excelWriter = excelWriter;

    public async Task<Stream> Handle(ExportLeaveAllocationsRequest request, CancellationToken cancellationToken)
    {
        var spec = new ExportLeaveAllocationsSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class ExportLeaveAllocationsSpecification : EntitiesByBaseFilterSpec<LeaveAllocation, LeaveAllocationExportDto>
{
    public ExportLeaveAllocationsSpecification(ExportLeaveAllocationsRequest request)
        : base(request) =>
        Query
            .Where(e => e.IsActive.Equals(request.IsActive!), request.IsActive.HasValue)
            .Where(e => e.Period.Equals(request.Period!), request.Period.HasValue)
            .Where(e => e.EmployeeId.Equals(request.EmployeeId!), request.EmployeeId.HasValue)
            .Where(e => e.LeaveAllocationTypeId.Equals(request.LeaveAllocationTypeId!), request.LeaveAllocationTypeId.HasValue)

            .Where(e => e.LastModifiedOn >= request.FromDate, request.FromDate.HasValue)
            .Where(e => e.LastModifiedOn <= request.ToDate, request.ToDate.HasValue)
                .OrderByDescending(e => e.LastModifiedOn);
}