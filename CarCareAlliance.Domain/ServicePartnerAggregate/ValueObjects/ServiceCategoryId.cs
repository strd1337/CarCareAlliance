using CarCareAlliance.Domain.Common.Models;

namespace CarCareAlliance.Domain.ServicePartnerAggregate.ValueObjects
{
    public sealed class ServiceCategoryId : AggregateRootId<Guid>
    {
        public override Guid Value { get; protected set; }

        private ServiceCategoryId(Guid value)
        {
            Value = value;
        }

        public static ServiceCategoryId CreateUnique() => new(Guid.NewGuid());

        public static ServiceCategoryId Create(Guid value)
            => new(value);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
