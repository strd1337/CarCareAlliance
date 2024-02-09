using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.ServicePartners.Common;

namespace CarCareAlliance.Application.ServicePartners.Commands.Delete
{
    public record ServicePartnerDeleteCommand(
        Guid ServicePartnerId) : ICommand<ServicePartnerDeleteResult>;
}
