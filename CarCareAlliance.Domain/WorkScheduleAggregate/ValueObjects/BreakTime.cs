using CarCareAlliance.Domain.Common.Models;

namespace CarCareAlliance.Domain.WorkScheduleAggregate.ValueObjects
{
    public class BreakTime : ValueObject
    {
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        private BreakTime(
            DateTime startTime,
            DateTime endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
        }

        public static BreakTime CreateNew(
            DateTime startTime,
            DateTime endTime)
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
    }
}
