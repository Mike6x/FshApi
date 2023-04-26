namespace FSH.WebApi.Domain.Property;

public class AssetIssue : AuditableEntity, IAggregateRoot
{
    public DefaultIdType AssetId { get; set; }
    public DefaultIdType RaisedByEmployeeId { get; set; }
    public string? IssueDescription { get; set; }
    public string? Status { get; set; }
    public DateTime ExpectedFixDate { get; set; }
    public DateTime ResolvedDate { get; set; }
    public string? Comment { get; set; }
}
