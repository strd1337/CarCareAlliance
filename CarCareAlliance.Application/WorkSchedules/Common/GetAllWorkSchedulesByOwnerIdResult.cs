using CarCareAlliance.Domain.WorkScheduleAggregate;

namespace CarCareAlliance.Application.WorkSchedules.Common
{
    public record GetAllWorkSchedulesByOwnerIdResult(
        ICollection<WorkSchedule> WorkSchedules);
}