using FSH.WebApi.Domain.People;

namespace FSH.WebApi.Application.People.Employees;

public class EmployeesByExportRequestSpec : EntitiesByBaseFilterSpec<Employee, EmployeeExportDto>
{
    public EmployeesByExportRequestSpec(ExportEmployeesRequest request)
        : base(request) =>
            Query
                .OrderBy(e => e.FirstName);
}

public class EmployeesByExportRequestWithDepartmentSpec : EntitiesByBaseFilterSpec<Employee, EmployeeExportDto>
{
    public EmployeesByExportRequestWithDepartmentSpec(ExportEmployeesRequest request)
        : base(request) =>
            Query
                .Include(e => e.Department)
                .Where(e => e.DepartmentId.Equals(request.DepartmentId!.Value), request.DepartmentId.HasValue);
}

public class EmployeesByExportRequestWithTeamSpec : EntitiesByBaseFilterSpec<Employee, EmployeeExportDto>
{
    public EmployeesByExportRequestWithTeamSpec(ExportEmployeesRequest request)
        : base(request) =>
            Query
                .Include(e => e.Team)
                .Where(e => e.TeamId.Equals(request.TeamId!.Value), request.TeamId.HasValue);
}