using CarCareAlliance.Infrastructure.Persistance.Repositories.Auth.Roles;
using CarCareAlliance.Presentation.Controllers.Common;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CarCareAlliance.Domain.UserProfileAggregate.ValueObjects;
using CarCareAlliance.Contracts.Staff.Register;
using CarCareAlliance.Application.Staff.Commands;

namespace CarCareAlliance.Presentation.Controllers.Staff
{
    [HasRole(RoleType.Admin)]
    [Authorize]
    [Route("staff")]
    public class StaffController(
        IMediator mediator,
        IMapper mapper) : ApiController
    {
        private readonly IMediator mediator = mediator;
        private readonly IMapper mapper = mapper;

        [HttpPost("register")]
        public async Task<IActionResult> Register(
            StaffRegisterRequest request,
            CancellationToken cancellationToken)
        {
            var command = mapper.Map<StaffRegisterCommand>(request);

            var result = await mediator.Send(command, cancellationToken);

            return result.Match(
                result => Ok(mapper.Map<StaffRegisterResponse>(result)),
                errors => Problem(errors));
        }
    }
}
