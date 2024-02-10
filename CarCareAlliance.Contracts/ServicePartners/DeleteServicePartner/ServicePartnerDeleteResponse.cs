namespace CarCareAlliance.Contracts.ServicePartners.DeleteServicePartner
{
    public record ServicePartnerDeleteResponse(
        Guid ServicePartnerId,
        string Name);
}
