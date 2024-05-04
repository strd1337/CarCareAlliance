using CarCareAlliance.Presentation.Client.Models.Vehicles;

namespace CarCareAlliance.Presentation.Client.Models.Tickets
{
    public class Ticket
    {
        public Guid TicketId { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime DateSubmitted { get; set; }
        public RepairStatus RepairStatus { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public Guid UserProfileId { get; set; }
        public Guid VehicleId { get; set; }
        public OrderDetails OrderDetails { get; set; } = new();
        public string ServicePartnerName { get; set; } = default!;
        public string VehicleVin { get; set; } = default!;
    }
}