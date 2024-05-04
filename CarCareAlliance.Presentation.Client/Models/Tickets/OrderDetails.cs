using CarCareAlliance.Presentation.Client.Models.ServicePartners;

namespace CarCareAlliance.Presentation.Client.Models.Tickets
{
    public class OrderDetails
    {
        public Guid OrderDetailsId { get; set; }
        public float Mileage { get; set; }
        public string Comments { get; set; } = string.Empty;
        public float FinalPrice { get; set; }
        public float PrepaymentAmount { get; set; }
        public ICollection<Guid> ServiceIds { get; set; } = default!;
        public ICollection<Service> Services { get; set; } = default!;
    }
}