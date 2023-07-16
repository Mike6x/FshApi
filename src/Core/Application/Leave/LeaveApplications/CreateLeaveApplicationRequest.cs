using FSH.WebApi.Application.Leave.LeaveAllocations;
using FSH.WebApi.Application.Settings.Dimensions;
using FSH.WebApi.Domain.Leave;
using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Application.Leave.LeaveApplications;

public class CreateLeaveApplicationRequest : IRequest<Guid>
{
    public DefaultIdType EmployeeId { get; set; }
    public DefaultIdType LeaveTypeId { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public DayOffType FirstLeaveDay { get; set; }
    public DayOffType LastLeaveDay { get; set; }

    // public DateTime RequestOn { get; set; }
    public string? RequestRemarks { get; set; }

    public RequestStatus Status { get; set; }
    public DefaultIdType? ApproverId { get; set; }

    // public DateTime? ApprovedOn { get; set; }
    // public string? ApproverRemarks { get; set; }
}

public class CreateRequestHandler(
    IRepositoryWithEvents<LeaveApplication> repository,
    IRepositoryWithEvents<LeaveAllocation> leaveAllocationRepo,
    IReadRepository<Dimension> dimensionRepo,
    IStringLocalizer<CreateRequestHandler> localizer)
    : IRequestHandler<CreateLeaveApplicationRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<LeaveApplication> _repository = repository;
    private readonly IRepositoryWithEvents<LeaveAllocation> _leaveAllocationRepo = leaveAllocationRepo;
    private readonly IReadRepository<Dimension> _dimensionRepo = dimensionRepo;
    private readonly IStringLocalizer _t = localizer;

    public async Task<Guid> Handle(CreateLeaveApplicationRequest request, CancellationToken cancellationToken)
    {
        if (request.Status != RequestStatus.Edited) request.Status = RequestStatus.Submited;

        var leaveType = await _dimensionRepo.FirstOrDefaultAsync(new DimensionByIdSpec(request.LeaveTypeId), cancellationToken)
            ?? throw new NotFoundException(_t["LeaveType with ID: {0} not found.", request.LeaveTypeId]);

        if (request.ToDate < request.FromDate) request.ToDate = request.FromDate;
        double offTime = LeaveApplicationHelper.DayOffCalculate(request.FromDate, request.ToDate, request.FirstLeaveDay, request.LastLeaveDay);

        if (offTime > leaveType.Value) throw new ConflictException(_t["Requested time is over limitation."]);

        double numberOfOnHandDays = leaveType.Value - offTime;

        var entity = new LeaveApplication(
            request.EmployeeId,
            request.LeaveTypeId,
            request.FromDate,
            request.ToDate,
            request.FirstLeaveDay,
            request.LastLeaveDay,
            DateTime.UtcNow,
            request.RequestRemarks,
            request.Status,
            request.ApproverId,
            null,
            null,
            null,
            leaveType.Value,
            numberOfOnHandDays);

        if (request.Status == RequestStatus.Submited && leaveType.FatherId != null && leaveType.FatherId != Guid.Empty)
        {
            var empAllocation = await _leaveAllocationRepo.FirstOrDefaultAsync(
                    new LeaveAllocationsBySpecs(true, null, request.EmployeeId, (DefaultIdType)leaveType.FatherId!, false), cancellationToken)
                ?? throw new ConflictException(_t["Not found leave allocation for Employee with ID: {0}.", request.EmployeeId]);

            if (offTime > empAllocation!.NumberOfOnHandDays)
            {
                throw new ConflictException(_t["Not enought allocation room for request of Employee with ID: {0}", request.EmployeeId]);
            }

            numberOfOnHandDays = empAllocation!.NumberOfOnHandDays - offTime;
            entity.Update(null, null, null, null, null, null, null, null, null, null, null, null, empAllocation.Id, empAllocation.NumberOfValidDays, numberOfOnHandDays);

            empAllocation.Update(null, null, null, null, null, null, null, null, null, null, null, numberOfOnHandDays, null);
            await _leaveAllocationRepo.UpdateAsync(empAllocation, cancellationToken);
        }

        await _repository.AddAsync(entity, cancellationToken);
        return entity.Id;
    }
}