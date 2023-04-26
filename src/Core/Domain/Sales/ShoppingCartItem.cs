using FSH.WebApi.Domain.Production;

namespace FSH.WebApi.Domain.Sales;
public class ShoppingCartItem : AuditableEntity, IAggregateRoot
{
    public int Quantity { get; private set; }
    public decimal ListPrice { get; private set; }
    public decimal Discount { get; private set; }
    public DefaultIdType ProductId { get; private set; }
    public virtual Product Product { get; private set; } = default!;

    public DefaultIdType ShoppingCartId { get; private set; }
    public virtual Order ShoppingCart { get; private set; } = default!;

    public ShoppingCartItem(DefaultIdType productId, decimal listPrice, int quantity)
    {
        ProductId = productId;
        ListPrice = listPrice;
        SetQuantity(quantity);
    }

    public void AddQuantity(int quantity)
    {
       // Guard.Against.OutOfRange(quantity, nameof(quantity), 0, int.MaxValue);

        Quantity += quantity;
    }

    public void SetQuantity(int quantity)
    {
       // Guard.Against.OutOfRange(quantity, nameof(quantity), 0, int.MaxValue);

        Quantity = quantity;
    }
}
