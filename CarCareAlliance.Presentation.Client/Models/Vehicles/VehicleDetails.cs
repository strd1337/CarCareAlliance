namespace CarCareAlliance.Presentation.Client.Models.Vehicles
{
    public class VehicleDetails
    {
        public VehicleEngineDetails Engine { get; set; } = default!;
        public VehicleTransmissionDetails Transmission { get; set; } = default!;
        public string DrivenWheels { get; set; } = string.Empty;
    }
}
