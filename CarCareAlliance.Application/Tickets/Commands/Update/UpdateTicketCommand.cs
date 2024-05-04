using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.Tickets.Common;
using CarCareAlliance.Domain.TicketAggregate.Enums;

namespace CarCareAlliance.Application.Tickets.Commands.Update
{
    public record UpdateTicketCommand(
        Guid TicketId,
        RepairStatus RepairStatus,
        string Comments) : ICommand<UpdateTicketResult>
    {
        public UpdateTicketCommand SetTicketId(Guid ticketId)
            => this with { TicketId = ticketId };
    }
}
