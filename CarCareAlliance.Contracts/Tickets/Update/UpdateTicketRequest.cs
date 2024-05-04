using CarCareAlliance.Domain.TicketAggregate.Enums;

namespace CarCareAlliance.Contracts.Tickets.Update
{
    public sealed class UpdateTicketRequest
    {
        public RepairStatus RepairStatus { get; set; }
        public string Comments { get; set; } = default!;
    }
}
