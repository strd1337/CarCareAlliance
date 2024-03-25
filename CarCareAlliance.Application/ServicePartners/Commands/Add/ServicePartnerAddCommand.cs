using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.ServicePartners.Common;
using CarCareAlliance.Contracts.ServicePartners.Common;

namespace CarCareAlliance.Application.ServicePartners.Commands.Add
{
    public record ServicePartnerAddCommand(
        string Name,
        string Description,
        ICollection<ServiceCategoryDto> ServiceCategories,
        ServicePartnerLocationDto Location) : ICommand<ServicePartnerAddResult>;
}