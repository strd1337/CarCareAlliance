namespace CarCareAlliance.Contracts.ServicePartners.Common
{
    public record ServicePartnerDto(
        Guid ServicePartnerId,
        string Name,
        string Description,
        Guid LogoId,
        Guid WorkScheduleId,
        ServicePartnerLocationDto Location);
}