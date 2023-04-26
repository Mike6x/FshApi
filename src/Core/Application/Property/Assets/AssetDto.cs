namespace FSH.WebApi.Application.Property.Assets;

public class AssetDto : IDto
{
    public DefaultIdType Id { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }

    public string? Model { get; set; }
    public string? Serial { get; set; }
    public string? ImagePath { get; set; }
    public string? Barcode { get; set; }

    public DateTime DateOfPurchase { get; set; }
    public DateTime? DateOfManufacture { get; set; }

    // public DateTime? YearOfValuation { get; set; }
    public int WarrantyInMonth { get; set; }
    public int DepreciationInMonth { get; set; }

    public int Quantity { get; set; } = 1;
    public double UnitPrice { get; set; } = 0;

    public DefaultIdType VendorId { get; set; }
    public string VendorName { get; set; } = default!;

    public DefaultIdType CategorieId { get; set; }
    public string CategorieName { get; set; } = default!;

    public DefaultIdType? SubCategorieId { get; set; }
    public string? SubCategoryName { get; set; }

    public DefaultIdType QualityStatusId { get; set; }
    public string QualityStatusName { get; set; } = default!;

    public string? Location { get; set; }
    public DefaultIdType? EmployeeId { get; set; }
    public string? EmployeeFirstName { get; set; }
    public string? EmployeeLastName { get; set; }

    public DefaultIdType UsingStatusId { get; set; }
    public string UsingStatusName { get; set; } = default!;
}

public class AssetDetailsDto : IDto
{
    public DefaultIdType Id { get; set; }

    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}

public class AssetExportDto : IDto
{
    public DefaultIdType Id { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }

    public string? Model { get; set; }
    public string? Serial { get; set; }
    public string? ImagePath { get; set; }
    public string? Barcode { get; set; }

    public DateTime DateOfPurchase { get; set; }
    public DateTime? DateOfManufacture { get; set; }

    // public DateTime? YearOfValuation { get; set; }
    public int WarrantyInMonth { get; set; }
    public int DepreciationInMonth { get; set; }

    public int Quantity { get; set; } = 1;
    public double UnitPrice { get; set; } = 0;

    public DefaultIdType VendorId { get; set; }
    public string VendorName { get; set; } = default!;

    public DefaultIdType CategorieId { get; set; }
    public string CategorieName { get; set; } = default!;

    public DefaultIdType? SubCategorieId { get; set; }
    public string? SubCategoryName { get; set; }

    public DefaultIdType QualityStatusId { get; set; }
    public string QualityStatusName { get; set; } = default!;

    public string? Location { get; set; }
    public DefaultIdType? EmployeeId { get; set; }
    public string? EmployeeFirstName { get; set; }
    public string? EmployeeLastName { get; set; }
    public DefaultIdType UsingStatusId { get; set; }
    public string UsingStatusName { get; set; } = default!;

    public DefaultIdType CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public DefaultIdType LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
}