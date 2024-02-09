namespace CarCareAlliance.Contracts.ServicePartners.AddServicePartner
{
    public record ServicePartnerAddResponse(
        Guid ServicePartnerId,
        string Name,
        string Description,
        Guid LogoId,
        Guid WorkScheduleId);
}