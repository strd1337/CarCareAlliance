using CarCareAlliance.Domain.ServicePartnerAggregate.Entities;

namespace CarCareAlliance.Application.ServicePartners.Common
{
    public record GetAllServicePartnersCategoriesResult(
        ICollection<ServiceCategory> ServiceCategories);
}
