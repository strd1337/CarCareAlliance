using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.Tickets.Common;

namespace CarCareAlliance.Application.Tickets.Queries.GetByUserId
{
    public record TicketGetAllByUserIdQuery(
        Guid UserId,
        Guid ServicePartnerId) : IQuery<TicketGetAllByUserIdResult>;
}
