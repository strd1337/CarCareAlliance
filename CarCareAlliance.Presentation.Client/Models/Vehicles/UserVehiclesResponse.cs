namespace CarCareAlliance.Presentation.Client.Models.Vehicles
{
    public class UserVehiclesResponse
    {
        public ICollection<Vehicle> Vehicles { get; set; } = default!;
    }
}
