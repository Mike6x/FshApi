namespace FSH.WebApi.Application.Organization.Departments;

public class DepartmentDto : IDto
{
    public Guid Id { get; set; }
    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }

    public Guid BusinessUnitId { get; set; }
    public string BusinessUnitName { get; set; } = default!;
}

public class DepartmentDetailsDto : IDto
{
    public Guid Id { get; set; }

    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}

public class DepartmentExportDto : IDto
{
    public Guid Id { get; set; }
    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public DefaultIdType BusinessUnitId { get; set; }
    public string BusinessUnitName { get; set; } = default!;

    public Guid CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public Guid LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
}