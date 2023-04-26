using FSH.WebApi.Domain.Place;

namespace FSH.WebApi.Application.Place.Retailers;

public class RetailerByIdWithChannelSpec : Specification<Retailer, RetailerDetailsDto>, ISingleResultSpecification<Retailer>
{
    public RetailerByIdWithChannelSpec(Guid id) =>
        Query
            .Include(e => e.Channel)
            .Where(e => e.Id == id);
}

public class RetailerByCodeSpec : Specification<Retailer>, ISingleResultSpecification<Retailer>
{
    public RetailerByCodeSpec(string code) =>
        Query
            .Where(e => e.Code == code);
}

public class RetailerByNameSpec : Specification<Retailer>, ISingleResultSpecification<Retailer>
{
    public RetailerByNameSpec(string name) =>
        Query
            .Where(e => e.Name == name);
}