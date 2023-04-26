namespace FSH.WebApi.Application.Price.PricePlans;

public class PricePlanDto : IDto
{
    public DefaultIdType Id { get; set; }

    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }

    public string? Color { get; set; }

    public string PackOfMea { get; set; } = default!; // Đơn vị tính - cái bộ, thùng
    public int PackQty { get; set; } // Quy cách thùng gồm mấy cái

    public decimal UnitPrice { get; set; }
    public decimal PriceVAT { get; set; }
    public decimal ListPrice { get; set; } // Recommended consumer price

    public int Priority { get; set; }
    public DateTime EffectiveDate { get; set; }
    public DateTime? ExpiredDate { get; set; }

    public DateTime? ListingDate { get; set; }
    public DateTime? NewDate { get; set; }
    public DateTime? ActiveDate { get; set; }
    public DateTime? EOLDate { get; set; }
    public DateTime? DisableDate { get; set; }

    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = default!;

    public Guid PriceGroupId { get; set; }
    public string PriceGroupName { get; set; } = default!;
}

public class PricePlanDetailsDto : IDto
{
    public DefaultIdType Id { get; set; }

    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}

public class PricePlanExportDto : IDto
{
    public DefaultIdType Id { get; set; }
    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }

    public string? Color { get; set; }

    public string PackOfMea { get; set; } = default!; // Đơn vị tính - cái bộ, thùng
    public int PackQty { get; set; } // Quy cách thùng gồm mấy cái

    public decimal UnitPrice { get; set; }
    public decimal PriceVAT { get; set; }
    public decimal ListPrice { get; set; } // Recommended consumer price

    public int Priority { get; set; }
    public DateTime EffectiveDate { get; set; }
    public DateTime? ExpiredDate { get; set; }

    public DateTime? ListingDate { get; set; }
    public DateTime? NewDate { get; set; }
    public DateTime? ActiveDate { get; set; }
    public DateTime? EOLDate { get; set; }
    public DateTime? DisableDate { get; set; }

    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = default!;

    public Guid PriceGroupId { get; set; }
    public string PriceGroupName { get; set; } = default!;

    public Guid CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public Guid LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
}