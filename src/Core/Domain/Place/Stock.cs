using FSH.WebApi.Domain.Production;

namespace FSH.WebApi.Domain.Place;
public class Stock : AuditableEntity, IAggregateRoot
{
    public DefaultIdType StoreId { get; private set; }
    public virtual Store Store { get; private set; } = default!;
    public DefaultIdType ProductId { get; private set; }
    public virtual Product Product { get; private set; } = default!;
    public int Quantity { get; private set; }

    public Stock(DefaultIdType storeId, DefaultIdType productId, int quantity)
    {
        StoreId = storeId;
        ProductId = productId;
        Quantity = quantity;
    }

    public Stock()
        : this(DefaultIdType.Empty, DefaultIdType.Empty, 0)
    {
    }

    public Stock Update(DefaultIdType? storeId, DefaultIdType? productId, int? quantity)
    {
        if (storeId.HasValue && storeId.Value != DefaultIdType.Empty && !StoreId.Equals(storeId.Value)) StoreId = storeId.Value;
        if (productId.HasValue && productId.Value != DefaultIdType.Empty && !ProductId.Equals(productId.Value)) ProductId = productId.Value;
        if (quantity is not null && Quantity != quantity) Quantity = quantity.Value;

        return this;
    }

}
