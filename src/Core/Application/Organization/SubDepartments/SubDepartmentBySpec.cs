using FSH.WebApi.Domain.People;

namespace FSH.WebApi.Application.Organization.SubDepartments;

public class SubDepartmentByIdWithDepartmentSpec : Specification<SubDepartment, SubDepartmentDetailsDto>, ISingleResultSpecification<SubDepartment>
{
    public SubDepartmentByIdWithDepartmentSpec(Guid id) =>
        Query
            .Include(e => e.Department)
            .Where(e => e.Id == id);
}

public class SubDepartmentByCodeSpec : Specification<SubDepartment>, ISingleResultSpecification<SubDepartment>
{
    public SubDepartmentByCodeSpec(string code) =>
        Query
            .Where(e => e.Code == code);
}

public class SubDepartmentByNameSpec : Specification<SubDepartment>, ISingleResultSpecification<SubDepartment>
{
    public SubDepartmentByNameSpec(string name) =>
        Query
            .Where(e => e.Name == name);
}