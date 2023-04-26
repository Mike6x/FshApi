using FSH.WebApi.Domain.Production;

namespace FSH.WebApi.Domain.Sales;
public class OrderItem : AuditableEntity, IAggregateRoot
{
    // public int ItemId { get; private set; }

    public int Quantity { get; private set; }
    public decimal ListPrice { get; private set; }
    public decimal Discount { get; private set; }
    public DefaultIdType ProductId { get; private set; }
    public virtual Product Product { get; private set; } = default!;
    public DefaultIdType OrderId { get; private set; }
    public virtual Order Order { get; private set; } = default!;
}
