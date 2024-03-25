namespace CarCareAlliance.Contracts.ServicePartners.Common
{
    public record ServiceDto(
        Guid ServiceId,
        string Name,
        string Description,
        float Price,
        float Duration);
}
