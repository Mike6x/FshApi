namespace FSH.WebApi.Domain.Catalog;

public class Brand : AuditableEntity, IAggregateRoot
{
    public int Order { get; private set; }
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public bool IsActive { get; private set; }

    public Brand(int order, string code, string name, string? description, bool isActive)
    {
        Order = order;
        Code = code;
        Name = name;
        Description = description;
        IsActive = isActive;
    }

    public Brand()
    : this(0, string.Empty, string.Empty, string.Empty, true)
    {
    }

    public Brand Update(int? order, string? code, string? name, string? description, bool? isActive)
    {
        if (order is not null && order.HasValue && Order != order) Order = order.Value;

        if (code is not null && Code?.Equals(code) is not true) Code = code;
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        if (isActive is not null && !IsActive.Equals(isActive)) IsActive = (bool)isActive;

        return this;
    }
}