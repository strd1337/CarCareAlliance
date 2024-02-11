using CarCareAlliance.Contracts.WorkSchedules.Common;

namespace CarCareAlliance.Contracts.WorkSchedules.GetByOwnerId
{
    public record WorkScheduleGetByOwnerIdResponse(
        WorkScheduleDto WorkSchedule);
}