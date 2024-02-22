using CarCareAlliance.Application.Vehicles.Commands;
using CarCareAlliance.Contracts.Vehicles.Add;
using CarCareAlliance.Presentation.Controllers.Common;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarCareAlliance.Presentation.Controllers.Vehicle
{
    [Authorize]
    [Route("vehicles")]
    public class VehicleController(
        IMediator mediator,
        IMapper mapper) : ApiController
    {
        private readonly IMediator mediator = mediator;
        private readonly IMapper mapper = mapper;

        [HttpPost]
        public async Task<IActionResult> AddVehicle(
            VehicleAddRequest request,
            CancellationToken cancellationToken)
        {
            var command = mapper.Map<VehicleAddCommand>(request);

            var result = await mediator
                .Send(command, cancellationToken);

            return result.Match(
                result => Ok(
                    mapper.Map<VehicleAddResponse>(result)),
                errors => Problem(errors));
        }
    }
}
