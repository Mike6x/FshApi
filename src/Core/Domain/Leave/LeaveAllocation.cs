using FSH.WebApi.Domain.People;
using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Domain.Leave;

/// <summary>
/// LeaveType: public holidays, compensatory leave, sick leave,
/// casual leave, maternity leave, religious holidays, and bereavement leave.
/// - Sick leave: Max 60 non-paid days.
/// - Non-paid Leave: Max 30 non-paid days.
/// - Paid Leave
///    + Annual Leave: 12 Paid day.
///    + Extra Leave : One Paid day / one working yaer.
///    + Compensatory leave.
/// - Wedding: Max 3 paid day.
/// - Parental leave: Max 3 Paid day.
/// - Bereavement leave: Max 3 Paid day.
/// .
/// - Holidays Leave.
/// - Maternity: Thai san.
/// .
/// NumberOfCompensationDays: Number of Restored day Off.
/// NumberOfCarryOverDays: Number of Valid Days from prevous period.
/// NumberOfAdditionalDays: Depend on the woring time.
/// NumberOfDays: number of leave days per year defined in the Law.
/// NumberOfValidDays: Total leave days employee can used upto date.
/// NumberOfOnHandDays: Total leave days employee can used in request.
/// IsLocked = true: Can not change LeaveAllocation.
/// </summary>
public class LeaveAllocation(
    bool isActive,
    int period,
    DateTime fromDate,
    DateTime toDate,
    DefaultIdType employeeId,
    DefaultIdType leaveAllocationTypeId,
    int numberOfAnnualDays,
    int numberOfExtraDays,
    double numberOfCarryOverDays,
    double numberOfCompensationDays,
    double numberOfValidDays,
    double numberOfOnHandDays,
    bool isLocked) : AuditableEntity, IAggregateRoot
{
    public bool IsActive { get; private set; } = isActive;
    public int Period { get; private set; } = period;
    public DateTime FromDate { get; private set; } = fromDate;
    public DateTime ToDate { get; private set; } = toDate;
    public DefaultIdType EmployeeId { get; private set; } = employeeId;
    public virtual Employee Employee { get; private set; } = default!;
    public DefaultIdType LeaveAllocationTypeId { get; private set; } = leaveAllocationTypeId;
    public virtual Dimension LeaveAllocationType { get; private set; } = default!;

    public int NumberOfAnnualDays { get; private set; } = numberOfAnnualDays;
    public int NumberOfExtraDays { get; private set; } = numberOfExtraDays;

    public double NumberOfCarryOverDays { get; private set; } = numberOfCarryOverDays;

    public double NumberOfCompensationDays { get; private set; } = numberOfCompensationDays;
    public double NumberOfValidDays { get; private set; } = numberOfValidDays;
    public double NumberOfOnHandDays { get; private set; } = numberOfOnHandDays;

    public bool IsLocked { get; private set; } = isLocked;

    public LeaveAllocation()
    : this(true, 0, DateTime.UtcNow, DateTime.UtcNow, Guid.Empty, Guid.Empty, 12, 0, 0, 0, 12, 12, false)
    {
    }

    public LeaveAllocation Update(
        bool? isActive,
        int? period,
        DateTime? fromDate,
        DateTime? toDate,
        DefaultIdType? employeeId,
        DefaultIdType? leaveAllocationTypeId,
        int? numberOfAnnualDays,
        int? numberOfExtraDays,
        double? numberOfCarryOverDays,
        double? numberOfCompensationDays,
        double? numberOfValidDays,
        double? numberOfOnHandDays,
        bool? isLocked)
    {
        if (period.HasValue && period != Period) Period = period.Value;

        if (fromDate.HasValue && fromDate.Value != DateTime.MinValue && !FromDate.Equals(fromDate.Value)) FromDate = fromDate.Value;
        if (toDate.HasValue && toDate.Value != DateTime.MinValue && !ToDate.Equals(toDate.Value)) ToDate = toDate.Value;

        if (employeeId.HasValue && employeeId != DefaultIdType.Empty && !EmployeeId.Equals(employeeId)) EmployeeId = (DefaultIdType)employeeId;
        if (leaveAllocationTypeId.HasValue && leaveAllocationTypeId != DefaultIdType.Empty && !LeaveAllocationTypeId.Equals(leaveAllocationTypeId)) LeaveAllocationTypeId = (DefaultIdType)leaveAllocationTypeId;

        if (numberOfAnnualDays.HasValue && numberOfAnnualDays != NumberOfAnnualDays) NumberOfAnnualDays = numberOfAnnualDays.Value;
        if (numberOfExtraDays.HasValue && numberOfExtraDays != NumberOfExtraDays) NumberOfExtraDays = numberOfExtraDays.Value;

        if (numberOfCarryOverDays.HasValue && numberOfCarryOverDays != NumberOfCarryOverDays) NumberOfCarryOverDays = numberOfCarryOverDays.Value;
        if (numberOfCompensationDays.HasValue && numberOfCompensationDays != NumberOfCompensationDays) NumberOfCompensationDays = numberOfCompensationDays.Value;

        if (numberOfValidDays.HasValue && numberOfValidDays != NumberOfValidDays) NumberOfValidDays = numberOfValidDays.Value;
        if (numberOfOnHandDays.HasValue && numberOfOnHandDays != NumberOfOnHandDays) NumberOfOnHandDays = numberOfOnHandDays.Value;

        if (isLocked.HasValue && isLocked != IsLocked) IsLocked = isLocked.Value;

        if (isActive.HasValue && isActive != IsActive) IsActive = isActive.Value;
        IsActive = IsActive && FromDate <= DateTime.Today && ToDate >= DateTime.Today && !IsLocked;

        return this;
    }

    public LeaveAllocation Lock(bool isLocked)
    {
        IsLocked = isLocked;
        return this;
    }
}