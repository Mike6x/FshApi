using FSH.WebApi.Domain.Catalog;
using FSH.WebApi.Domain.People;
using FSH.WebApi.Domain.Purchase;
using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Domain.Property;

public class Asset : AuditableEntity, IAggregateRoot
{
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }

    public string? Model { get; private set; }
    public string? Serial { get; private set; }
    public string? ImagePath { get; private set; }
    public string? Barcode { get; private set; }

    public DateTime DateOfPurchase { get; private set; }
    public DateTime? DateOfManufacture { get; private set; }

  // public DateTime? YearOfValuation { get; private set; }
    public int WarrantyInMonth { get; private set; }
    public int DepreciationInMonth { get; private set; }

    public int Quantity { get; private set; } = 1;
    public double UnitPrice { get; private set; } = 0;
    public DefaultIdType VendorId { get; private set; }
    public virtual Vendor Vendor { get; private set; } = default!;

    public DefaultIdType CategorieId { get; private set; }
    public virtual Categorie Categorie { get; private set; } = default!;
    public DefaultIdType? SubCategorieId { get; private set; }
    public virtual SubCategorie? SubCategory { get; private set; }

    public DefaultIdType QualityStatusId { get; private set; }
    public virtual Dimension QualityStatus { get; private set; } = default!;

    public string? Location { get; private set; }
    public DefaultIdType? EmployeeId { get; private set; }
    public virtual Employee? Employee { get; private set; }
    public DefaultIdType UsingStatusId { get; private set; }
    public virtual Dimension UsingStatus { get; private set; } = default!;

    public Asset(
        string code,
        string name,
        string? description,
        string? model,
        string? serial,
        string? imagePath,
        string? barcode,
        DateTime dateOfPurchase,
        DateTime? dateOfManufacture,
        int warrantyInMonth,
        int depreciationInMonth,
        int quantity,
        double unitPrice,
        DefaultIdType vendorId,
        DefaultIdType categorieId,
        DefaultIdType? subCategorieId,
        DefaultIdType qualityStatusId,
        string? location,
        DefaultIdType? employeeId,
        DefaultIdType usingStatusId)
    {
        Code = code;
        Name = name;
        Description = description ?? string.Empty;

        Model = model ?? string.Empty;
        Serial = serial ?? string.Empty;
        ImagePath = imagePath ?? string.Empty;
        Barcode = barcode ?? code;

        DateOfPurchase = (dateOfPurchase == default) ? DateTime.UtcNow : dateOfPurchase;
        DateOfManufacture = dateOfManufacture ?? DateTime.UtcNow;
        WarrantyInMonth = (warrantyInMonth == default) ? 24 : warrantyInMonth;
        DepreciationInMonth = (depreciationInMonth == default) ? 48 : depreciationInMonth;

        Quantity = (quantity == default) ? 1 : quantity;
        UnitPrice = unitPrice;
        VendorId = vendorId;

        CategorieId = categorieId;
        SubCategorieId = (subCategorieId == DefaultIdType.Empty) ? null : subCategorieId;

        QualityStatusId = qualityStatusId;

        Location = location ?? string.Empty;
        EmployeeId = (employeeId == DefaultIdType.Empty) ? null : employeeId;
        UsingStatusId = usingStatusId;
    }

    public Asset()
        : this(
        string.Empty,
        string.Empty,
        string.Empty,
        string.Empty,
        string.Empty,
        string.Empty,
        string.Empty,
        DateTime.UtcNow,
        DateTime.UtcNow,
        24,
        48,
        1,
        1,
        DefaultIdType.Empty,
        DefaultIdType.Empty,
        null,
        DefaultIdType.Empty,
        string.Empty,
        null,
        DefaultIdType.Empty)
    {
    }

    public Asset Update(
        string? code,
        string? name,
        string? description,
        string? model,
        string? serial,
        string? imagePath,
        string? barcode,
        DateTime? dateOfPurchase,
        DateTime? dateOfManufacture,
        int? warrantyInMonth,
        int? depreciationInMonth,
        int? quantity,
        double? unitPrice,
        DefaultIdType? vendorId,
        DefaultIdType?categorieId,
        DefaultIdType? subCategorieId,
        DefaultIdType? qualityStatusId,
        string? location,
        DefaultIdType? employeeId,
        DefaultIdType? usingStatusId)
    {
        if (code is not null && Code?.Equals(code) is not true) Code = code;
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (description is not null && Description?.Equals(description) is not true) Description = description;

        if (model is not null && Model?.Equals(model) is not true) Model = model;
        if (serial is not null && Serial?.Equals(serial) is not true) Serial = model;
        if (imagePath is not null && ImagePath?.Equals(imagePath) is not true) ImagePath = imagePath;
        if (barcode is not null && Barcode?.Equals(barcode) is not true) Barcode = barcode;

        if (dateOfPurchase is not null && !DateOfPurchase.Equals(dateOfPurchase)) DateOfPurchase = (DateTime)dateOfPurchase;
        if (dateOfManufacture is not null && !DateOfManufacture.Equals(dateOfManufacture)) DateOfManufacture = (DateTime)dateOfManufacture;
        if (warrantyInMonth is not null && warrantyInMonth.HasValue && WarrantyInMonth != warrantyInMonth) WarrantyInMonth = warrantyInMonth.Value;
        if (depreciationInMonth is not null && depreciationInMonth.HasValue && DepreciationInMonth != depreciationInMonth) DepreciationInMonth = depreciationInMonth.Value;

        if (quantity is not null && quantity.HasValue && Quantity != quantity) Quantity = quantity.Value;
        if (unitPrice is not null && unitPrice.HasValue && UnitPrice != unitPrice) UnitPrice = unitPrice.Value;

        if (vendorId.HasValue && vendorId.Value != DefaultIdType.Empty && !VendorId.Equals(vendorId.Value)) VendorId = vendorId.Value;
        if (categorieId.HasValue && categorieId.Value != DefaultIdType.Empty && !CategorieId.Equals(categorieId.Value)) CategorieId = categorieId.Value;
        if (subCategorieId.HasValue && subCategorieId.Value != DefaultIdType.Empty && !SubCategorieId.Equals(subCategorieId.Value)) SubCategorieId = subCategorieId.Value;

        if (qualityStatusId.HasValue && qualityStatusId.Value != DefaultIdType.Empty && !QualityStatusId.Equals(qualityStatusId.Value)) QualityStatusId = qualityStatusId.Value;

        if (location is not null && Location?.Equals(location) is not true) Location = location;
        if (employeeId.HasValue && employeeId.Value != DefaultIdType.Empty && !EmployeeId.Equals(employeeId.Value)) EmployeeId = employeeId.Value;
        if (usingStatusId.HasValue && usingStatusId.Value != DefaultIdType.Empty && !UsingStatusId.Equals(usingStatusId.Value)) UsingStatusId = usingStatusId.Value;

        return this;
    }

    public Asset ClearImagePath()
    {
        ImagePath = string.Empty;
        return this;
    }
}