namespace FSH.WebApi.Application.Settings.Menus;
public class MenuDto : IDto
{
    public DefaultIdType Id { get; set; }
    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }

    public string Href { get; set; } = string.Empty;
    public string? Icon { get; set; }
    public int Parent { get; set; }
}

public class MenuDetailsDto : IDto
{
    public DefaultIdType Id { get; set; }
    public int Code { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class MenuExportDto : IDto
{
    public DefaultIdType Id { get; set; }
    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }

    public string Href { get; set; } = string.Empty;
    public string? Icon { get; set; }
    public int Parent { get; set; }
}