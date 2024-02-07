using Microsoft.AspNetCore.Authorization;

namespace CarCareAlliance.Infrastructure.Persistance.Repositories.Auth.Roles
{
    public class RoleRequirement(string role) 
        : IAuthorizationRequirement
    {
        public string Role { get; } = role;
    }
}
