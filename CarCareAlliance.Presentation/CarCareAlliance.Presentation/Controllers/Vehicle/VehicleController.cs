using CarCareAlliance.Application.Vehicles.Commands.Add;
using CarCareAlliance.Application.Vehicles.Queries.Get;
using CarCareAlliance.Application.Vehicles.Queries.GetByUserId;
using CarCareAlliance.Contracts.Vehicles.Add;
using CarCareAlliance.Contracts.Vehicles.Get;
using CarCareAlliance.Contracts.Vehicles.GetByUserId;
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

        [HttpGet("{vehicleId}")]
        public async Task<IActionResult> Get(
            Guid vehicleId,
            CancellationToken cancellationToken)
        {
            var request = new VehicleGetRequest(vehicleId);

            var query = mapper.Map<VehicleGetQuery>(request);

            var result = await mediator
                .Send(query, cancellationToken);

            return result.Match(
                result => Ok(
                    mapper.Map<VehicleGetResponse>(result)),
                errors => Problem(errors));
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(
            Guid userId,
            CancellationToken cancellationToken)
        {
            var request = new VehicleGetByUserIdRequest(userId);

            var query = mapper.Map<VehicleGetByUserIdQuery>(request);

            var result = await mediator
                .Send(query, cancellationToken);

            return result.Match(
                result => Ok(
                    mapper.Map<VehicleGetByUserIdResponse>(result)),
                errors => Problem(errors));
        }
    }
}
