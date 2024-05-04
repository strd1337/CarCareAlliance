using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.Common.Pagination;
using CarCareAlliance.Contracts.Tickets.Common;
using CarCareAlliance.Domain.TicketAggregate.Enums;

namespace CarCareAlliance.Application.Tickets.Queries.GetAllByFilters
{
    public record TicketGetAllByFiltersQuery(
        Guid UserId,
        RepairStatus? RepairStatus) : PagedQuery, IQuery<PagedResult<TicketDto>>;
}
