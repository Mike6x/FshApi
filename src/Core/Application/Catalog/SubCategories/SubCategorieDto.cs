namespace FSH.WebApi.Application.Catalog.SubCategories;

public class SubCategorieDto : IDto
{
    public DefaultIdType Id { get; set; }
    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public CatalogType Type { get; set; }
    public bool IsActive { get; set; }

    public DefaultIdType CategorieId { get; set; }
    public string CategorieName { get; set; } = default!;
}

public class SubCategorieDetailsDto : IDto
{
    public DefaultIdType Id { get; set; }

    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public CatalogType Type { get; set; }
    public bool IsActive { get; set; }
}

public class SubCategorieExportDto : IDto
{
    public DefaultIdType Id { get; set; }
    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public CatalogType Type { get; set; }
    public bool IsActive { get; set; }

    public DefaultIdType CategorieId { get; set; }
    public string CategorieName { get; set; } = default!;

    public DefaultIdType CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public DefaultIdType LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
}