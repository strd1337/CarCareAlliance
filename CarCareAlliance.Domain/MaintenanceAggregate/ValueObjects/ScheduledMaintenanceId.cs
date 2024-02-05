using CarCareAlliance.Domain.Common.Models;

namespace CarCareAlliance.Domain.MaintenanceAggregate.ValueObjects
{
    public sealed class ScheduledMaintenanceId : AggregateRootId<Guid>
    {
        public override Guid Value { get; protected set; }

        private ScheduledMaintenanceId(Guid value)
        {
            Value = value;
        }

        public static ScheduledMaintenanceId CreateUnique() => new(Guid.NewGuid());

        public static ScheduledMaintenanceId Create(Guid value)
            => new(value);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
