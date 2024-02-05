using CarCareAlliance.Domain.Common.Models;

namespace CarCareAlliance.Domain.ServicePartnerAggregate.ValueObjects
{
    public sealed class ServiceId : AggregateRootId<Guid>
    {
        public override Guid Value { get; protected set; }

        private ServiceId(Guid value)
        {
            Value = value;
        }

        public static ServiceId CreateUnique() => new(Guid.NewGuid());

        public static ServiceId Create(Guid value)
            => new(value);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
