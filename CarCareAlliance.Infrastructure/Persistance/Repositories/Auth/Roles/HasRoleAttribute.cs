using CarCareAlliance.Domain.UserProfileAggregate.ValueObjects;
using Microsoft.AspNetCore.Authorization;

namespace CarCareAlliance.Infrastructure.Persistance.Repositories.Auth.Roles
{
    public sealed class HasRoleAttribute(RoleType role) 
        : AuthorizeAttribute(policy: role.ToString())
    {
    }
}
