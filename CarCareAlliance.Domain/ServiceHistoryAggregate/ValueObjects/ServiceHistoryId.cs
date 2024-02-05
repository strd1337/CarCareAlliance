using CarCareAlliance.Domain.Common.Models;

namespace CarCareAlliance.Domain.ServiceHistoryAggregate.ValueObjects
{
    public sealed class ServiceHistoryId : AggregateRootId<Guid>
    {
        public override Guid Value { get; protected set; }

        private ServiceHistoryId(Guid value)
        {
            Value = value;
        }

        public static ServiceHistoryId CreateUnique() => new(Guid.NewGuid());

        public static ServiceHistoryId Create(Guid value)
            => new(value);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
