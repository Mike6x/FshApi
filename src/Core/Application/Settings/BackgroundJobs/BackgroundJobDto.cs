using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Application.Settings.BackgroundJobs;

public class BackgroundJobDto : IDto
{
    public DefaultIdType Id { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public BackgroundJobType Type { get; set; }
    public CommandList Command { get; set; }

    public DateTime RunTime { get; set; }

    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public int? RepeatTimes { get; set; }

    public DefaultIdType? FatherId { get; set; }
    public string? FatherCode { get; set; }
    public int DayOfMonth => RunTime.Day;
    public DayOfWeek DayOfWeek => RunTime.DayOfWeek;
}

public class BackgroundJobDetailsDto : IDto
{
    public DefaultIdType Id { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}

public class BackgroundJobExportDto : IDto
{
    public DefaultIdType Id { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public BackgroundJobType Type { get; set; }
    public CommandList Command { get; set; }

    public DateTime RunTime { get; set; }

    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public int? RepeatTimes { get; set; }

    public DefaultIdType? FatherId { get; set; }
    public string? FatherCode { get; set; }

    public DefaultIdType CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public DefaultIdType LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }

    public int DayOfMonth => RunTime.Day;
    public DayOfWeek DayOfWeek => RunTime.DayOfWeek;
}