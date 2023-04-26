using FSH.WebApi.Domain.People;

namespace FSH.WebApi.Application.Organization.SubDepartments;

public class SubDepartmentsByDepartmentSpec : Specification<SubDepartment>
{
    public SubDepartmentsByDepartmentSpec(Guid fatherId) =>
        Query
            .Where(e => e.DepartmentId == fatherId);
}
