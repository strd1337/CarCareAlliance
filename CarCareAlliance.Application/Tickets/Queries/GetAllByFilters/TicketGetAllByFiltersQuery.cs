using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.Common.Pagination;
using CarCareAlliance.Domain.ServiceHistoryAggregate.Entities;
using CarCareAlliance.Domain.TicketAggregate.Enums;

namespace CarCareAlliance.Application.Tickets.Queries.GetAllByFilters
{
    public record TicketGetAllByFiltersQuery(
        Guid ServicePartnerId,
        DateTime? DateTime,
        RepairStatus? RepairStatus) : PagedQuery, IQuery<PagedResult<Ticket>>;
}
