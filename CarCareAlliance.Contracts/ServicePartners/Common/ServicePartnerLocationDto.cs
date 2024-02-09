namespace CarCareAlliance.Contracts.ServicePartners.Common
{
    public record ServicePartnerLocationDto(
        float Latitude,
        float Longitude,
        string Address,
        string City,
        string? State,
        string Country,
        string PostalCode,
        string Description);
}