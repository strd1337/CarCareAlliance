using CarCareAlliance.Domain.AuthenticationAggregate;
using CarCareAlliance.Domain.UserProfileAggregate;

namespace CarCareAlliance.Application.Users.Common
{
    public record UserProfileGetResult(
        Authentication AuthUser,
        UserProfile User);
}
