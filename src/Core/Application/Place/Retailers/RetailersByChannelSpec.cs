using FSH.WebApi.Domain.Place;

namespace FSH.WebApi.Application.Place.Retailers;

public class RetailersByChannelSpec : Specification<Retailer>
{
    public RetailersByChannelSpec(Guid fatherId) =>
        Query.Where(e => e.ChannelId == fatherId);
}