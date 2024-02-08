using CarCareAlliance.Application.Auth.Common;
using CarCareAlliance.Application.Common.CQRS;

namespace CarCareAlliance.Application.Auth.Queries
{
    public record LoginQuery(
        string Email,
        string Password) : IQuery<AuthenticationResult>;
}
