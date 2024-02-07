using CarCareAlliance.Application.Auth.Commands.Register;
using CarCareAlliance.Contracts.Authentication;
using CarCareAlliance.Presentation.Controllers.Common;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarCareAlliance.Presentation.Controllers.Auth
{
    [Route("auth")]
    public class AuthenticationController(
        IMediator mediator,
        IMapper mapper) : ApiController
    {
        private readonly IMediator mediator = mediator;
        private readonly IMapper mapper = mapper;

        [HttpPost("register")]
        public async Task<IActionResult> Register(
            RegisterRequest request,
            CancellationToken cancellationToken)
        {
            var command = mapper.Map<RegisterCommand>(request);

            var authResult = await mediator.Send(command, cancellationToken);

            return authResult.Match(
                authResult => Ok(mapper.Map<AuthenticationResponse>(authResult)),
                errors => Problem(errors));
        }
    }
}