using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.WorkSchedules.Common;
using CarCareAlliance.Domain.WorkScheduleAggregate.ValueObjects;

namespace CarCareAlliance.Application.WorkSchedules.Commands.Add
{
    public record WorkScheduleAddCommand(
        DayOfWeek DayOfWeek,
        TimeOnly StartTime,
        TimeOnly EndTime,
        Guid OwnerId,
        ICollection<BreakTime> BreakTimes) : ICommand<WorkScheduleAddResult>;
}