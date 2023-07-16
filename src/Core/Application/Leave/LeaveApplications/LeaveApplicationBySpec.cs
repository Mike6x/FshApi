using FSH.WebApi.Domain.Leave;

namespace FSH.WebApi.Application.Leave.LeaveApplications;

public class LeaveApplicationByIdSpec : Specification<LeaveApplication, LeaveApplicationDetailsDto>, ISingleResultSpecification<LeaveApplication>
{
    public LeaveApplicationByIdSpec(Guid id) =>
        Query
            .Where(e => e.Id == id);
}

// public class LeaveApplicationByCodeSpec : Specification<LeaveApplication>, ISingleResultSpecification<LeaveApplication>
// {
//    public LeaveApplicationByCodeSpec(int period) =>
//        Query
//            .Where(e => e.Period == period);
// }
// public class LeaveApplicationByNameSpec : Specification<LeaveApplication>, ISingleResultSpecification<LeaveApplication>
// {
//    public LeaveApplicationByNameSpec(string name) =>
//        Query
//            .Where(e => e.Name == name);
// }