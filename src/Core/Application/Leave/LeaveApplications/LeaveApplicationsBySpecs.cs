using FSH.WebApi.Domain.Leave;

namespace FSH.WebApi.Application.Leave.LeaveApplications;

public class LeaveApplicationsBySpecs : Specification<LeaveApplication>
{
    public LeaveApplicationsBySpecs(int? period, DefaultIdType employeeId, DefaultIdType? leaveTypeId) =>
        Query
            .Where(e => e.EmployeeId == employeeId)
            .Where(e => e.LeaveTypeId.Equals(leaveTypeId!), leaveTypeId.HasValue)
            .Where(e => e.LeaveAllocation.Period.Equals(period!), period.HasValue)
                .OrderByDescending(e => e.LastModifiedOn);
}
