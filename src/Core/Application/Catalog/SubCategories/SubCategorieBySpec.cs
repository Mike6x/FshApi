using FSH.WebApi.Domain.Organization;

namespace FSH.WebApi.Application.Catalog.SubCategories;

public class SubCategorieByIdWithCategorieSpec : Specification<SubCategorie, SubCategorieDetailsDto>, ISingleResultSpecification<SubCategorie>
{
    public SubCategorieByIdWithCategorieSpec(DefaultIdType id) =>
        Query
            .Include(e => e.Categorie)
            .Where(e => e.Id == id);
}

public class SubCategorieByCodeSpec : Specification<SubCategorie>, ISingleResultSpecification<SubCategorie>
{
    public SubCategorieByCodeSpec(string code) =>
        Query
            .Where(e => e.Code == code);
}

public class SubCategorieByNameSpec : Specification<SubCategorie>, ISingleResultSpecification<SubCategorie>
{
    public SubCategorieByNameSpec(string name) =>
        Query
            .Where(e => e.Name == name);
}