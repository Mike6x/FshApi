namespace FSH.WebApi.Application.Organization.SubDepartments;

public class SubDepartmentDto : IDto
{
    public Guid Id { get; set; }
    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }

    public Guid DepartmentId { get; set; }
    public string DepartmentName { get; set; } = default!;
}

public class SubDepartmentDetailsDto : IDto
{
    public Guid Id { get; set; }

    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}

public class SubDepartmentExportDto : IDto
{
    public Guid Id { get; set; }
    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }

    public DefaultIdType DepartmentId { get; set; }
    public string DepartmentName { get; set; } = default!;

    public Guid CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public Guid LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
}