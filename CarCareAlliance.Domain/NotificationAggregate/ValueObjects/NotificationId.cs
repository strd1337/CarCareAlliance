using CarCareAlliance.Domain.Common.Models;

namespace CarCareAlliance.Domain.NotificationAggregate.ValueObjects
{
    public sealed class NotificationId : AggregateRootId<Guid>
    {
        public override Guid Value { get; protected set; }

        private NotificationId(Guid value)
        {
            Value = value;
        }

        public static NotificationId CreateUnique() => new(Guid.NewGuid());

        public static NotificationId Create(Guid value)
            => new(value);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
