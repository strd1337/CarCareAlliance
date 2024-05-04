using CarCareAlliance.Domain.ServiceHistoryAggregate.Enums;
using CarCareAlliance.Domain.TicketAggregate.Enums;

namespace CarCareAlliance.Contracts.Tickets.Common
{
    public sealed class TicketDto
    {
        public Guid TicketId { get; set; }
        public string Description { get; set; } = default!;
        public DateTime DateSubmitted { get; set; }
        public RepairStatus RepairStatus { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string ServicePartnerName { get; set; } = default!;
        public string VehicleVin { get; set; } = default!;
        public OrderDetailsDto OrderDetails { get; set; } = default!;
        public Guid VehicleId { get; set; }
        public Guid UserProfileId { get; set; }
    }
}
