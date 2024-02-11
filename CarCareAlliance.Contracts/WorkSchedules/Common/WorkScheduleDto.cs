namespace CarCareAlliance.Contracts.WorkSchedules.Common
{
    public record WorkScheduleDto(
        Guid WorkScheduleId,
        DayOfWeek DayOfWeek,
        TimeOnly StartTime,
        TimeOnly EndTime,
        Guid OwnerId,
        ICollection<DayOfWeek> Weekends,
        ICollection<BreakTimeDto> BreakTimes);
}