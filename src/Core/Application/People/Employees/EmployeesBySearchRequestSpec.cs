// using FSH.WebApi.Domain.People;

// namespace FSH.WebApi.Application.People.Employees;

// public class EmployeesBySearchRequestWithDepartmentSpec : EntitiesByPaginationFilterSpec<Employee, EmployeeDto>
// {
//    public EmployeesBySearchRequestWithDepartmentSpec(SearchEmployeesRequest request)
//        : base(request) =>
//            Query
//                .Include(e => e.Department)
//                .Where(e => e.DepartmentId.Equals(request.DepartmentId!.Value), request.DepartmentId.HasValue)
//                .OrderBy(e => e.FirstName, !request.HasOrderBy());
// }

// public class EmployeesBySearchRequestWithTeamSpec : EntitiesByPaginationFilterSpec<Employee, EmployeeDto>
// {
//    public EmployeesBySearchRequestWithTeamSpec(SearchEmployeesRequest request)
//        : base(request) =>
//            Query
//                .Include(e => e.Team)
//                .Where(e => e.TeamId.Equals(request.TeamId!.Value), request.TeamId.HasValue)
//                .OrderBy(e => e.FirstName, !request.HasOrderBy());
// }