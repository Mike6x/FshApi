using FSH.WebApi.Domain.Production;

namespace FSH.WebApi.Application.Production.Products;

public class ProductByIdSpec : Specification<Product, ProductDetailsDto>, ISingleResultSpecification<Product>
{
    public ProductByIdSpec(DefaultIdType id) =>
        Query
            .Where(e => e.Id == id);
}

public class ProductByCodeSpec : Specification<Product>, ISingleResultSpecification<Product>
{
    public ProductByCodeSpec(string code) =>
        Query
            .Where(e => e.Code == code);
}

public class ProductByNameSpec : Specification<Product>, ISingleResultSpecification<Product>
{
    public ProductByNameSpec(string name) =>
        Query.Where(p => p.Name == name);
}

public class ProductByIdWithBrandSpec : Specification<Product, ProductDetailsDto>, ISingleResultSpecification<Product>
{
    public ProductByIdWithBrandSpec(DefaultIdType id) =>
        Query
            .Where(p => p.Id == id)
            .Include(p => p.Brand);
}