namespace FSH.WebApi.Application.Integration.ApiSerials;

public class ApiSerialDto : IDto
{
    public DefaultIdType Id { get; set; }

    public string ItemSerial { get; set; } = default!;
    public string ItemCode { get; set; } = default!;
    public string PoNumber { get; set; } = default!;
    public string PoStatus { get; set; } = default!;
    public DateTime PoCreatedDate { get; set; } = default!;
    public DateTime PoModifiedDate { get; set; } = default!;
    public string ItemName { get; set; } = default!;
    public string ItemClass { get; set; } = default!;
    public string ItemBrand { get; set; } = default!;
    public string PoProcessStatus { get; set; } = default!;
    public string CustomStatusSys { get; set; } = default!;
    public string CustomStatusIbsm { get; set; } = default!;

    public string? ImportStatus { get; set; }
    public DefaultIdType? CronJobId { get; set; }
    public string? CronJobName { get; set; }
}

public class ApiSerialDetailsDto : IDto
{
    public string ItemSerial { get; set; } = default!;
    public string ItemCode { get; set; } = default!;
    public string PoNumber { get; set; } = default!;
    public string ItemName { get; set; } = default!;
}

public class ApiSerialExportDto : IDto
{
    public DefaultIdType Id { get; set; }

    public string ItemSerial { get; set; } = default!;
    public string ItemCode { get; set; } = default!;
    public string PoNumber { get; set; } = default!;
    public string PoStatus { get; set; } = default!;
    public DateTime PoCreatedDate { get; set; } = default!;
    public DateTime PoModifiedDate { get; set; } = default!;
    public string ItemName { get; set; } = default!;
    public string ItemClass { get; set; } = default!;
    public string ItemBrand { get; set; } = default!;
    public string PoProcessStatus { get; set; } = default!;
    public string CustomStatusSys { get; set; } = default!;
    public string CustomStatusIbsm { get; set; } = default!;

    public string? ImportStatus { get; set; }
    public DefaultIdType? CronJobId { get; set; }
    public string? CronJobName { get; set; }

    public DefaultIdType CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public DefaultIdType LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
}