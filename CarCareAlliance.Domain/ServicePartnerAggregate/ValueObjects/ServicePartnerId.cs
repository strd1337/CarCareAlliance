using CarCareAlliance.Domain.Common.Models;

namespace CarCareAlliance.Domain.ServicePartnerAggregate.ValueObjects
{
    public sealed class ServicePartnerId : AggregateRootId<Guid>
    {
        public override Guid Value { get; protected set; }

        private ServicePartnerId(Guid value)
        {
            Value = value;
        }

        public static ServicePartnerId CreateUnique() => new(Guid.NewGuid());

        public static ServicePartnerId Create(Guid value)
            => new(value);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
