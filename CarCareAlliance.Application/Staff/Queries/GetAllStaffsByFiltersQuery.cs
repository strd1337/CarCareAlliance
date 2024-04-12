using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.Common.Pagination;
using CarCareAlliance.Application.Staff.Common;

namespace CarCareAlliance.Application.Staff.Queries
{
    public record GetAllStaffsByFiltersQuery(
        string SearchKey) : PagedQuery, IQuery<PagedResult<GetAllStaffsByFiltersResult>>;
}
