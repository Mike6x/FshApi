using FSH.WebApi.Domain.Organization;

namespace FSH.WebApi.Application.Organization.Teams;

public class TeamsBySubDepartmentSpec : Specification<Team>
{
    public TeamsBySubDepartmentSpec(Guid fatherId) =>
        Query
            .Where(e => e.SubDepartmentId == fatherId);
}