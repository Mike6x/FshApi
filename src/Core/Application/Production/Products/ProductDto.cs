using FSH.WebApi.Application.Catalog.Brands;

namespace FSH.WebApi.Application.Production.Products;

public class ProductDto : IDto
{
    public DefaultIdType Id { get; set; }
    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }

    public decimal ListPrice { get; set; }
    public string? ImagePath { get; set; }

    public decimal Weight { get; set; }
    public decimal Length { get; set; }
    public decimal Width { get; set; }
    public decimal Height { get; set; }

    public DefaultIdType BrandId { get; set; }
    public string BrandName { get; set; } = default!;
    public DefaultIdType CategorieId { get; set; }
    public string CategorieName { get; set; } = default!;
    public DefaultIdType? SubCategorieId { get; set; }
    public string? SubCategorieName { get; set; }
    public DefaultIdType? VendorId { get; set; }
    public string? VendorName { get; set; }
}

public class ProductDetailsDto : IDto
{
    public DefaultIdType Id { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }

    public string? ImagePath { get; set; }
    public BrandDto Brand { get; set; } = default!;
}

public class ProductExportDto : IDto
{
    public DefaultIdType Id { get; set; }
    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }

    public decimal ListPrice { get; set; }
    public string? ImagePath { get; set; }

    public decimal Weight { get; set; }
    public decimal Length { get; set; }
    public decimal Width { get; set; }
    public decimal Height { get; set; }

    public DefaultIdType BrandId { get; set; }
    public string BrandName { get; set; } = default!;
    public DefaultIdType CategorieId { get; set; }
    public string CategorieName { get; set; } = default!;
    public DefaultIdType? SubCategorieId { get; set; }
    public string? SubCategorieName { get; set; }
    public DefaultIdType? VendorId { get; set; }
    public string? VendorName { get; set; }

    public DefaultIdType CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public DefaultIdType LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
}