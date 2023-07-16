namespace FSH.WebApi.Domain.Catalog;

public class Categorie(int order, string code, string name, string? description, DefaultIdType groupCategorieId, CatalogType? type, bool? isActive) : AuditableEntity, IAggregateRoot
{
    public int Order { get; private set; } = order;
    public string Code { get; private set; } = code;
    public string Name { get; private set; } = name;
    public string? Description { get; private set; } = description ?? string.Empty;
    public CatalogType Type { get; private set; } = type ?? CatalogType.General;
    public bool IsActive { get; private set; } = isActive ?? true;

    public DefaultIdType GroupCategorieId { get; private set; } = groupCategorieId;
    public virtual GroupCategorie GroupCategorie { get; private set; } = default!;
    public virtual ICollection<SubCategorie> SubCategories { get; private set; } = default!;

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
        if (type is not null && !Type.Equals(CatalogType.All) && !Type.Equals(type)) Type = type.Value;
        if (isActive is not null && !IsActive.Equals(isActive)) IsActive = (bool)isActive;

        if (groupCategorieId.HasValue && groupCategorieId.Value != DefaultIdType.Empty && !GroupCategorieId.Equals(groupCategorieId.Value)) GroupCategorieId = groupCategorieId.Value;

        return this;
    }
}