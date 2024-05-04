namespace CarCareAlliance.Presentation.Client.Models.Tickets
{
    public class CreateTicketRequest
    {
        public Guid UserProfileId { get; set; }
        public Guid VehicleId { get; set; }
        public float Mileage { get; set; }
        public Guid ServicePartnerId { get; set; }
        public string OrderDetailsComments { get; set; } = default!;
        public string TicketDescription { get; set; } = default!;
        public ICollection<Guid> ServiceIds { get; set; } = default!;
        public Guid AssignedMechanicId { get; set; }
    }
}