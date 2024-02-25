using CarCareAlliance.Domain.ServiceHistoryAggregate.Entities;

namespace CarCareAlliance.Application.Tickets.Common
{
    public record TicketGetAllByUserIdResult(
        ICollection<Ticket> Tickets);
}
