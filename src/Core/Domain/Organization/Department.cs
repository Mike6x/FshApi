namespace FSH.WebApi.Domain.Organization;

public class Department : AuditableEntity, IAggregateRoot
{
    public int Order { get; private set; }
    public string Code { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = string.Empty;

    public bool IsActive { get; private set; }

    public DefaultIdType BusinessUnitId { get; private set; }
    public virtual BusinessUnit BusinessUnit { get; private set; } = default!;

    public Department(int order, string code, string name, string description, bool isActive, DefaultIdType businessUnitId)
    {
        Order = order;
        Code = code;
        Name = name;
        Description = description;
        IsActive = isActive;
        BusinessUnitId = businessUnitId;
    }

    public Department()
        : this(0, string.Empty, string.Empty, string.Empty, true, DefaultIdType.Empty)
    {
    }

    public Department Update(
        int? order,
        string? code,
        string? name,
        string? description,
        bool? isActive,
        DefaultIdType? businessUnitId)
    {
        if (order is not null && order.HasValue && Order != order) Order = order.Value;
        if (code is not null && Code?.Equals(code) is not true) Code = code;
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        if (isActive is not null && !IsActive.Equals(isActive)) IsActive = (bool)isActive;
        if (businessUnitId.HasValue && businessUnitId.Value != DefaultIdType.Empty && !BusinessUnitId.Equals(businessUnitId.Value)) BusinessUnitId = businessUnitId.Value;

        return this;
    }
}
