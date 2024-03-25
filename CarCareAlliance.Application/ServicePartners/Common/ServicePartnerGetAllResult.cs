namespace CarCareAlliance.Application.ServicePartners.Common
{
    public record ServicePartnerGetAllResult(
        ICollection<ServicePartnerResult> ServicePartners);
}