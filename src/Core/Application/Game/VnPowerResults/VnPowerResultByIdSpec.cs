using FSH.WebApi.Domain.Game;

namespace FSH.WebApi.Application.Game.VnPowerResults;

public class VnPowerResultByIdSpec : Specification<VnPowerResult, VnPowerResultDetailsDto>, ISingleResultSpecification<VnPowerResult>
{
    public VnPowerResultByIdSpec(DefaultIdType id) =>
        Query
            .Where(e => e.Id == id);
}