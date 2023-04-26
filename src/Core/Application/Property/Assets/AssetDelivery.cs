namespace FSH.WebApi.Application.Property.Assets;
public class AssetDelivery
{
    public string Code { get; set; } = default!;
    public string? Description { get; set; }
    public int Quantity { get; set; } = 1;
}
