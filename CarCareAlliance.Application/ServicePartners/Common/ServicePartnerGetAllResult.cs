using CarCareAlliance.Domain.ServicePartnerAggregate;

namespace CarCareAlliance.Application.ServicePartners.Common
{
    public record ServicePartnerGetAllResult(
        ICollection<ServicePartner> ServicePartners);
}