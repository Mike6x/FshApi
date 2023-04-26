using FSH.WebApi.Domain.People;

namespace FSH.WebApi.Application.People.Titles;

public class TitleByIdSpec : Specification<Title, TitleDetailsDto>, ISingleResultSpecification<Title>
{
    public TitleByIdSpec(DefaultIdType id) =>
        Query
            .Where(e => e.Id == id);
}

public class TitleByCodeSpec : Specification<Title>, ISingleResultSpecification<Title>
{
    public TitleByCodeSpec(string code) =>
        Query
            .Where(e => e.Code == code);
}

public class TitleByNameSpec : Specification<Title>, ISingleResultSpecification<Title>
{
    public TitleByNameSpec(string name) =>
        Query
            .Where(e => e.Name == name);
}