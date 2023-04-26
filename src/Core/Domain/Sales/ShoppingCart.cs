namespace FSH.WebApi.Domain.Sales;
internal class ShoppingCart : AuditableEntity, IAggregateRoot
{
    public string UserId { get; private set; }
    private readonly List<ShoppingCartItem> _items = new List<ShoppingCartItem>();
    public IReadOnlyCollection<ShoppingCartItem> Items => _items.AsReadOnly();

    public DateTime LastAccessed { get; set; }
    public int TimeToLiveInSeconds { get; set; } = 30; // default

    public ShoppingCart(string userId)
    {
        UserId = userId;
    }

    public void AddItem(DefaultIdType productId, decimal unitPrice, int quantity = 1)
    {
        if (!Items.Any(i => i.ProductId == productId))
        {
            _items.Add(new ShoppingCartItem(productId, unitPrice, quantity));
            return;
        }

        var existingItem = Items.First(i => i.ProductId == productId);
        existingItem.AddQuantity(quantity);
    }

    public void RemoveEmptyItems()
    {
        _items.RemoveAll(i => i.Quantity == 0);
    }

    public decimal Total()
    {
        decimal total = 0m;
        foreach (var item in Items)
        {
            total += item.ListPrice * item.Quantity * (1 - item.Discount);
        }

        return total;
    }

}
