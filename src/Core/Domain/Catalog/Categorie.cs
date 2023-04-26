using FSH.WebApi.Domain.Sales;

namespace FSH.WebApi.Domain.Catalog;

public class Categorie : AuditableEntity, IAggregateRoot
{
    public int Order { get; private set; }
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public CatalogType Type { get; private set; }
    public bool IsActive { get; private set; }

    public DefaultIdType GroupCategorieId { get; private set; }
    public virtual GroupCategorie GroupCategorie { get; private set; } = default!;
    public virtual ICollection<SubCategorie> SubCategories { get; private set; } = default!;

    public Categorie(int order, string code, string name, string? description, DefaultIdType groupCategorieId, CatalogType? type, bool? isActive)
    {
        Order = order;
        Code = code;
        Name = name;
        Description = description ?? string.Empty;
        Type = type ?? CatalogType.General;
        IsActive = isActive ?? true;

        GroupCategorieId = groupCategorieId;
    }

    public Categorie()
        : this(0, string.Empty, string.Empty, string.Empty, DefaultIdType.Empty, CatalogType.General, true)
    {
    }

    public Categorie Update(int? order, string? code, string? name, string? description, DefaultIdType? groupCategorieId, CatalogType? type, bool? isActive)
    {
        if (order is not null && order.HasValue && Order != order) Order = order.Value;

        if (code is not null && Code?.Equals(code) is not true) Code = code;
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        if (type is not null && !Type.Equals(type)) Type = type.Value;
        if (isActive is not null && !IsActive.Equals(isActive)) IsActive = (bool)isActive;

        if (groupCategorieId.HasValue && groupCategorieId.Value != DefaultIdType.Empty && !GroupCategorieId.Equals(groupCategorieId.Value)) GroupCategorieId = groupCategorieId.Value;

        return this;
    }
}