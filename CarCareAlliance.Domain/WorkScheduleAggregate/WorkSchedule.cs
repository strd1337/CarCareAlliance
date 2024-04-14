using CarCareAlliance.Domain.Common.Models;
using CarCareAlliance.Domain.WorkScheduleAggregate.ValueObjects;

namespace CarCareAlliance.Domain.WorkScheduleAggregate
{
    public sealed class WorkSchedule : AggregateRoot<WorkScheduleId, Guid>
    {
        private readonly List<BreakTime> breakTimes = [];

        public DayOfWeek DayOfWeek { get; private set; }
        public TimeOnly StartTime { get; private set; }
        public TimeOnly EndTime { get; private set; }
        public Guid OwnerId { get; private set; }
        
        public IReadOnlyList<BreakTime> BreakTimes =>
            breakTimes.AsReadOnly();

        private WorkSchedule(
            WorkScheduleId id,
            DayOfWeek dayOfWeek,
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
            DayOfWeek dayOfWeek,
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

        public void AddBreakTimes(List<BreakTime> breakTimes)
        {
            this.breakTimes.AddRange(breakTimes);
        }

        public void Update(DayOfWeek dayOfWeek,
            TimeOnly startTime,
            TimeOnly endTime,
            List<BreakTime> breakTimes)
        {
            DayOfWeek = dayOfWeek;
            StartTime = startTime;
            EndTime = endTime;
            this.breakTimes.Clear();
            this.breakTimes.AddRange(breakTimes);
        }

#pragma warning disable CS8618
        private WorkSchedule()
        {
        }
#pragma warning restore CS8618
    }
}
