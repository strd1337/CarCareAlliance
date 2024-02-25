using CarCareAlliance.Application.Tickets.Commands.Create;
using CarCareAlliance.Application.Tickets.Queries.GetAllByFilters;
using CarCareAlliance.Application.Tickets.Queries.GetByUserId;
using CarCareAlliance.Contracts.Common;
using CarCareAlliance.Contracts.Tickets.Common;
using CarCareAlliance.Contracts.Tickets.Create;
using CarCareAlliance.Contracts.Tickets.Get;
using CarCareAlliance.Domain.TicketAggregate.Enums;
using CarCareAlliance.Presentation.Common.Helpers;
using CarCareAlliance.Presentation.Controllers.Common;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarCareAlliance.Presentation.Controllers.Ticket
{
    [Route("tickets")]
    public class TicketController(
        IMediator mediator,
        IMapper mapper) : ApiController
    {
        private readonly IMediator mediator = mediator;
        private readonly IMapper mapper = mapper;

        [HttpPost]
        public async Task<IActionResult> Create(
            TicketCreateRequest request,
            CancellationToken cancellationToken)
        {
            var command = mapper.Map<TicketCreateCommand>(request);

            var result = await mediator
                .Send(command, cancellationToken);

            return result.Match(
                result => Ok(
                    mapper.Map<TicketCreateResponse>(result)),
                errors => Problem(errors));
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetAllByFilters(
            [FromQuery(Name = "ServicePartnerId")] Guid servicePartnerId,
            [FromQuery(Name = "Date")] DateTime? dateTime,
            [FromQuery(Name = "RepairStatus")] RepairStatus? repairStatus,
            [FromQuery] PaginationFilter filter,
            CancellationToken cancellationToken)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

            var query = new TicketGetAllByFiltersQuery(servicePartnerId, dateTime, repairStatus)
            {
                PageNumber = validFilter.PageNumber,
                PageSize = validFilter.PageSize
            };

            var result = await mediator
                .Send(query, cancellationToken);

            return result.Match(
                result => Ok(
                    mapper.Map<PagedResponse<TicketDto>>(result)),
                errors => Problem(errors));
        }

        [HttpGet("user/{userId}/servicepartner/{servicePartnerId}")]
        public async Task<IActionResult> Get(
           Guid userId,
           Guid servicePartnerId,
           CancellationToken cancellationToken)
        {
            var request = new TicketGetAllByUserIdRequest(userId, servicePartnerId);

            var query = mapper.Map<TicketGetAllByUserIdQuery>(request);

            var result = await mediator
                .Send(query, cancellationToken);
            
            return result.Match(
                result => Ok(
                    mapper.Map<TicketGetAllByUserIdResponse>(result)),
                errors => Problem(errors));
        }
    }
}
