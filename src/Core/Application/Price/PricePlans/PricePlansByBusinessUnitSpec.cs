using FSH.WebApi.Domain.Price;

namespace FSH.WebApi.Application.Price.PricePlans;

public class PricePlansByPriceGroupSpec : Specification<PricePlan>
{
    public PricePlansByPriceGroupSpec(Guid fatherId) =>
        Query.Where(e => e.PriceGroupId == fatherId);
}