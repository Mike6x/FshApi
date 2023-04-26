using FSH.WebApi.Domain.Sales;

namespace FSH.WebApi.Domain.Catalog;

public class SubCategorie : AuditableEntity, IAggregateRoot
{
    public int Order { get; private set; }
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public CatalogType Type { get; private set; }
    public bool IsActive { get; private set; }

    public DefaultIdType CategorieId { get; private set; }
    public virtual Categorie Categorie { get; private set; } = default!;

    public SubCategorie(int order, string code, string name, string? description, DefaultIdType categorieId, CatalogType? type, bool? isActive)
    {
        Order = order;
        Code = code;
        Name = name;
        Description = description ?? string.Empty;
        Type = type ?? CatalogType.General;
        IsActive = isActive ?? true;

        CategorieId = categorieId;
    }

    public SubCategorie()
        : this(0, string.Empty, string.Empty, string.Empty, DefaultIdType.Empty, CatalogType.General, true)
    {
    }

    public SubCategorie Update(int? order, string? code, string? name, string? description, DefaultIdType? categorieId, CatalogType? type, bool? isActive)
    {
        if (order is not null && order.HasValue && Order != order) Order = order.Value;

        if (code is not null && Code?.Equals(code) is not true) Code = code;
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        if (type is not null && !Type.Equals(type)) Type = type.Value;
        if (isActive is not null && !IsActive.Equals(isActive)) IsActive = (bool)isActive;

        if (categorieId.HasValue && categorieId.Value != DefaultIdType.Empty && !CategorieId.Equals(categorieId.Value)) CategorieId = categorieId.Value;

        return this;
    }
}