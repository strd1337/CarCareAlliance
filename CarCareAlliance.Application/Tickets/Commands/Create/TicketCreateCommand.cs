using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.Tickets.Common;

namespace CarCareAlliance.Application.Tickets.Commands.Create
{
    public record TicketCreateCommand() : ICommand<TicketCreateResult>;
}
