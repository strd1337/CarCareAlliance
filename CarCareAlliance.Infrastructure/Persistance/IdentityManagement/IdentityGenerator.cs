using CarCareAlliance.Domain.UserProfileAggregate.ValueObjects;

namespace CarCareAlliance.Infrastructure.Persistance.IdentityManagement
{
    public sealed class IdentityGenerator
    {
        private readonly List<RoleId> roleIds = [];

        private readonly int roleIdsCount = 3;

        public IReadOnlyList<RoleId> RoleIds => roleIds.AsReadOnly();

        private IdentityGenerator()
        {
            GenerateRoleIds();
        }

        public static IdentityGenerator Create()
        {
            return new IdentityGenerator();
        }

        private void GenerateRoleIds()
        {
            for (int i = 0; i < roleIdsCount; ++i)
            {
                roleIds.Add(RoleId.CreateUnique());
            }
        }
    }
}
