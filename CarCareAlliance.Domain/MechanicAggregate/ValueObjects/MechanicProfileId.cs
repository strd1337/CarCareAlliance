using CarCareAlliance.Domain.Common.Models;

namespace CarCareAlliance.Domain.MechanicAggregate.ValueObjects
{
    public sealed class MechanicProfileId : AggregateRootId<Guid>
    {
        public override Guid Value { get; protected set; }

        private MechanicProfileId(Guid value)
        {
            Value = value;
        }

        public static MechanicProfileId CreateUnique() => new(Guid.NewGuid());

        public static MechanicProfileId Create(Guid value)
            => new(value);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
