using CarCareAlliance.Contracts.WorkSchedules.Common;

namespace CarCareAlliance.Contracts.WorkSchedules.AddWorkSchedule
{
    public record WorkScheduleAddRequest(
        DayOfWeek DayOfWeek,
        TimeOnly StartTime,
        TimeOnly EndTime,
        Guid OwnerId,
        ICollection<BreakTimeDto> BreakTimes);
}