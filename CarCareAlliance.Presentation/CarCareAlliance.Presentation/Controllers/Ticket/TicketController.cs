using CarCareAlliance.Application.ServicePartners.Commands.Update;
using CarCareAlliance.Application.Tickets.Commands.Create;
using CarCareAlliance.Application.Tickets.Commands.Update;
using CarCareAlliance.Application.Tickets.Queries.GetAllByFilters;
using CarCareAlliance.Contracts.Common;
using CarCareAlliance.Contracts.ServicePartners.UpdateServicePartner;
using CarCareAlliance.Contracts.Tickets.Common;
using CarCareAlliance.Contracts.Tickets.Create;
using CarCareAlliance.Contracts.Tickets.Update;
using CarCareAlliance.Domain.TicketAggregate.Enums;
using CarCareAlliance.Presentation.Common.Helpers;
using CarCareAlliance.Presentation.Controllers.Common;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarCareAlliance.Presentation.Controllers.Ticket
{
    [Authorize]
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

        [HttpGet("{userId}/search")]
        public async Task<IActionResult> GetAllByFiltersAndUserId(
            Guid userId,
            [FromQuery(Name = "RepairStatus")] RepairStatus? repairStatus,
            [FromQuery] PaginationFilter filter,
            CancellationToken cancellationToken)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

            var query = new TicketGetAllByFiltersQuery(userId, repairStatus)
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
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            Guid id,
            UpdateTicketRequest request,
            CancellationToken cancellationToken)
        {
            var command = mapper.Map<UpdateTicketCommand>(request).SetTicketId(id);

            var result = await mediator
                .Send(command, cancellationToken);

            return result.Match(
                result => Ok(
                    mapper.Map<UpdateTicketResponse>(result)),
                errors => Problem(errors));
        }
    }
}
