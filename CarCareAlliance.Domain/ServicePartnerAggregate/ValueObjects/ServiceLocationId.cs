using CarCareAlliance.Domain.Common.Models;

namespace CarCareAlliance.Domain.ServicePartnerAggregate.ValueObjects
{
    public sealed class ServiceLocationId : AggregateRootId<Guid>
    {
        public override Guid Value { get; protected set; }

        private ServiceLocationId(Guid value)
        {
            Value = value;
        }

        public static ServiceLocationId CreateUnique() => new(Guid.NewGuid());

        public static ServiceLocationId Create(Guid value)
            => new(value);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
