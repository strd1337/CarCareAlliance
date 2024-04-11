using CarCareAlliance.Contracts.ServicePartners.Common;

namespace CarCareAlliance.Contracts.ServicePartners.GetAllServicePartnersCategories
{
    public record GetAllServicePartnersCategoriesResponse(
        ICollection<ServiceCategoryDto> ServiceCategories);
}
