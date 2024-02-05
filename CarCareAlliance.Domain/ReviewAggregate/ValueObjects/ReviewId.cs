using CarCareAlliance.Domain.Common.Models;


namespace CarCareAlliance.Domain.ReviewAggregate.ValueObjects
{
    public sealed class ReviewId : AggregateRootId<Guid>
    {
        public override Guid Value { get; protected set; }

        private ReviewId(Guid value)
        {
            Value = value;
        }

        public static ReviewId CreateUnique() => new(Guid.NewGuid());

        public static ReviewId Create(Guid value)
            => new(value);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
