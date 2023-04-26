using FSH.WebApi.Domain.Place;

namespace FSH.WebApi.Domain.Sales;
public class Order : AuditableEntity, IAggregateRoot
{
    public string Code { get; set; }
    public byte Status { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime RequiredDate { get; set; }
    public DateTime? ShippedDate { get; set; }

    public DefaultIdType? CustomerId { get; set; }
    public DefaultIdType? StoreId { get; set; }
    public DefaultIdType? StaffId { get; set; }

    public virtual Customer Customer { get; set; } = default!;
    public virtual Staff Staff { get; set; } = default!;
    public virtual Store Store { get; set; } = default!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = default!;

    public Order(string code, byte status, DateTime? orderDate, DateTime? requiredDate, DateTime? shippedDate, DefaultIdType? customerId, DefaultIdType? staffId, DefaultIdType? storeId )
    {
        Code = code;
        Status = status;
        OrderDate = orderDate ?? DateTime.Today;
        RequiredDate = requiredDate ?? DateTime.Today;
        ShippedDate = shippedDate ?? DateTime.Today;

        CustomerId = customerId;
        StaffId = staffId;
        StoreId = storeId;
    }

    public Order()
        : this(string.Empty, 0, DateTime.Today, DateTime.Today, DateTime.Today, null, null, null)
        {
            OrderItems = new HashSet<OrderItem>();
        }

    public Order Update(string? code, byte? status, DateTime? orderDate, DateTime? requiredDate, DateTime? shippedDate, DefaultIdType? customerId, DefaultIdType? staffId, DefaultIdType? storeId)
    {
        if (code is not null && Code?.Equals(code) is not true) Code = code;
        if (status is not null && !Status.Equals(status)) Status = (byte)status;
        if (orderDate.HasValue && orderDate.Value != DateTime.MinValue && !OrderDate.Equals(orderDate.Value)) OrderDate = orderDate.Value;
        if (requiredDate.HasValue && requiredDate.Value != DateTime.MinValue && !RequiredDate.Equals(requiredDate.Value)) RequiredDate = requiredDate.Value;
        if (shippedDate.HasValue && shippedDate.Value != DateTime.MinValue && !ShippedDate.Equals(shippedDate.Value)) ShippedDate = shippedDate.Value;

        if (customerId.HasValue && customerId.Value != DefaultIdType.Empty && !CustomerId.Equals(customerId.Value)) CustomerId = customerId.Value;
        if (staffId.HasValue && staffId.Value != DefaultIdType.Empty && !StaffId.Equals(staffId.Value)) StaffId = staffId.Value;
        if (storeId.HasValue && storeId.Value != DefaultIdType.Empty && !StoreId.Equals(storeId.Value)) StoreId = storeId.Value;

        return this;
    }

    public decimal Total()
    {
        decimal total = 0m;
        foreach (var item in OrderItems)
        {
            total += item.ListPrice * item.Quantity * (1 - item.Discount);
        }

        return total;
    }
}
