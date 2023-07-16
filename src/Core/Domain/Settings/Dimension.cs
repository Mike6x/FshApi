namespace FSH.WebApi.Domain.Settings;
public class Dimension : AuditableEntity, IAggregateRoot
{
    public int Order { get; private set; }
    public string Code { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public bool IsActive { get; set; }

    public string? FullName { get; private set; }
    public string? NativeName { get; private set; }
    public string? FullNativeName { get; private set; }

    public string Type { get; private set; } = default!;
    public string Value { get; private set; } = default!;
    public int? NumericValue { get; private set; }

    public DefaultIdType? FatherId { get; private set; }
    public virtual Dimension? Father { get; private set; }
    public virtual ICollection<Dimension> InverseFather { get; private set; } = default!;
}
