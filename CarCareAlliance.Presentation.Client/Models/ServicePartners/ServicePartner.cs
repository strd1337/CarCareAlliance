namespace CarCareAlliance.Presentation.Client.Models.ServicePartners
{
    public class ServicePartner
    {
        public Guid ServicePartnerId { get; set; } 
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ServiceLocation ServiceLocation { get; set; } = null!;
    }
}
