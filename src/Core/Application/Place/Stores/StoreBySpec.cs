using FSH.WebApi.Domain.Place;

namespace FSH.WebApi.Application.Place.Stores;

public class StoreByIdWithRetailerSpec : Specification<Store, StoreDetailsDto>, ISingleResultSpecification<Store>
{
    public StoreByIdWithRetailerSpec(Guid id) =>
        Query
            .Include(e => e.Retailer)
            .Where(e => e.Id == id);
}

public class StoreByCodeSpec : Specification<Store>, ISingleResultSpecification<Store>
{
    public StoreByCodeSpec(string code) =>
        Query
            .Where(e => e.Code == code);
}

public class StoreByNameSpec : Specification<Store>, ISingleResultSpecification<Store>
{
    public StoreByNameSpec(string name) =>
        Query
            .Where(e => e.Name == name);
}