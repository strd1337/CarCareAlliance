using CarCareAlliance.Domain.WorkScheduleAggregate;

namespace CarCareAlliance.Application.WorkSchedules.Common
{
    public record WorkScheduleGetByOwnerIdResult(
        WorkSchedule WorkSchedule);
}