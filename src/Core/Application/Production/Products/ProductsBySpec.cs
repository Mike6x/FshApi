using FSH.WebApi.Domain.Production;

namespace FSH.WebApi.Application.Production.Products;
public class ProductsByBrandSpec : Specification<Product>
{
    public ProductsByBrandSpec(DefaultIdType brandId) =>
        Query.Where(p => p.BrandId == brandId);
}

public class ProductsByCategorySpec : Specification<Product>
{
    public ProductsByCategorySpec(DefaultIdType categorieId) =>

     Query.Where(e => e.CategorieId == categorieId);
}

public class ProductsBySubCategorySpec : Specification<Product>
{
    public ProductsBySubCategorySpec(DefaultIdType subCategorieId) =>

     Query.Where(e => e.SubCategorieId == subCategorieId);
}

public class ProductsByVenderSpec : Specification<Product>
{
    public ProductsByVenderSpec(DefaultIdType vendorId) =>

     Query.Where(e => e.VendorId == vendorId);
}