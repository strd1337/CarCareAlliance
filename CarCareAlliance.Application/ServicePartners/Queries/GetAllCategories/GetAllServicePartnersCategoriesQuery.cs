using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.ServicePartners.Common;

namespace CarCareAlliance.Application.ServicePartners.Queries.GetAllCategories
{
    public record GetAllServicePartnersCategoriesQuery() : 
        IQuery<GetAllServicePartnersCategoriesResult>;
}
