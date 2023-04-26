using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Application.Settings.Menus;

public class MenuByIdSpecification : Specification<Menu, MenuDetailsDto>, ISingleResultSpecification<Menu>
{
    public MenuByIdSpecification(DefaultIdType id) =>
        Query
            .Where(e => e.Id == id);
}

public class MenuByCodeSpec : Specification<Menu>, ISingleResultSpecification<Menu>
{
    public MenuByCodeSpec(string code) =>
        Query.Where(e => e.Code == code);
}

public class MenuByNameSpec : Specification<Menu>, ISingleResultSpecification<Menu>
{
    public MenuByNameSpec(string name) =>
        Query.Where(e => e.Name == name);
}