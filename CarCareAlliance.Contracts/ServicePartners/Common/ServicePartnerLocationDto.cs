﻿namespace CarCareAlliance.Contracts.ServicePartners.Common
{
    public record ServicePartnerLocationDto(
        Guid LocationId,
        float Latitude,
        float Longitude,
        string Address,
        string City,
        string Country,
        string PostalCode,
        string Description,
        string? State = null);
}