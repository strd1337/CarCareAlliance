using CarCareAlliance.Contracts.ServicePartners.Common;

namespace CarCareAlliance.Contracts.ServicePartners.GetAll
{
    public record ServicePartnerGetAllResponse(
        ICollection<ServicePartnerDto> ServicePartners);
}
