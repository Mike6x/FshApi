using FSH.WebApi.Domain.Leave;

namespace FSH.WebApi.Application.Leave.LeaveAllocations;

public class LeaveAllocationByIdSpec : Specification<LeaveAllocation, LeaveAllocationDetailsDto>, ISingleResultSpecification<LeaveAllocation>
{
    public LeaveAllocationByIdSpec(Guid id) =>
        Query
            .Where(e => e.Id == id);
}

public class LeaveAllocationByPeriodSpec : Specification<LeaveAllocation>, ISingleResultSpecification<LeaveAllocation>
{
    public LeaveAllocationByPeriodSpec(int period) =>
        Query
            .Where(e => e.Period == period);
}

public class LeaveAllocationByTypeSpec : Specification<LeaveAllocation>, ISingleResultSpecification<LeaveAllocation>
{
    public LeaveAllocationByTypeSpec(Guid typeId) =>
        Query
            .Where(e => e.LeaveAllocationTypeId == typeId);
}

public class LeaveAllocationByEmployeeSpec : Specification<LeaveAllocation>, ISingleResultSpecification<LeaveAllocation>
{
    public LeaveAllocationByEmployeeSpec(Guid employeeId) =>
        Query
            .Where(e => e.EmployeeId == employeeId);
}