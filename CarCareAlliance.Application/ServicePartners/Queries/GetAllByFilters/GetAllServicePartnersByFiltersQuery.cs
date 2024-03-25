using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.Common.Pagination;
using CarCareAlliance.Application.ServicePartners.Common;

namespace CarCareAlliance.Application.ServicePartners.Queries.GetAllByFilters
{
    public record GetAllServicePartnersByFiltersQuery(
        string SearchKey) : PagedQuery, IQuery<PagedResult<ServicePartnerResult>>;
}
