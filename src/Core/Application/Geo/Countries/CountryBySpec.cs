using FSH.WebApi.Domain.Geo;

namespace FSH.WebApi.Application.Geo.Countries;

public class CountryByIdSpec : Specification<Country, CountryDetailsDto>, ISingleResultSpecification<Country>
{
    public CountryByIdSpec(DefaultIdType id) =>
        Query
            .Where(e => e.Id == id);
}

public class CountryByCodeSpec : Specification<Country>, ISingleResultSpecification<Country>
{
    public CountryByCodeSpec(string code) =>
        Query
            .Where(e => e.Code == code);
}

public class CountryByNameSpec : Specification<Country>, ISingleResultSpecification<Country>
{
    public CountryByNameSpec(string name) =>
        Query
            .Where(e => e.Name == name);
}