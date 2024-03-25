using CarCareAlliance.Contracts.Staff.Common;
using CarCareAlliance.Contracts.WorkSchedules.Common;

namespace CarCareAlliance.Contracts.ServicePartners.Common
{
    public record ServicePartnerDto(
        Guid ServicePartnerId,
        string Name,
        string Description,
        ICollection<ServiceCategoryDto> ServiceCategories,
        ServicePartnerLocationDto Location,
        ICollection<MechanicDto> Mechanics,
        ICollection<WorkScheduleDto> WorkSchedules);
}