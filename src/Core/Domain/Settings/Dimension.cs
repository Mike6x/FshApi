namespace FSH.WebApi.Domain.Settings;
public class Dimension : AuditableEntity, IAggregateRoot
{
    public int Order { get; private set; }
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public bool IsActive { get; set; }

    public string? FullName { get; private set; }
    public string? NativeName { get; private set; }
    public string? FullNativeName { get; private set; }

    public int Value { get; set; }
    public string Type { get; set; } = default!;

    public DefaultIdType? FatherId { get; private set; }
    public virtual Dimension? Father { get; private set; }
    public virtual ICollection<Dimension> InverseFather { get; private set; } = default!;

    public Dimension(
        int order,
        string code,
        string name,
        string? description,
        bool? isActive,
        string? fullname,
        string? nativeName,
        string? fullNativeName,
        int value,
        string? type,
        DefaultIdType? fatherId)
    {
        Order = order;
        Code = code;
        Name = name;
        Description = description ?? string.Empty;
        IsActive = isActive ?? true;
        FullName = fullname;
        NativeName = nativeName;
        FullNativeName = fullNativeName;
        Type = type ?? string.Empty;
        Value = value;
        FatherId = fatherId;
    }

    public Dimension()
    : this(0, string.Empty, string.Empty, string.Empty, true, null, null, null, 0, string.Empty, null)
    {
    }

    public Dimension Update(
        int? order,
        string? code,
        string? name,
        string? description,
        bool? isActive,
        string? fullName,
        string? nativeName,
        string? fullNativeName,
        int? value,
        string? type,
        DefaultIdType? fatherId)
    {
                if (order is not null && Order != order) Order = order.Value;
                if (code is not null && Code?.Equals(code) is not true) Code = code;
                if (name is not null && Name?.Equals(name) is not true) Name = name;
                if (description is not null && Description?.Equals(description) is not true) Description = description;
                if (isActive is not null && !IsActive.Equals(isActive)) IsActive = (bool)isActive;

                if (fullName is not null && FullName?.Equals(fullName) is not true) FullName = fullName;
                if (nativeName is not null && NativeName?.Equals(nativeName) is not true) NativeName = nativeName;
                if (fullNativeName is not null && FullNativeName?.Equals(fullNativeName) is not true) FullNativeName = fullNativeName;

                if (value is not null && value.HasValue && Value != value) Value = value.Value;
                if (type is not null && Type?.Equals(type) is not true) Type = type;
                if (fatherId == DefaultIdType.Empty || fatherId == null)
                {
                    FatherId = null;
                }
                else if (!FatherId.Equals(fatherId.Value))
                {
                    FatherId = fatherId.Value;
                }

                return this;
            }
}
