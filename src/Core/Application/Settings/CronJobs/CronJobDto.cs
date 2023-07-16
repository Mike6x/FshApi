namespace FSH.WebApi.Application.Settings.CronJobs;

public class CronJobDto : IDto
{
    public DefaultIdType Id { get; set; }

    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }

    public DateTime RunTime { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }

    public int TotalRecord { get; set; }
    public int NumberOfSuccessed { get; set; }
    public int NumberOfFailed { get; set; }
    public int NumberOfDuplicated { get; set; }
    public int NumberOfExisted { get; set; }
}

public class CronJobDetailsDto : IDto
{
    public DefaultIdType Id { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}

public class CronJobExportDto : IDto
{
    public DefaultIdType Id { get; set; }

    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }

    public DateTime RunTime { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }

    public int TotalRecord { get; set; }
    public int NumberOfSuccessed { get; set; }
    public int NumberOfFailed { get; set; }
    public int NumberOfDuplicated { get; set; }
    public int NumberOfExisted { get; set; }
}