using FSH.WebApi.Domain.Organization;

namespace FSH.WebApi.Application.Organization.Departments;

public class DepartmentByIdWithBusinessUnitSpec : Specification<Department, DepartmentDetailsDto>, ISingleResultSpecification<Department>
{
    public DepartmentByIdWithBusinessUnitSpec(Guid id) =>
        Query
            .Include(e => e.BusinessUnit)
            .Where(e => e.Id == id);
}

public class DepartmentByCodeSpec : Specification<Department>, ISingleResultSpecification<Department>
{
    public DepartmentByCodeSpec(string code) =>
        Query
            .Where(e => e.Code == code);
}

public class DepartmentByNameSpec : Specification<Department>, ISingleResultSpecification<Department>
{
    public DepartmentByNameSpec(string name) =>
        Query
            .Where(e => e.Name == name);
}