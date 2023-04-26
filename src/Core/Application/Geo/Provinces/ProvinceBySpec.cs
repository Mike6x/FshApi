using FSH.WebApi.Domain.Geo;

namespace FSH.WebApi.Application.Geo.Provinces;

public class ProvinceByIdSpec : Specification<Province, ProvinceDetailsDto>, ISingleResultSpecification<Province>
{
    public ProvinceByIdSpec(DefaultIdType id) =>
        Query
            .Where(e => e.Id == id);
}

public class ProvinceByCodeSpec : Specification<Province>, ISingleResultSpecification<Province>
{
    public ProvinceByCodeSpec(string code) =>
        Query
            .Where(e => e.Code == code);
}

public class ProvinceByNameSpec : Specification<Province>, ISingleResultSpecification<Province>
{
    public ProvinceByNameSpec(string name) =>
        Query
            .Where(e => e.Name == name);
}