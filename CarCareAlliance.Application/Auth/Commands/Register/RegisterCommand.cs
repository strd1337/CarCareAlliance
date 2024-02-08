using CarCareAlliance.Application.Auth.Common;
using CarCareAlliance.Application.Common.CQRS;

namespace CarCareAlliance.Application.Auth.Commands.Register
{
    public record RegisterCommand(
        string UserName,
        string Email,
        string Password) : ICommand<AuthenticationResult>;
}
