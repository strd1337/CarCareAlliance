namespace CarCareAlliance.Application.ServicePartners.Common
{
    public record ServicePartnerDeleteResult(
        Guid ServicePartnerId,
        string Name);
}