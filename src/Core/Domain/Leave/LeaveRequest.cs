using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Leave;

public class LeaveRequest : AuditableEntity, IAggregateRoot
{
    public int LeaveTypeId { get; set; }
    public virtual LeaveType LeaveType { get; set; } = default!;

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime DateRequested { get; set; }
    public string? RequestComments { get; set; }
    public DateTime? DateActioned { get; set; }
    public bool? Approved { get; set; }
    public bool Cancelled { get; set; }
    public string? RequestingEmployeeId { get; set; }
}