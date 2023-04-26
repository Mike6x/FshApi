using FSH.WebApi.Domain.Sales;

namespace FSH.WebApi.Domain.Catalog;

public class GroupCategorie : AuditableEntity, IAggregateRoot
{
    public int Order { get; private set; }
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public CatalogType Type { get; private set; }
    public bool IsActive { get; private set; }

    public DefaultIdType BusinessLineId { get; private set; }
    public virtual BusinessLine BusinessLine { get; private set; } = default!;
    public virtual ICollection<Categorie> Categories { get; private set; } = default!;

    public GroupCategorie(int order, string code, string name, string? description, DefaultIdType businessLineId, CatalogType? type, bool? isActive)
    {
        Order = order;
        Code = code;
        Name = name;
        Description = description ?? string.Empty;
        Type = type ?? CatalogType.General;
        IsActive = isActive ?? true;

        BusinessLineId = businessLineId;
    }

    public GroupCategorie()
        : this(0, string.Empty, string.Empty, string.Empty, DefaultIdType.Empty, CatalogType.General, true)
    {
    }

    public GroupCategorie Update(int? order, string? code, string? name, string? description, DefaultIdType? businessLineId, CatalogType? type, bool? isActive)
    {
        if (order is not null && order.HasValue && Order != order) Order = order.Value;

        if (code is not null && Code?.Equals(code) is not true) Code = code;
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        if (type is not null && !Type.Equals(type)) Type = type.Value;
        if (isActive is not null && !IsActive.Equals(isActive)) IsActive = (bool)isActive;

        if (businessLineId.HasValue && businessLineId.Value != DefaultIdType.Empty && !BusinessLineId.Equals(businessLineId.Value)) BusinessLineId = businessLineId.Value;

        return this;
    }
}