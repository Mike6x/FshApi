using FSH.WebApi.Domain.Price;

namespace FSH.WebApi.Application.Price.PricePlans;

public class PricePlanByIdWithPriceGroupSpec : Specification<PricePlan, PricePlanDetailsDto>, ISingleResultSpecification<PricePlan>
{
    public PricePlanByIdWithPriceGroupSpec(Guid id) =>
        Query
            .Include(e => e.PriceGroup)
            .Where(e => e.Id == id);
}

public class PricePlanByCodeSpec : Specification<PricePlan>, ISingleResultSpecification<PricePlan>
{
    public PricePlanByCodeSpec(string code) =>
        Query
            .Where(e => e.Code == code);
}

public class PricePlanByNameSpec : Specification<PricePlan>, ISingleResultSpecification<PricePlan>
{
    public PricePlanByNameSpec(string name) =>
        Query
            .Where(e => e.Name == name);
}