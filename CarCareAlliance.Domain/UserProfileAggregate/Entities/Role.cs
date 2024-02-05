using CarCareAlliance.Domain.Common.Models;
using CarCareAlliance.Domain.UserProfileAggregate.ValueObjects;

namespace CarCareAlliance.Domain.UserProfileAggregate.Entities
{
    public sealed class Role : Entity<RoleId>
    {
        public string Name { get; private set; }

        private Role(
            RoleId id,
            string name) : base(id)
        {
            Name = name;
        }

        public static Role Create(
            string name)
        {
            return new(
                RoleId.CreateUnique(),
                name);
        }
    }
}
