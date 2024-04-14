namespace CarCareAlliance.Contracts.WorkSchedules.GetAllByOwnerId
{
    public record GetAllWorkSchedulesByOwnerIdRequest(
        Guid OwnerId);
}