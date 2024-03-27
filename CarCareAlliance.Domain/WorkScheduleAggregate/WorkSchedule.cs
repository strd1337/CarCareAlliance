using CarCareAlliance.Domain.Common.Models;
using CarCareAlliance.Domain.WorkScheduleAggregate.ValueObjects;

namespace CarCareAlliance.Domain.WorkScheduleAggregate
{
    public sealed class WorkSchedule : AggregateRoot<WorkScheduleId, Guid>
    {
        private readonly List<DayOfWeek> weekends = [];
        private readonly List<BreakTime> breakTimes = [];

        public DayOfWeek DayOfWeek { get; private set; }
        public TimeOnly StartTime { get; private set; }
        public TimeOnly EndTime { get; private set; }
        public Guid OwnerId { get; private set; }
        
        public IReadOnlyList<DayOfWeek> Weekends => weekends.AsReadOnly();
        
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

        public void AddWeekends(List<DayOfWeek> weekends)
        {
            this.weekends.AddRange(weekends);
        }

        public void AddBreakTimes(List<BreakTime> breakTimes)
        {
            this.breakTimes.AddRange(breakTimes);
        }

        public void Update(DayOfWeek dayOfWeek,
            TimeOnly startTime,
            TimeOnly endTime,
            Guid ownerId,
            List<DayOfWeek> weekends,
            List<BreakTime> breakTimes)
        {
            DayOfWeek = dayOfWeek;
            StartTime = startTime;
            EndTime = endTime;
            OwnerId = ownerId;
            this.breakTimes.Clear();
            this.breakTimes.AddRange(breakTimes);
            this.weekends.Clear();
            this.weekends.AddRange(weekends);
        }

#pragma warning disable CS8618
        private WorkSchedule()
        {
        }
#pragma warning restore CS8618
    }
}
