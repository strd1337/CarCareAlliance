using CarCareAlliance.Presentation.Controllers.Common;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CarCareAlliance.Contracts.Staff.Register;
using CarCareAlliance.Application.Staff.Commands;

namespace CarCareAlliance.Presentation.Controllers.Staff
{
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
