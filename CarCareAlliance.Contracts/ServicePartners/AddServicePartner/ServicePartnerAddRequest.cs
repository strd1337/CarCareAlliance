using CarCareAlliance.Contracts.ServicePartners.Common;

namespace CarCareAlliance.Contracts.ServicePartners.AddServicePartner
{
    public record ServicePartnerAddRequest(
        string Name,
        string Description,
        Guid LogoId,
        Guid WorkScheduleId,
        ServicePartnerLocationDto Location);
}