namespace CarCareAlliance.Presentation.Client.Models.ServicePartners
{
    public class ServiceLocation
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? State { get; set; }
    }
}
