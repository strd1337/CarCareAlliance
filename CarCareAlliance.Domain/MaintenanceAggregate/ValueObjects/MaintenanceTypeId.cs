using CarCareAlliance.Domain.Common.Models;

namespace CarCareAlliance.Domain.MaintenanceAggregate.ValueObjects
{
    public sealed class MaintenanceTypeId : AggregateRootId<Guid>
    {
        public override Guid Value { get; protected set; }

        private MaintenanceTypeId(Guid value)
        {
            Value = value;
        }

        public static MaintenanceTypeId CreateUnique() => new(Guid.NewGuid());

        public static MaintenanceTypeId Create(Guid value)
            => new(value);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
