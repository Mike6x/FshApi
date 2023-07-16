namespace FSH.WebApi.Domain.Settings;

/// <summary>
/// Code = BackgroundJobName = Command_yyyymmdd_hhmm.
/// </summary>
public class CronJob(
    string code,
    string name,
    string? description,

    DateTime runTime,
    DateTime fromDate,
    DateTime toDate,

    int totalRecord,
    int numberOfSuccessed,
    int numberOfFailed,
    int numberOfDuplicated,
    int numberOfExisted) : AuditableEntity, IAggregateRoot
{
    public string Code { get; set; } = code;
    public string Name { get; set; } = name;
    public string? Description { get; set; } = description;

    public DateTime RunTime { get; set; } = runTime;
    public DateTime FromDate { get; set; } = fromDate;
    public DateTime ToDate { get; set; } = toDate;

    public int TotalRecord { get; set; } = totalRecord;
    public int NumberOfSuccessed { get; set; } = numberOfSuccessed;
    public int NumberOfFailed { get; set; } = numberOfFailed;
    public int NumberOfDuplicated { get; set; } = numberOfDuplicated;
    public int NumberOfExisted { get; set; } = numberOfExisted;

    public CronJob()
        : this(string.Empty, string.Empty, null, DateTime.UtcNow, DateTime.UtcNow, DateTime.UtcNow, 0, 0, 0, 0, 0)
    {
    }

    public CronJob Update(
            string? code,
            string? name,
            string? description,
            DateTime? runTime,
            DateTime? fromDate,
            DateTime? toDate,
            int? totalRecord,
            int? numberOfSuccessed,
            int? numberOfFailed,
            int? numberOfDuplicated,
            int? numberOfExisted)
    {
        if (code is not null && Code?.Equals(code) is not true) Code = code;
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (description is not null && Description?.Equals(description) is not true) Description = description;

        if (runTime is not null && !RunTime.Equals(runTime)) RunTime = (DateTime)runTime;

        if (fromDate is not null && !FromDate.Equals(fromDate)) FromDate = (DateTime)fromDate;
        if (toDate is not null && !ToDate.Equals(toDate)) ToDate = (DateTime)toDate;

        if (totalRecord is not null && totalRecord.HasValue && TotalRecord != totalRecord) TotalRecord = totalRecord.Value;
        if (numberOfSuccessed is not null && numberOfSuccessed.HasValue && NumberOfSuccessed != numberOfSuccessed) NumberOfSuccessed = numberOfSuccessed.Value;
        if (numberOfFailed is not null && numberOfFailed.HasValue && NumberOfFailed != numberOfFailed) NumberOfFailed = numberOfFailed.Value;
        if (numberOfDuplicated is not null && numberOfDuplicated.HasValue && NumberOfDuplicated != numberOfDuplicated) NumberOfDuplicated = numberOfDuplicated.Value;
        if (numberOfExisted is not null && numberOfExisted.HasValue && NumberOfExisted != numberOfExisted) NumberOfExisted = numberOfExisted.Value;

        return this;
    }
}
