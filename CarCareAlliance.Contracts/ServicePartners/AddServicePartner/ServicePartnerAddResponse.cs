namespace CarCareAlliance.Contracts.ServicePartners.AddServicePartner
{
    public record ServicePartnerAddResponse(
        Guid ServicePartnerId,
        string Name);
}