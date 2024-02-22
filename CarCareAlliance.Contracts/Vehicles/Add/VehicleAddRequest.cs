namespace CarCareAlliance.Contracts.Vehicles.Add
{
    public record VehicleAddRequest(
        string Brand,
        string Model,
        int Year,
        string Vin,
        string LicensePlate,
        Guid UserProfileId);
}