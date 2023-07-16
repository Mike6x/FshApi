using FSH.WebApi.Domain.Game;

namespace FSH.WebApi.Application.Game.VnPowers;

public class VnPowerByIdSpec : Specification<VnPower, VnPowerDetailsDto>, ISingleResultSpecification<VnPower>
{
    public VnPowerByIdSpec(DefaultIdType id) =>
        Query
            .Where(e => e.Id == id);
}

public class VnPowerByCodeSpec : Specification<VnPower>, ISingleResultSpecification<VnPower>
{
    public VnPowerByCodeSpec(int code) =>
        Query
            .Where(e => e.DrawId == code);
}

// public class VnPowerByNameSpec : Specification<VnPower>, ISingleResultSpecification<VnPower>
// {
//    public VnPowerByNameSpec(string name) =>
//        Query
//            .Where(e => e.Name == name);
// }