using FSH.WebApi.Domain.Organization;

namespace FSH.WebApi.Application.Organization.Teams;

public class TeamByIdWithSubDepartmentSpec : Specification<Team, TeamDetailsDto>, ISingleResultSpecification<Team>
{
    public TeamByIdWithSubDepartmentSpec(Guid id) =>
        Query
            .Include(e => e.SubDepartment)
            .Where(e => e.Id == id);
}

public class TeamByCodeSpec : Specification<Team>, ISingleResultSpecification<Team>
{
    public TeamByCodeSpec(string code) =>
        Query
            .Where(e => e.Code == code);
}

public class TeamByNameSpec : Specification<Team>, ISingleResultSpecification<Team>
{
    public TeamByNameSpec(string name) =>
        Query
            .Where(e => e.Name == name);
}