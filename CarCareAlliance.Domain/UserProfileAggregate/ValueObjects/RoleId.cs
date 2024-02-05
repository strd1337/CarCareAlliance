using CarCareAlliance.Domain.Common.Models;

namespace CarCareAlliance.Domain.UserProfileAggregate.ValueObjects
{
    public sealed class RoleId : AggregateRootId<Guid>
    {
        public override Guid Value { get; protected set; }

        private RoleId(Guid value)
        {
            Value = value;
        }

        public static RoleId CreateUnique() => new(Guid.NewGuid());

        public static RoleId Create(Guid value)
            => new(value);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
