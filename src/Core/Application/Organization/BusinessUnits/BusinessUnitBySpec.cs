using FSH.WebApi.Domain.Organization;

namespace FSH.WebApi.Application.Organization.BusinessUnits;

public class BusinessUnitByIdSpec : Specification<BusinessUnit, BusinessUnitDetailsDto>, ISingleResultSpecification<BusinessUnit>
{
    public BusinessUnitByIdSpec(Guid id) =>
        Query
            .Where(e => e.Id == id);
}

public class BusinessUnitByCodeSpec : Specification<BusinessUnit>, ISingleResultSpecification<BusinessUnit>
{
    public BusinessUnitByCodeSpec(string code) =>
        Query
            .Where(e => e.Code == code);
}

public class BusinessUnitByNameSpec : Specification<BusinessUnit>, ISingleResultSpecification<BusinessUnit>
{
    public BusinessUnitByNameSpec(string name) =>
        Query
            .Where(e => e.Name == name);
}