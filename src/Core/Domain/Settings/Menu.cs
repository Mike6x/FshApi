namespace FSH.WebApi.Domain.Settings;

public class Menu(
    int order,
    string code,
    string name,
    string? description,
    bool? isActive,
    string href,
    string? icon,
    int parent) : AuditableEntity, IAggregateRoot
{
    public int Order { get; private set; } = order;
    public string Code { get; private set; } = code;
    public string Name { get; private set; } = name;
    public string? Description { get; private set; } = description ?? string.Empty;
    public bool IsActive { get; private set; } = isActive ?? true;

    public string Href { get; private set; } = href;
    public string? Icon { get; private set; } = icon;
    public int Parent { get; private set; } = parent;

    public Menu()
    : this(0, string.Empty, string.Empty, string.Empty, true, string.Empty, string.Empty, 0)
    {
    }

    public Menu Update(int? order, string? code, string? name, string? description, bool? isActive, string? href, string? icon, int? parent)
    {
        // if (order.HasValue && !Order.Equals(order)) Order = (int)order;
        if (order is not null && order.HasValue && Order != order) Order = order.Value;

        // if (code.HasValue && !Code.Equals(code)) Code = (int)code;
        if (code is not null && Code?.Equals(code) is not true) Code = code;
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        if (isActive is not null && !IsActive.Equals(isActive)) IsActive = (bool)isActive;

        if (href is not null && Href?.Equals(href) is not true) Href = href;
        if (icon is not null && Icon?.Equals(icon) is not true) Icon = icon;
        if (parent.HasValue && !Parent.Equals(parent)) Parent = (int)parent;

        return this;
    }
}
