using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace CarCareAlliance.Infrastructure.Persistance.Repositories.Auth.Roles
{
    public class RoleAuthorizationPolicyProvider(
        IOptions<AuthorizationOptions> options)
                : DefaultAuthorizationPolicyProvider(options)
    {
        public override async Task<AuthorizationPolicy?> GetPolicyAsync(
            string policyName)
        {
            var policy = await base.GetPolicyAsync(policyName);

            if (policy is not null)
            {
                return policy;
            }

            return new AuthorizationPolicyBuilder()
                .AddRequirements(new RoleRequirement(policyName))
                .Build();
        }
    }
}