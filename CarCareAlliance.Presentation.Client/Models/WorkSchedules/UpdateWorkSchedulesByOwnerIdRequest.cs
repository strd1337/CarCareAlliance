namespace CarCareAlliance.Presentation.Client.Models.WorkSchedules
{
    public class UpdateWorkSchedulesByOwnerIdRequest
    {
        public ICollection<WorkSchedule> WorkSchedules { get; set; } = default!;
    }
}
