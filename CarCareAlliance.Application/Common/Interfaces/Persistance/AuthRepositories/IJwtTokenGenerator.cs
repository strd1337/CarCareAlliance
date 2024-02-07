using CarCareAlliance.Domain.AuthenticationAggregate;
using CarCareAlliance.Domain.UserProfileAggregate;

namespace CarCareAlliance.Application.Common.Interfaces.Persistance.AuthRepositories
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(Authentication authUser, UserProfile userProfile);
    }
}