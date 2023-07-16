using FSH.WebApi.Domain.Game;

namespace FSH.WebApi.Application.Game.VnPowerForcasts;

public class VnPowerForcastByIdSpec : Specification<VnPowerForcast, VnPowerForcastDetailsDto>, ISingleResultSpecification<VnPowerForcast>
{
    public VnPowerForcastByIdSpec(DefaultIdType id) =>
        Query
            .Where(e => e.Id == id);
}