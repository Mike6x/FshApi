using FSH.WebApi.Domain.Place;

namespace FSH.WebApi.Application.Place.Stores;

public class StoresByRetailerSpec : Specification<Store>
{
    public StoresByRetailerSpec(Guid fatherId) =>
        Query.Where(e => e.RetailerId == fatherId);
}