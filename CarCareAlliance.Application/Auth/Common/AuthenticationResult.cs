using CarCareAlliance.Domain.AuthenticationAggregate;

namespace CarCareAlliance.Application.Auth.Common
{
    public record AuthenticationResult(
        Guid AuthenticationId,
        Authentication AuthUser,
        string Token);
}
