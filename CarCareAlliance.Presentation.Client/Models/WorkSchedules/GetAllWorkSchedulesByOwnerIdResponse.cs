namespace CarCareAlliance.Presentation.Client.Models.WorkSchedules
{
    public class GetAllWorkSchedulesByOwnerIdResponse
    {
        public ICollection<WorkSchedule> WorkSchedules { get; set; } = default!;
    }
}
