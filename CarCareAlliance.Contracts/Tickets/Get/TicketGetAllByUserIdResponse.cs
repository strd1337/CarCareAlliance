using CarCareAlliance.Contracts.Tickets.Common;

namespace CarCareAlliance.Contracts.Tickets.Get
{
    public record TicketGetAllByUserIdResponse(
        ICollection<TicketDto> Tickets);
}
