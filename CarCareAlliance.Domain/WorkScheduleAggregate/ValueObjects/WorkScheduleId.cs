using CarCareAlliance.Domain.Common.Models;

namespace CarCareAlliance.Domain.WorkScheduleAggregate.ValueObjects
{
    public sealed class WorkScheduleId : AggregateRootId<Guid>
    {
        public override Guid Value { get; protected set; }

        private WorkScheduleId(Guid value)
        {
            Value = value;
        }

        public static WorkScheduleId CreateUnique() => new(Guid.NewGuid());

        public static WorkScheduleId Create(Guid value)
            => new(value);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
