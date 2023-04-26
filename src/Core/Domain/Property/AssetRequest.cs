namespace FSH.WebApi.Domain.Property;

public class AssetRequest : AuditableEntity, IAggregateRoot
{
    public DefaultIdType AssetId { get; set; }
    public DefaultIdType RequestedEmployeeId { get; set; }
    public DefaultIdType ApprovedByEmployeeId { get; set; }
    public string? RequestDetails { get; set; }
    public string? Status { get; set; }
    public DateTime RequestDate { get; set; }
    public DateTime ReceiveDate { get; set; }
    public string? Comment { get; set; }
}
