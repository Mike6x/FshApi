namespace FSH.WebApi.Application.Catalog.Categories;

public class CategorieByIdIdWithGroupCategorieSpec : Specification<Categorie, CategorieDetailsDto>, ISingleResultSpecification<Categorie>
{
    public CategorieByIdIdWithGroupCategorieSpec(DefaultIdType id) =>
        Query
            .Include(e => e.GroupCategorie)
            .Where(e => e.Id == id);
}

public class CategorieByCodeSpec : Specification<Categorie>, ISingleResultSpecification<Categorie>
{
    public CategorieByCodeSpec(string code) =>
        Query
            .Where(e => e.Code == code);
}

public class CategorieByNameSpec : Specification<Categorie>, ISingleResultSpecification<Categorie>
{
    public CategorieByNameSpec(string name) =>
        Query
            .Where(e => e.Name == name);
}