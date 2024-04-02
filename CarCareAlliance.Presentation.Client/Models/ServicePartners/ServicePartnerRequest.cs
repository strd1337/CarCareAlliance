using CarCareAlliance.Presentation.Client.Models.Mechanics;
using CarCareAlliance.Presentation.Client.Models.WorkSchedules;

namespace CarCareAlliance.Presentation.Client.Models.ServicePartners
{
    public class ServicePartnerRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ICollection<ServiceCategory> ServiceCategories { get; set; } = null!;
        public ServiceLocation Location { get; set; } = null!;
        public ICollection<MechanicProfile> Mechanics { get; set; } = null!;
        public ICollection<WorkSchedule> WorkSchedules { get; set; } = null!;
    }
}
