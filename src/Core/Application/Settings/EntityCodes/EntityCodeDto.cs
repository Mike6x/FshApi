using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Application.Settings.EntityCodes;

public class EntityCodeDto : IDto
{
    public DefaultIdType Id { get; set; }
    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string Seperator { get; set; } = default!;
    public int? Value { get; set; }
    public CodeType? Type { get; set; }
    public bool IsActive { get; set; }
}

public class EntityCodeDetailsDto : IDto
{
    public DefaultIdType Id { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;

}

public class EntityCodeExportDto : IDto
{
    public DefaultIdType Id { get; set; }
    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string Seperator { get; set; } = default!;
    public int? Value { get; set; }
    public CodeType? Type { get; set; }
    public bool IsActive { get; set; }

    public DefaultIdType CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public DefaultIdType LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
}