using CarCareAlliance.Contracts.WorkSchedules.Common;

namespace CarCareAlliance.Contracts.WorkSchedules.UpdateByOwnerId
{
    public record UpdateWorkSchedulesByOwnerIdRequest(
        ICollection<WorkScheduleDto> WorkSchedules);
}
