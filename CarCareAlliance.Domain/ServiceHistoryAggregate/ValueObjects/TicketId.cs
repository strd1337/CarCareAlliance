using CarCareAlliance.Domain.Common.Models;

namespace CarCareAlliance.Domain.TicketAggregate.ValueObjects
{
    public sealed class TicketId : AggregateRootId<Guid>
    {
        public override Guid Value { get; protected set; }

        private TicketId(Guid value)
        {
            Value = value;
        }

        public static TicketId CreateUnique() => new(Guid.NewGuid());

        public static TicketId Create(Guid value)
            => new(value);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
