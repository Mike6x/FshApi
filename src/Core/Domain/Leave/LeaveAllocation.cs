using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Leave;

public class LeaveAllocation : AuditableEntity, IAggregateRoot
{
    public int LeaveTypeId { get; set; }
    public virtual LeaveType LeaveType { get; set; } = default!;

    public int NumberOfDays { get; set; }
    public int Period { get; set; }
    public string? EmployeeId { get; set; }
}