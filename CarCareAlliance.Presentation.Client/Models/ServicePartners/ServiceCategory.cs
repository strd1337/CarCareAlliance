namespace CarCareAlliance.Presentation.Client.Models.ServicePartners
{
    public class ServiceCategory
    {
        public Guid ServiceCategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ICollection<Service> Services { get; set; } = null!;
    }
}