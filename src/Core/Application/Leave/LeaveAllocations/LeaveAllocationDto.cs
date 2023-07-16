namespace FSH.WebApi.Application.Leave.LeaveAllocations;
public class LeaveAllocationDto : IDto
{
    public DefaultIdType Id { get; set; }
    public bool IsActive { get; set; }
    public int Period { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public DefaultIdType EmployeeId { get; set; }
    public string EmployeeCode { get; set; } = default!;
    public string EmployeeFirstName { get; set; } = default!;
    public DefaultIdType LeaveAllocationTypeId { get; set; }
    public string LeaveAllocationTypeName { get; set; } = default!;

    public int NumberOfAnnualDays { get; set; }
    public int NumberOfExtraDays { get; set; }

    public double NumberOfCarryOverDays { get; set; }
    public double NumberOfCompensationDays { get; set; }
    public double NumberOfValidDays { get; set; }
    public double NumberOfOnHandDays { get; set; }

    public bool IsLocked { get; set; }
    public DefaultIdType LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
}

public class LeaveAllocationDetailsDto : IDto
{
    public DefaultIdType Id { get; set; }
    public bool IsActive { get; set; }
    public int Period { get; set; }
    public DefaultIdType EmployeeId { get; set; }
    public string EmployeeCode { get; set; } = default!;
    public string EmployeeFirstName { get; set; } = default!;
    public DefaultIdType LeaveAllocationTypeId { get; set; }
    public string LeaveAllocationTypeName { get; set; } = default!;

    public int NumberOfAnnualDays { get; set; }
    public int NumberOfExtraDays { get; set; }

    public double NumberOfCarryOverDays { get; set; }
    public double NumberOfCompensationDays { get; set; }
    public double NumberOfValidDays { get; set; }
    public double NumberOfOnHandDays { get; set; }
    public bool IsLocked { get; set; }
}

public class LeaveAllocationExportDto : IDto
{
    public bool IsActive { get; set; }
    public int Period { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public DefaultIdType EmployeeId { get; set; }
    public string EmployeeCode { get; set; } = default!;
    public string EmployeeFirstName { get; set; } = default!;
    public DefaultIdType LeaveAllocationTypeId { get; set; }
    public string LeaveAllocationTypeName { get; set; } = default!;

    public int NumberOfAnnualDays { get; set; }
    public int NumberOfExtraDays { get; set; }

    public double NumberOfCarryOverDays { get; set; }
    public double NumberOfCompensationDays { get; set; }
    public double NumberOfValidDays { get; set; }
    public double NumberOfOnHandDays { get; set; }
    public bool IsLocked { get; set; }

    public DefaultIdType CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public DefaultIdType LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
}