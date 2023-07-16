using FSH.WebApi.Domain.Leave;

namespace FSH.WebApi.Application.Leave.LeaveApplications;
public class LeaveApplicationHelper
{
    public static double DayOffCalculate(DateTime fromDate, DateTime toDate, DayOffType firstLeaveDay, DayOffType lastLeaveDay)
    {
        double fistDayOff = firstLeaveDay switch
        {
            DayOffType.FullDay => 1,
            DayOffType.Sheet1 => 0.5,
            DayOffType.Sheet2 => 0.5,
            _ => 0,
        };

        double lastDayOff = lastLeaveDay switch
        {
            DayOffType.FullDay => 1,
            DayOffType.Sheet1 => 0.5,
            DayOffType.Sheet2 => 0.5,
            _ => 0,
        };

        double totalDayOff = (toDate - fromDate).Days;

        if (totalDayOff <= 0) return fistDayOff;
        else
            return totalDayOff - 1 + fistDayOff + lastDayOff;
    }
}
