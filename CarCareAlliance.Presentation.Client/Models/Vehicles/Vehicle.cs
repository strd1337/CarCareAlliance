namespace CarCareAlliance.Presentation.Client.Models.Vehicles
{
    public class Vehicle
    {
        public Guid VehicleId { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Vin { get; set; } = string.Empty;
        public string LicensePlate { get; set; } = string.Empty;
        public Guid UserProfileId { get; set; }
        public VehicleDetails Details { get; set; } = new();
    }
}