using CarCareAlliance.Domain.Common.Models;

namespace CarCareAlliance.Domain.PhotoAggregate.ValueObjects
{
    public sealed class PhotoId : AggregateRootId<Guid>
    {
        public override Guid Value { get; protected set; }

        private PhotoId(Guid value)
        {
            Value = value;
        }

        public static PhotoId CreateUnique() => new(Guid.NewGuid());

        public static PhotoId Create(Guid value)
            => new(value);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
