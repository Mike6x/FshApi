using FSH.WebApi.Domain.Price;

namespace FSH.WebApi.Application.Price.PriceGroups;

public class PriceGroupByIdSpec : Specification<PriceGroup, PriceGroupDetailsDto>, ISingleResultSpecification<PriceGroup>
{
    public PriceGroupByIdSpec(Guid id) =>
        Query
            .Where(e => e.Id == id);
}

public class PriceGroupByCodeSpec : Specification<PriceGroup>, ISingleResultSpecification<PriceGroup>
{
    public PriceGroupByCodeSpec(string code) =>
        Query
            .Where(e => e.Code == code);
}

public class PriceGroupByNameSpec : Specification<PriceGroup>, ISingleResultSpecification<PriceGroup>
{
    public PriceGroupByNameSpec(string name) =>
        Query
            .Where(e => e.Name == name);
}