using CarCareAlliance.Presentation.Controllers.Common;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CarCareAlliance.Contracts.Staff.Register;
using CarCareAlliance.Application.Staff.Commands;
using CarCareAlliance.Presentation.Common.Helpers;
using CarCareAlliance.Contracts.Staff.Common;
using CarCareAlliance.Contracts.Common;
using CarCareAlliance.Application.Staff.Queries;

namespace CarCareAlliance.Presentation.Controllers.Staff
{
    //[Authorize]
    [Route("staffs")]
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

        [HttpGet("search")]
        public async Task<IActionResult> GetAllByFilters(
            [FromQuery(Name = "SearchKey")] string? searchKey,
            [FromQuery] PaginationFilter filter,
            CancellationToken cancellationToken)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

            var query = new GetAllStaffsByFiltersQuery(searchKey ?? string.Empty)
            {
                PageNumber = validFilter.PageNumber,
                PageSize = validFilter.PageSize
            };

            var result = await mediator
                .Send(query, cancellationToken);

            return result.Match(
                result => Ok(
                    mapper.Map<PagedResponse<MechanicDto>>(result)),
                errors => Problem(errors));
        }
    }
}
