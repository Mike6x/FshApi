using FSH.WebApi.Domain.People;

namespace FSH.WebApi.Application.People.Employees;

public class EmployeesByBusinessUnitSpec : Specification<Employee>
{
    public EmployeesByBusinessUnitSpec(DefaultIdType fatherId) =>
        Query
            .Where(e => e.DepartmentId == fatherId);
}

public class EmployeesByDepartmentSpec : Specification<Employee>
{
    public EmployeesByDepartmentSpec(DefaultIdType fatherId) =>
        Query
            .Where(e => e.DepartmentId == fatherId);
}

public class EmployeesBySubDepartmentSpec : Specification<Employee>
{
    public EmployeesBySubDepartmentSpec(DefaultIdType fatherId) =>
        Query
            .Where(e => e.SubDepartmentId == fatherId);
}

public class EmployeesByTeamSpec : Specification<Employee>
{
    public EmployeesByTeamSpec(DefaultIdType fatherId) =>
        Query
            .Where(e => e.TeamId == fatherId);
}

public class EmployeesByTitleSpec : Specification<Employee>
{
    public EmployeesByTitleSpec(DefaultIdType fatherId) =>
        Query
            .Where(e => e.TitleId == fatherId);
}

public class EmployeesBySuperiorSpec : Specification<Employee>
{
    public EmployeesBySuperiorSpec(DefaultIdType fatherId) =>
        Query
            .Where(e => e.SuperiorId == fatherId);
}