using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.Common.Pagination;
using CarCareAlliance.Application.Vehicles.Common;

namespace CarCareAlliance.Application.Vehicles.Queries.GetAllByUserIdAndFilters
{
    public record GetAllByUserIdAndFiltersQuery(
        Guid UserId) : PagedQuery, IQuery<PagedResult<VehicleGetResult>>;
}
