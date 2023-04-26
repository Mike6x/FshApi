namespace FSH.WebApi.Application.Catalog.SubCategories;

public class SubCategoriesByCategorieSpec : Specification<SubCategorie>
{
    public SubCategoriesByCategorieSpec(DefaultIdType fatherId) =>
        Query.Where(e => e.CategorieId == fatherId);
}