using CarCareAlliance.Presentation.Client.Models.Vehicles;

namespace CarCareAlliance.Presentation.Client.Models.Tickets
{
    public class UpdateTicketRequest
    {
        public Guid TicketId { get; set; }
        public RepairStatus RepairStatus { get; set; }
        public string Comments { get; set; } = default!;
    }
}
