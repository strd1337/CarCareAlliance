using CarCareAlliance.Contracts.ServicePartners.Common;

namespace CarCareAlliance.Contracts.Tickets.Common
{
    public sealed class OrderDetailsDto
    {
        public Guid OrderDetailsId { get; set; }
        public float Mileage { get; set; }
        public string Comments { get; set; } = default!;
        public float FinalPrice { get; set; }
        public float PrepaymentAmount { get; set; }
        public ICollection<ServiceDto> Services { get; set; } = default!;
        public ICollection<Guid> ServiceIds { get; set; } = default!;
    }
}