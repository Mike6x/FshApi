namespace FSH.WebApi.Application.Settings.Dimensions;

public class DimensionDto : IDto
{
    public DefaultIdType Id { get; set; }

    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }

    public string? FullName { get; set; }
    public string? NativeName { get; set; }
    public string? FullNativeName { get; set; }
    public int Value { get; set; }
    public string Type { get; set; } = default!;
    public Guid? FatherId { get; set; }
    public string? FatherName { get; set; }
}

public class DimensionDetailsDto : IDto
{
    public DefaultIdType Id { get; set; }

    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? NativeName { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public int Value { get; set; }
    public string Type { get; set; } = default!;
    public Guid? FatherId { get; set; }
    public string? FatherName { get; set; }
}

public class DimensionExportDto : IDto
{
    public DefaultIdType Id { get; set; }

    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }

    public string? FullName { get; set; }
    public string? NativeName { get; set; }
    public string? FullNativeName { get; set; }

    public int Value { get; set; }
    public string Type { get; set; } = default!;
    public Guid? FatherId { get; set; }
    public string? FatherName { get; set; }

    public DefaultIdType CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public DefaultIdType LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
}