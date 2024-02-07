using CarCareAlliance.Domain.Common.Models;

namespace CarCareAlliance.Domain.VehicleAggregate.ValueObjects
{
    public sealed class VehicleId : AggregateRootId<Guid>
    {
        public override Guid Value { get; protected set; }

        private VehicleId(Guid value)
        {
            Value = value;
        }

        public static VehicleId CreateUnique() => new(Guid.NewGuid());

        public static VehicleId Create(Guid value)
            => new(value);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
