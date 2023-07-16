using FSH.WebApi.Domain.Leave;

namespace FSH.WebApi.Application.Leave.LeaveApplications;

public class LeaveApplicationDto : IDto
{
    public DefaultIdType Id { get; set; }
    public DefaultIdType EmployeeId { get;  set; }
    public string EmployeeCode { get; set; } = default!;
    public string EmployeeFirstName { get; set; } = default!;

    public DefaultIdType LeaveTypeId { get;  set; }
    public string LeaveTypeName { get; set; } = default!;

    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public DayOffType FirstLeaveDay { get; set; }
    public DayOffType LastLeaveDay { get; set; }

    public DateTime RequestOn { get; set; }
    public string? RequestRemarks { get; set; }

    public RequestStatus Status { get; set; }

    public DefaultIdType? ApproverId { get; set; }
    public string? ApproverCode { get; set; }
    public string? ApproverFirstName { get; set; }
    public DateTime? ApprovedOn { get;  set; }
    public string? ApproverRemarks { get;  set; }

    public DefaultIdType? LeaveAllocationId { get; set; }
    public int? LeaveAllocationPeriod { get; set; }

    public double? NumberOfValidDays { get; set; }
    public double? NumberOfOnHandDays { get; set; }

    public DefaultIdType LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }

    public double OffTime => LeaveApplicationHelper.DayOffCalculate(FromDate, ToDate, FirstLeaveDay, LastLeaveDay);
}

public class LeaveApplicationDetailsDto : IDto
{
    public DefaultIdType Id { get; set; }
    public DefaultIdType EmployeeId { get; set; }
    public string? EmployeeCode { get; set; } = default!;
    public string? EmployeeFirstName { get; set; } = default!;

    public DefaultIdType LeaveTypeId { get; set; }
    public string? LeaveTypeName { get; set; } = default!;

    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public DayOffType FirstLeaveDay { get; set; }
    public DayOffType LastLeaveDay { get; set; }

    public DateTime RequestOn { get; set; }
    public string? RequestRemarks { get; set; }

    public RequestStatus Status { get; set; }

    public DefaultIdType? ApproverId { get; set; }
    public string? ApproverCode { get; set; }
    public string? ApproverFirstName { get; set; } = default!;
    public DateTime? ApprovedOn { get; set; }
    public string? ApproverRemarks { get; set; }

    public DefaultIdType? LeaveAllocationId { get; set; }
    public int? LeaveAllocationPeriod { get; set; }

    public double? NumberOfValidDays { get; set; }
    public double? NumberOfOnHandDays { get; set; }
}

public class LeaveApplicationExportDto : IDto
{
    public DefaultIdType Id { get; set; }
    public DefaultIdType EmployeeId { get; set; }
    public string EmployeeCode { get; set; } = default!;
    public string EmployeeFirstName { get; set; } = default!;

    public DefaultIdType LeaveTypeId { get; set; }
    public string LeaveTypeName { get; set; } = default!;

    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public DayOffType FirstLeaveDay { get; set; }
    public DayOffType LastLeaveDay { get; set; }

    public DateTime RequestOn { get; set; }
    public string? RequestRemarks { get; set; }

    public RequestStatus Status { get; set; }

    public DefaultIdType? ApproverId { get; set; }
    public string ApproverCode { get; set; } = default!;
    public string ApproverFirstName { get; set; } = default!;
    public DateTime? ApprovedOn { get; set; }
    public string? ApproverRemarks { get; set; }

    public DefaultIdType? LeaveAllocationId { get; set; }
    public int? LeaveAllocationPeriod { get; set; }

    public double? NumberOfValidDays { get; set; }
    public double? NumberOfOnHandDays { get; set; }

    public DefaultIdType CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public DefaultIdType LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
}