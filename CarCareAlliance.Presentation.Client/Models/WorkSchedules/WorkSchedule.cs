namespace CarCareAlliance.Presentation.Client.Models.WorkSchedules
{
    public class WorkSchedule
    {
        public Guid WorkScheduleId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public Guid OwnerId { get; set; }
        public ICollection<BreakTime> BreakTimes { get; set; } = default!;
    }
}