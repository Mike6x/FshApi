namespace FSH.WebApi.Application.Catalog.Categories;

public class CategoriesByGroupCategorieSpec : Specification<Categorie>
{
    public CategoriesByGroupCategorieSpec(DefaultIdType fatherId) =>
        Query.Where(e => e.GroupCategorieId == fatherId);
}