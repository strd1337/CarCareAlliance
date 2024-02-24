using CarCareAlliance.Application.Tickets.Commands.Create;
using CarCareAlliance.Contracts.Tickets.Create;
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
    }
}
