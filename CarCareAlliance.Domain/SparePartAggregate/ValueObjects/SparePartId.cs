using CarCareAlliance.Domain.Common.Models;


namespace CarCareAlliance.Domain.SparePartAggregate.ValueObjects
{
    public sealed class SparePartId : AggregateRootId<Guid>
    {
        public override Guid Value { get; protected set; }

        private SparePartId(Guid value)
        {
            Value = value;
        }

        public static SparePartId CreateUnique() => new(Guid.NewGuid());

        public static SparePartId Create(Guid value)
            => new(value);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
