namespace FSH.WebApi.Domain.Settings;

/// <summary>
/// Command_yyyymmdd_hhmm.
/// </summary>
public class BackgroundJob(
    string code,
    string name,
    string? description,
    BackgroundJobType type,
    CommandList command,
    DateTime runTime,
    DateTime fromDate,
    DateTime toDate,
    int? repeatTimes,
    DefaultIdType? fatherId) : AuditableEntity, IAggregateRoot
{
    public string Code { get; private set; } = code;
    public string Name { get; private set; } = name;
    public string? Description { get; private set; } = description;

    public BackgroundJobType Type { get; private set; } = type == BackgroundJobType.All
        ? BackgroundJobType.FireAndForget
        : type;

    public CommandList Command { get; private set; } = command;

    public DateTime RunTime { get; private set; } = runTime == DateTime.MinValue ? DateTime.UtcNow : runTime;
    public DateTime FromDate { get; set; } = fromDate == DateTime.MinValue ? DateTime.UtcNow : fromDate;
    public DateTime ToDate { get; set; } = toDate == DateTime.MinValue ? DateTime.UtcNow : toDate;
    public int? RepeatTimes { get; private set; } = repeatTimes ?? 1;

    public DefaultIdType? FatherId { get; private set; } = fatherId == DefaultIdType.Empty ? null : fatherId;
    public virtual BackgroundJob? Father { get; private set; }

    public BackgroundJob()
    : this(
          string.Empty,
          string.Empty,
          null,
          BackgroundJobType.FireAndForget,
          CommandList.BrandGenerator,
          DateTime.UtcNow,
          DateTime.UtcNow,
          DateTime.UtcNow,
          1,
          null)
    {
    }

    public BackgroundJob Update(
        string code,
        string name,
        string? description,
        BackgroundJobType? type,
        CommandList? command,
        DateTime? runTime,
        DateTime? fromDate,
        DateTime? toDate,
        int? repeatTimes,
        DefaultIdType? fatherId)
    {
        if (code is not null && Code?.Equals(code) is not true) Code = code;
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (description is not null && Description?.Equals(description) is not true) Description = description;

        if (type is not null && !Type.Equals(BackgroundJobType.All) && !Type.Equals(type)) Type = type.Value;
        if (command is not null && !Command.Equals(CommandList.All) && !Command.Equals(command)) Command = command.Value;
        if (runTime is not null && !RunTime.Equals(runTime)) RunTime = runTime.Value;

        if (fromDate is not null && !FromDate.Equals(fromDate)) FromDate = (DateTime)fromDate;
        if (toDate is not null && !ToDate.Equals(toDate)) ToDate = (DateTime)toDate;
        if (repeatTimes is not null && RepeatTimes?.Equals(repeatTimes) is not true) RepeatTimes = repeatTimes;

        if (fatherId == DefaultIdType.Empty)
        {
            FatherId = null;
        }
        else if (fatherId.HasValue && !FatherId.Equals(fatherId.Value))
        {
            FatherId = fatherId.Value;
        }

        return this;
    }
}