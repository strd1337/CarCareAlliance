using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.ServicePartners.Common;
using CarCareAlliance.Contracts.ServicePartners.Common;
using CarCareAlliance.Contracts.WorkSchedules.Common;

namespace CarCareAlliance.Application.ServicePartners.Commands.Update
{
    public record ServicePartnerUpdateCommand(
        Guid ServicePartnerId,
        string Name,
        string Description,
        ICollection<ServiceCategoryDto> ServiceCategories,
        ServicePartnerLocationDto Location,
        ICollection<WorkScheduleDto> WorkSchedules) : ICommand<ServicePartnerUpdateResult>
    {
        public ServicePartnerUpdateCommand SetServicePartnerId(Guid ServicePartnerId) 
            => this with { ServicePartnerId = ServicePartnerId };
    }
}
