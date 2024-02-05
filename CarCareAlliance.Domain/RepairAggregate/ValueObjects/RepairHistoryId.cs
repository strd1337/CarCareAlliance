using CarCareAlliance.Domain.Common.Models;

namespace CarCareAlliance.Domain.RepairAggregate.ValueObjects
{
    public sealed class RepairHistoryId : AggregateRootId<Guid>
    {
        public override Guid Value { get; protected set; }

        private RepairHistoryId(Guid value)
        {
            Value = value;
        }

        public static RepairHistoryId CreateUnique() => new(Guid.NewGuid());

        public static RepairHistoryId Create(Guid value)
            => new(value);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
