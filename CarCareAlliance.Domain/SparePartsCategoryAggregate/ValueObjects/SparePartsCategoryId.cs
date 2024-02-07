using CarCareAlliance.Domain.Common.Models;

namespace CarCareAlliance.Domain.PartsCategoryAggregate.ValueObjects
{
    public sealed class SparePartsCategoryId : AggregateRootId<Guid>
    {
        public override Guid Value { get; protected set; }

        private SparePartsCategoryId(Guid value)
        {
            Value = value;
        }

        public static SparePartsCategoryId CreateUnique() => new(Guid.NewGuid());

        public static SparePartsCategoryId Create(Guid value)
            => new(value);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
