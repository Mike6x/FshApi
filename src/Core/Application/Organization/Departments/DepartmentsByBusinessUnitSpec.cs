using FSH.WebApi.Domain.Organization;

namespace FSH.WebApi.Application.Organization.Departments;

public class DepartmentsByBusinessUnitSpec : Specification<Department>
{
    public DepartmentsByBusinessUnitSpec(Guid fatherId) =>
        Query.Where(e => e.BusinessUnitId == fatherId);
}