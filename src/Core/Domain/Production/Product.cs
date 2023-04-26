using FSH.WebApi.Domain.Catalog;
using FSH.WebApi.Domain.Purchase;

namespace FSH.WebApi.Domain.Production;

public class Product : AuditableEntity, IAggregateRoot
{
    public int Order { get; private set; }
    public string Code { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public bool IsActive { get; private set; }

    public decimal ListPrice { get; private set; }
    public string? ImagePath { get; private set; }

    // public string ImageThumbnailUrl { get; set; }
    // public bool IsProductOfTheWeek { get; set; }
    // public bool InStock { get; set; }

    public decimal Weight { get; private set; }
    public decimal Length { get; private set; }
    public decimal Width { get; private set; }
    public decimal Height { get; private set; }

    public DefaultIdType BrandId { get; private set; }
    public virtual Brand Brand { get; private set; } = default!;

    public DefaultIdType CategorieId { get; private set; }
    public virtual Categorie Categorie { get; private set; } = default!;
    public DefaultIdType? SubCategorieId { get; private set; }
    public virtual SubCategorie? SubCategorie { get; private set; }

    public DefaultIdType? VendorId { get; private set; }
    public virtual Vendor? Vendor { get; private set; }

    // public virtual ICollection<OrderItem> OrderItems { get; set; }
    // public virtual ICollection<Stock> Stocks { get; set; }

    public Product(
        int order,
        string code,
        string name,
        string? description,
        bool isActive,
        decimal listPrice,
        string? imagePath,
        decimal weight,
        decimal length,
        decimal width,
        decimal height,
        DefaultIdType brandId,
        DefaultIdType categorieId,
        DefaultIdType? subCategorieId,
        DefaultIdType? vendorId)
    {
        Order = order;
        Code = code;
        Name = name;
        Description = description;
        IsActive = isActive;

        ListPrice = listPrice;
        ImagePath = imagePath;

        Weight = weight;
        Length = length;
        Width = width;
        Height = height;

        BrandId = brandId;
        CategorieId = categorieId;
        SubCategorieId = (subCategorieId == DefaultIdType.Empty) ? null : subCategorieId;
        VendorId = (vendorId == DefaultIdType.Empty) ? null : vendorId;
    }

    public Product()
        : this(
                  0,
                  string.Empty,
                  string.Empty,
                  string.Empty,
                  true,
                  0,
                  string.Empty,
                  0,
                  0,
                  0,
                  0,
                  DefaultIdType.Empty,
                  DefaultIdType.Empty,
                  null,
                  null)
    {
        // Only needed for working with dapper (See GetProductViaDapperRequest)
        // If you're not using dapper it's better to remove this constructor.
    }

    public Product Update(
        int? order,
        string? code,
        string? name,
        string? description,
        bool? isActive,
        decimal? listPrice,
        string? imagePath,
        decimal? weight,
        decimal? length,
        decimal? width,
        decimal? height,
        DefaultIdType? brandId,
        DefaultIdType? categorieId,
        DefaultIdType? subCategorieId,
        DefaultIdType? vendorId)
    {
        if (order is not null && order.HasValue && Order != order) Order = order.Value;
        if (code is not null && Code?.Equals(code) is not true) Code = code;
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        if (isActive is not null && !IsActive.Equals(isActive)) IsActive = (bool)isActive;

        if (listPrice.HasValue && ListPrice != listPrice) ListPrice = listPrice.Value;
        if (imagePath is not null && ImagePath?.Equals(imagePath) is not true) ImagePath = imagePath;

        if (weight.HasValue && Weight != weight) Weight = weight.Value;
        if (length.HasValue && Length != length) Length = length.Value;
        if (width.HasValue && Width != width) Width = width.Value;
        if (height.HasValue && Height != height) Height = height.Value;

        if (brandId.HasValue && brandId.Value != DefaultIdType.Empty && !BrandId.Equals(brandId.Value)) BrandId = brandId.Value;
        if (categorieId.HasValue && categorieId.Value != DefaultIdType.Empty && !CategorieId.Equals(categorieId.Value)) CategorieId = categorieId.Value;
        if (subCategorieId.HasValue && subCategorieId.Value != DefaultIdType.Empty && !SubCategorieId.Equals(subCategorieId.Value)) SubCategorieId = subCategorieId.Value;
        if (vendorId.HasValue && vendorId.Value != DefaultIdType.Empty && !VendorId.Equals(vendorId.Value)) VendorId = vendorId.Value;

        return this;
    }

    public Product ClearImagePath()
    {
        ImagePath = string.Empty;
        return this;
    }
}

// public class Product : AuditableEntity, IAggregateRoot
// {
//    public string Name { get; private set; } = default!;
//    public string? Description { get; private set; }
//    public decimal Rate { get; private set; }
//    public string? ImagePath { get; private set; }
//    public DefaultIdType BrandId { get; private set; }
//    public virtual Brand Brand { get; private set; } = default!;
//    public Product()
//    {
//        // Only needed for working with dapper (See GetProductViaDapperRequest)
//        // If you're not using dapper it's better to remove this constructor.
//    }
//    public Product(string name, string? description, decimal rate, DefaultIdType brandId, string? imagePath)
//    {
//        Name = name;
//        Description = description;
//        Rate = rate;
//        ImagePath = imagePath;
//        BrandId = brandId;
//    }
//    public Product Update(string? name, string? description, decimal? rate, DefaultIdType? brandId, string? imagePath)
//    {
//        if (name is not null && Name?.Equals(name) is not true) Name = name;
//        if (description is not null && Description?.Equals(description) is not true) Description = description;
//        if (rate.HasValue && Rate != rate) Rate = rate.Value;
//        if (brandId.HasValue && brandId.Value != DefaultIdType.Empty && !BrandId.Equals(brandId.Value)) BrandId = brandId.Value;
//        if (imagePath is not null && ImagePath?.Equals(imagePath) is not true) ImagePath = imagePath;
//        return this;
//    }
//    public Product ClearImagePath()
//    {
//        ImagePath = string.Empty;
//        return this;
//    }
// }