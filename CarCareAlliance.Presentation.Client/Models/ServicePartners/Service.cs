namespace CarCareAlliance.Presentation.Client.Models.ServicePartners
{
    public class Service
    {
        public Guid ServiceId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public float Price { get; set; } 
        public float Duration { get; set; }
    }
}