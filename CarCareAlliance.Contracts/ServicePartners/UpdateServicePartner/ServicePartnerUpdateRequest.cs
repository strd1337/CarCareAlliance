using CarCareAlliance.Contracts.ServicePartners.Common;
using CarCareAlliance.Contracts.WorkSchedules.Common;

namespace CarCareAlliance.Contracts.ServicePartners.UpdateServicePartner
{
    public record ServicePartnerUpdateRequest(
        string Name,
        string Description,
        ICollection<ServiceCategoryDto> ServiceCategories,
        ServicePartnerLocationDto Location,
        ICollection<WorkScheduleDto> WorkSchedules);
}
