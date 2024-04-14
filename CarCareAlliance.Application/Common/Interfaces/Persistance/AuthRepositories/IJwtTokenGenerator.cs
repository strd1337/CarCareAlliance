using CarCareAlliance.Domain.AuthenticationAggregate;
using CarCareAlliance.Domain.MechanicAggregate;
using CarCareAlliance.Domain.UserProfileAggregate;
using System.Security.Claims;

namespace CarCareAlliance.Application.Common.Interfaces.Persistance.AuthRepositories
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(Authentication authUser, UserProfile userProfile, MechanicProfile? mechanic);
        ClaimsPrincipal ValidateToken(string token);
    }
}