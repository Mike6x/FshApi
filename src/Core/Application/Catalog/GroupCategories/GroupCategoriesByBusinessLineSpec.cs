namespace FSH.WebApi.Application.Catalog.GroupCategories;

public class GroupCategoriesByBusinessLineSpec : Specification<GroupCategorie>
{
    public GroupCategoriesByBusinessLineSpec(DefaultIdType fatherId) =>
        Query.Where(e => e.BusinessLineId == fatherId);
}