namespace CarCareAlliance.Contracts.ServicePartners.UpdateServicePartner
{
    public record ServicePartnerUpdateResponse(
        Guid ServicePartnerId,
        string Name);
}
