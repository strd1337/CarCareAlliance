using CarCareAlliance.Domain.Common.Models;

namespace CarCareAlliance.Domain.TicketAggregate.ValueObjects
{
    public sealed class OrderDetailsId : AggregateRootId<Guid>
    {
        public override Guid Value { get; protected set; }

        private OrderDetailsId(Guid value)
        {
            Value = value;
        }

        public static OrderDetailsId CreateUnique() => new(Guid.NewGuid());

        public static OrderDetailsId Create(Guid value)
            => new(value);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
