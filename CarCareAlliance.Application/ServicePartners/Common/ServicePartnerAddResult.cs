namespace CarCareAlliance.Application.ServicePartners.Common
{
    public record ServicePartnerAddResult(
        Guid ServicePartnerId,
        string Name,
        string Description,
        Guid LogoId,
        Guid WorkScheduleId);
}