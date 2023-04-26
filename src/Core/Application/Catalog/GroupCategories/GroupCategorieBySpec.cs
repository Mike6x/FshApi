namespace FSH.WebApi.Application.Catalog.GroupCategories;

public class GroupCategorieByIdWithBusinessLineSpec : Specification<GroupCategorie, GroupCategorieDetailsDto>, ISingleResultSpecification<GroupCategorie>
{
    public GroupCategorieByIdWithBusinessLineSpec(DefaultIdType id) =>
        Query
            .Include(e => e.BusinessLine)
            .Where(e => e.Id == id);
}

public class GroupCategorieByCodeSpec : Specification<GroupCategorie>, ISingleResultSpecification<GroupCategorie>
{
    public GroupCategorieByCodeSpec(string code) =>
        Query
            .Where(e => e.Code == code);
}

public class GroupCategorieByNameSpec : Specification<GroupCategorie>, ISingleResultSpecification<GroupCategorie>
{
    public GroupCategorieByNameSpec(string name) =>
        Query
            .Where(e => e.Name == name);
}