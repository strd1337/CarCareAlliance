using CarCareAlliance.Domain.Common.Models;

namespace CarCareAlliance.Domain.UserProfileAggregate.ValueObjects
{
    public sealed class UserProfileId : AggregateRootId<Guid>
    {
        public override Guid Value { get; protected set; }

        private UserProfileId(Guid value)
        {
            Value = value;
        }

        public static UserProfileId CreateUnique() => new(Guid.NewGuid());

        public static UserProfileId Create(Guid value)
            => new(value);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
