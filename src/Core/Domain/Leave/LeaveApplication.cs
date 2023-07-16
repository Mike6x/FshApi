using FSH.WebApi.Domain.People;
using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Domain.Leave;

public class LeaveApplication(
    DefaultIdType employeeId,
    DefaultIdType leaveTypeId,
    DateTime fromDate,
    DateTime toDate,
    DayOffType firstLeaveDay,
    DayOffType lastLeaveDay,
    DateTime requestOn,
    string? requestRemarks,
    RequestStatus status,
    DefaultIdType? approverId,
    DateTime? approvedOn,
    string? approverRemarks,
    DefaultIdType? leaveAllocationId,
    double? numberOfValidDays,
    double? numberOfOnHandDays) : AuditableEntity, IAggregateRoot
{
    public DefaultIdType EmployeeId { get; private set; } = employeeId;
    public virtual Employee Employee { get; private set; } = default!;
    public DefaultIdType LeaveTypeId { get; private set; } = leaveTypeId;
    public virtual Dimension LeaveType { get; private set; } = default!;

    public DateTime FromDate { get; private set; } = fromDate;
    public DateTime ToDate { get; private set; } = toDate;
    public DayOffType FirstLeaveDay { get; private set; } = firstLeaveDay;
    public DayOffType LastLeaveDay { get; private set; } = lastLeaveDay;

    public DateTime RequestOn { get; private set; } = requestOn;
    public string? RequestRemarks { get; private set; } = requestRemarks;

    public RequestStatus Status { get; private set; } = status;

    public DefaultIdType? ApproverId { get; private set; } = approverId;
    public virtual Employee? Approver { get; private set; }
    public DateTime? ApprovedOn { get; private set; } = approvedOn;
    public string? ApproverRemarks { get; private set; } = approverRemarks;

    public DefaultIdType? LeaveAllocationId { get; private set; } = leaveAllocationId;
    public virtual LeaveAllocation? LeaveAllocation { get; private set; }
    public double? NumberOfValidDays { get; private set; } = numberOfValidDays;
    public double? NumberOfOnHandDays { get; private set; } = numberOfOnHandDays;

    public LeaveApplication()
        : this(Guid.Empty, Guid.Empty, DateTime.UtcNow, DateTime.UtcNow, DayOffType.FullDay, DayOffType.FullDay, DateTime.UtcNow, null, RequestStatus.Edited, null, null, null, Guid.Empty, null, null)
    {
    }

    public LeaveApplication Update(
        DefaultIdType? employeeId,
        DefaultIdType? leaveTypeId,
        DateTime? fromDate,
        DateTime? toDate,
        DayOffType? firstLeaveDay,
        DayOffType? lastLeaveDay,
        DateTime? requestOn,
        string? requestRemarks,
        RequestStatus? status,
        DefaultIdType? approverId,
        DateTime? approvedOn,
        string? approverRemarks,
        DefaultIdType? leaveAllocationId,
        double? numberOfValidDays,
        double? numberOfOnHandDays)
    {
        if (employeeId.HasValue && employeeId.Value != DefaultIdType.Empty && !EmployeeId.Equals(employeeId.Value)) EmployeeId = employeeId.Value;
        if (leaveTypeId.HasValue && leaveTypeId.Value != DefaultIdType.Empty && !LeaveTypeId.Equals(leaveTypeId.Value)) LeaveTypeId = leaveTypeId.Value;

        if (fromDate.HasValue && fromDate.Value != DateTime.MinValue && !FromDate.Equals(fromDate.Value)) FromDate = fromDate.Value;
        if (toDate.HasValue && toDate.Value != DateTime.MinValue && !ToDate.Equals(toDate.Value)) ToDate = toDate.Value;
        if (firstLeaveDay is not null && !FirstLeaveDay.Equals(firstLeaveDay)) FirstLeaveDay = (DayOffType)firstLeaveDay;
        if (lastLeaveDay is not null && !LastLeaveDay.Equals(lastLeaveDay)) LastLeaveDay = (DayOffType)lastLeaveDay;

        if (requestOn.HasValue && requestOn.Value != DateTime.MinValue && !RequestOn.Equals(requestOn.Value)) RequestOn = requestOn.Value;
        if (requestRemarks is not null && RequestRemarks?.Equals(requestRemarks) is not true) RequestRemarks = requestRemarks;

        if (status is not null && !Status.Equals(status)) Status = (RequestStatus)status;

        if (approverId.HasValue && approverId.Value != DefaultIdType.Empty && !ApproverId.Equals(approverId.Value)) ApproverId = approverId.Value;
        if (approvedOn.HasValue && approvedOn.Value != DateTime.MinValue && !ApprovedOn.Equals(approvedOn.Value)) ApprovedOn = approvedOn.Value;
        if (approverRemarks is not null && ApproverRemarks?.Equals(approverRemarks) is not true) ApproverRemarks = approverRemarks;

        if (leaveAllocationId.HasValue && leaveAllocationId.Value != DefaultIdType.Empty && !LeaveAllocationId.Equals(leaveAllocationId.Value)) LeaveAllocationId = leaveAllocationId.Value;
        if (numberOfValidDays.HasValue && numberOfValidDays != NumberOfValidDays) NumberOfValidDays = numberOfValidDays.Value;
        if (numberOfOnHandDays.HasValue && numberOfOnHandDays != NumberOfOnHandDays) NumberOfOnHandDays = numberOfOnHandDays.Value;

        return this;
    }
}