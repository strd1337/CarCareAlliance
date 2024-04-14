using CarCareAlliance.Presentation.Client.Models.WorkSchedules;

namespace CarCareAlliance.Presentation.Client.Services.Interfaces
{
    public interface IWorkScheduleService
    {
        Task<bool> CreateAsync(WorkSchedule workSchedule);
        Task<bool> UpdateByOwnerIdAsync(Guid ownerId, ICollection<WorkSchedule> workSchedules);
        Task<GetAllWorkSchedulesByOwnerIdResponse> GetAllByOwnerIdAsync(Guid ownerId);
    }
}
