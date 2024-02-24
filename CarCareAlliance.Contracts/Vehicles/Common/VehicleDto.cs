namespace CarCareAlliance.Contracts.Vehicles.Common
{
    public record VehicleDto(
        Guid VehicleId,
        string Brand,
        string Model,
        int Year,
        string Vin,
        string LicensePlate,
        Guid UserProfileId);
}
