namespace FSH.WebApi.Application.Catalog.GroupCategories;

public class GroupCategorieDto : IDto
{
    public DefaultIdType Id { get; set; }
    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public CatalogType Type { get; set; }
    public bool IsActive { get; set; }

    public DefaultIdType BusinessLineId { get; set; }
    public string BusinessLineName { get; set; } = default!;
}

public class GroupCategorieDetailsDto : IDto
{
    public DefaultIdType Id { get; set; }

    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public CatalogType Type { get; set; }
    public bool IsActive { get; set; }
}

public class GroupCategorieExportDto : IDto
{
    public DefaultIdType Id { get; set; }
    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public CatalogType Type { get; set; }
    public bool IsActive { get; set; }

    public DefaultIdType BusinessLineId { get; set; }
    public string BusinessLineName { get; set; } = default!;

    public DefaultIdType CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public DefaultIdType LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
}