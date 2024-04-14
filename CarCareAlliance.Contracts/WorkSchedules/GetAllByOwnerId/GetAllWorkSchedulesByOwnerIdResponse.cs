using CarCareAlliance.Contracts.WorkSchedules.Common;

namespace CarCareAlliance.Contracts.WorkSchedules.GetAllByOwnerId
{
    public record GetAllWorkSchedulesByOwnerIdResponse(
        ICollection<WorkScheduleDto> WorkSchedules);
}