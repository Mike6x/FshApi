using FSH.WebApi.Domain.Production;

namespace FSH.WebApi.Domain.Price;
public class PricePlan : AuditableEntity, IAggregateRoot
{
    public int Order { get; private set; }
    public string Code { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public bool IsActive { get; private set; }

    public string? Color { get; private set; }

    public string PackOfMea { get; private set; } = default!; // Đơn vị tính - cái bộ, thùng
    public int PackQty { get; private set; } // Quy cách thùng gồm mấy cái

    public decimal UnitPrice { get; private set; }
    public decimal PriceVAT { get; private set; }
    public decimal ListPrice { get; private set; } // Recommended consumer price

    public int Priority { get; private set; }
    public DateTime EffectiveDate { get; private set; }
    public DateTime? ExpiredDate { get; private set; }

    public DateTime? ListingDate { get; private set; }
    public DateTime? NewDate { get; private set; }
    public DateTime? ActiveDate { get; private set; }
    public DateTime? EOLDate { get; private set; }
    public DateTime? DisableDate { get; private set; }

    public Guid ProductId { get; private set; }
    public virtual Product Product { get; private set; } = default!;

    public Guid PriceGroupId { get; private set; }
    public virtual PriceGroup PriceGroup { get; private set; } = default!;

    public PricePlan(
        int order,
        string code,
        string name,
        string? description,
        bool isActive,
        string? color,
        string packOfMea,
        int packQty,
        decimal unitPrice,
        decimal priceVAT,
        decimal listPrice,
        int priority,
        DateTime effectiveDate,
        DateTime? expiredDate,
        DateTime? listingDate,
        DateTime? newDate,
        DateTime? activeDate,
        DateTime? eOLDate,
        DateTime? disableDate,
        DefaultIdType productId,
        DefaultIdType priceGroupId)
    {
        Order = order;
        Code = code;
        Name = name;
        Description = description;
        IsActive = isActive;

        Color = color;

        PackOfMea = packOfMea;
        PackQty = packQty;

        UnitPrice = unitPrice;
        PriceVAT = priceVAT;
        ListPrice = listPrice;

        Priority = priority;
        EffectiveDate = effectiveDate;
        ExpiredDate = expiredDate;

        ListingDate = listingDate;
        NewDate = newDate;
        ActiveDate = activeDate;
        EOLDate = eOLDate;
        DisableDate = disableDate;

        ProductId = productId;
        PriceGroupId = priceGroupId;
    }

    public PricePlan()
        : this(
                0,
                string.Empty,
                string.Empty,
                string.Empty,
                true,
                string.Empty,
                string.Empty,
                1,
                0,
                0,
                0,
                0,
                DateTime.UtcNow,
                null,
                DateTime.UtcNow,
                DateTime.UtcNow,
                DateTime.UtcNow,
                null,
                null,
                Guid.Empty,
                Guid.Empty)
            {
            }

    public PricePlan Update(
        int? order,
        string? code,
        string? name,
        string? description,
        bool? isActive,
        string? color,
        string? packOfMea,
        int? packQty,
        decimal? unitPrice,
        decimal? priceVAT,
        decimal? listPrice,
        int? priority,
        DateTime? effectiveDate,
        DateTime? expiredDate,
        DateTime? listingDate,
        DateTime? newDate,
        DateTime? activeDate,
        DateTime? eOLDate,
        DateTime? disableDate,
        DefaultIdType? productId,
        DefaultIdType? priceGroupId)
    {
        if (order is not null && order.HasValue && Order != order) Order = order.Value;
        if (code is not null && Code?.Equals(code) is not true) Code = code;
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        if (isActive is not null && !IsActive.Equals(isActive)) IsActive = (bool)isActive;

        if (color is not null && Color?.Equals(color) is not true) Color = color;

        if (packOfMea is not null && PackOfMea?.Equals(packOfMea) is not true) PackOfMea = packOfMea;
        if (packQty is not null && packQty.HasValue && PackQty != packQty) PackQty = packQty.Value;

        if (unitPrice.HasValue && UnitPrice != unitPrice) UnitPrice = unitPrice.Value;
        if (priceVAT.HasValue && PriceVAT != priceVAT) PriceVAT = priceVAT.Value;
        if (listPrice.HasValue && ListPrice != listPrice) ListPrice = listPrice.Value;

        if (priority is not null && priority.HasValue && Priority != priority) Priority = priority.Value;
        if (effectiveDate is not null && !EffectiveDate.Equals(effectiveDate)) EffectiveDate = (DateTime)effectiveDate;
        if (expiredDate is not null && !ExpiredDate.Equals(expiredDate)) ExpiredDate = (DateTime)expiredDate;

        if (listingDate is not null && !ListingDate.Equals(listingDate)) ListingDate = (DateTime)listingDate;
        if (newDate is not null && !NewDate.Equals(newDate)) NewDate = (DateTime)newDate;
        if (activeDate is not null && !ActiveDate.Equals(activeDate)) ActiveDate = (DateTime)activeDate;
        if (eOLDate is not null && !EOLDate.Equals(eOLDate)) EOLDate = (DateTime)eOLDate;
        if (disableDate is not null && !DisableDate.Equals(disableDate)) DisableDate = (DateTime)disableDate;

        if (productId.HasValue && productId.Value != DefaultIdType.Empty && !ProductId.Equals(productId.Value)) ProductId = productId.Value;
        if (priceGroupId.HasValue && priceGroupId.Value != DefaultIdType.Empty && !PriceGroupId.Equals(priceGroupId.Value)) PriceGroupId = priceGroupId.Value;

        return this;
    }
}
