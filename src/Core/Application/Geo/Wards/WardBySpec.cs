using FSH.WebApi.Domain.Geo;

namespace FSH.WebApi.Application.Geo.Wards;

public class WardByIdSpec : Specification<Ward, WardDetailsDto>, ISingleResultSpecification<Ward>
{
    public WardByIdSpec(DefaultIdType id) =>
        Query
            .Where(e => e.Id == id);
}

public class WardByCodeSpec : Specification<Ward>, ISingleResultSpecification<Ward>
{
    public WardByCodeSpec(string code) =>
        Query
            .Where(e => e.Code == code);
}

public class WardByNameSpec : Specification<Ward>, ISingleResultSpecification<Ward>
{
    public WardByNameSpec(string name) =>
        Query
            .Where(e => e.Name == name);
}