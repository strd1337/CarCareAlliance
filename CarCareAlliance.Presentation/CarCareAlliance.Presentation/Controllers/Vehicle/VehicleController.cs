using CarCareAlliance.Application.Vehicles.Commands.Add;
using CarCareAlliance.Application.Vehicles.Queries.Get;
using CarCareAlliance.Application.Vehicles.Queries.GetAllByUserIdAndFilters;
using CarCareAlliance.Contracts.Common;
using CarCareAlliance.Contracts.Vehicles.Add;
using CarCareAlliance.Contracts.Vehicles.Common;
using CarCareAlliance.Contracts.Vehicles.Get;
using CarCareAlliance.Presentation.Common.Helpers;
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

        [HttpGet("users/{userId}")]
        public async Task<IActionResult> GetAllByUserIdAndFilters(
            Guid userId,
            [FromQuery] PaginationFilter filter,
            CancellationToken cancellationToken)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

            var query = new GetAllByUserIdAndFiltersQuery(userId)
            {
                PageNumber = validFilter.PageNumber,
                PageSize = validFilter.PageSize
            };

            var result = await mediator
               .Send(query, cancellationToken);

            return result.Match(
                result => Ok(
                    mapper.Map<PagedResponse<VehicleDto>>(result)),
                errors => Problem(errors));
        }
    }
}
