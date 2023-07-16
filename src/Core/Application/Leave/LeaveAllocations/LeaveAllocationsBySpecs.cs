using FSH.WebApi.Domain.Leave;

namespace FSH.WebApi.Application.Leave.LeaveAllocations;
public class LeaveAllocationsBySpecs : Specification<LeaveAllocation>
{
    public LeaveAllocationsBySpecs(bool? isActive, int? period, DefaultIdType employeeId, DefaultIdType typeId,  bool? isLocked) =>
    Query
        .Where(e => e.IsActive.Equals(isActive!), isActive.HasValue)
        .Where(e => e.IsLocked.Equals(isLocked!), isLocked.HasValue)
        .Where(e => e.Period.Equals(period!), period.HasValue)
        .Where(e => e.EmployeeId == employeeId)
        .Where(e => e.LeaveAllocationTypeId == typeId)
                .OrderByDescending(e => e.LastModifiedOn);
}