namespace CarCareAlliance.Contracts.WorkSchedules.Common
{
    public record BreakTimeDto(
        TimeOnly StartTime,
        TimeOnly EndTime);
}