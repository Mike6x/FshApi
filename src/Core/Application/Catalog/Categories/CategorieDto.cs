namespace FSH.WebApi.Application.Catalog.Categories;

public class CategorieDto : IDto
{
    public DefaultIdType Id { get; set; }
    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public CatalogType Type { get; set; }
    public bool IsActive { get; set; }

    public DefaultIdType GroupCategorieId { get; set; }
    public string GroupCategorieName { get; set; } = default!;
}

public class CategorieDetailsDto : IDto
{
    public DefaultIdType Id { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public CatalogType Type { get; set; }
    public bool IsActive { get; set; }
}

public class CategorieExportDto : IDto
{
    public DefaultIdType Id { get; set; }
    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public CatalogType Type { get; set; }
    public bool IsActive { get; set; }

    public DefaultIdType GroupCategorieId { get; set; }
    public string GroupCategorieName { get; set; } = default!;

    public DefaultIdType CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public DefaultIdType LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
}