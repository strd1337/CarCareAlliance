using CarCareAlliance.Domain.Common.Models;

namespace CarCareAlliance.Domain.WorkScheduleAggregate.ValueObjects
{
    public class BreakTime : ValueObject
    {
        public TimeOnly StartTime { get; private set; }
        public TimeOnly EndTime { get; private set; }

        private BreakTime(
            TimeOnly startTime,
            TimeOnly endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
        }

        public static BreakTime CreateNew(
            TimeOnly startTime,
            TimeOnly endTime)
        {
            return new(
                startTime,
                endTime);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return StartTime;
            yield return EndTime;
        }

        public BreakTime() { }
    }
}