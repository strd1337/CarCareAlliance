using CarCareAlliance.Domain.Common.Models;
using CarCareAlliance.Domain.WorkScheduleAggregate.ValueObjects;

namespace CarCareAlliance.Domain.WorkScheduleAggregate
{
    public sealed class WorkSchedule : AggregateRoot<WorkScheduleId, Guid>
    {
        private readonly List<DateTime> weekends = [];
        private readonly List<BreakTime> breakTimes = [];

        public DateTime DayOfWeek { get; private set; }
        public TimeOnly StartTime { get; private set; }
        public TimeOnly EndTime { get; private set; }
        public Guid OwnerId { get; private set; }
        
        public IReadOnlyList<DateTime> Weekends => weekends.AsReadOnly();
        
        public IReadOnlyList<BreakTime> BreakTimes =>
            breakTimes.AsReadOnly();

        private WorkSchedule(
            WorkScheduleId id,
            DateTime dayOfWeek,
            TimeOnly startTime,
            TimeOnly endTime,
            Guid ownerId) : base(id)
        {
            DayOfWeek = dayOfWeek;
            StartTime = startTime;
            EndTime = endTime;
            OwnerId = ownerId;
        }

        public static WorkSchedule Create(
            DateTime dayOfWeek,
            TimeOnly startTime,
            TimeOnly endTime,
            Guid ownerId)
        {
            return new WorkSchedule(
                WorkScheduleId.CreateUnique(),
                dayOfWeek,
                startTime,
                endTime,
                ownerId);
        }
    }
}
