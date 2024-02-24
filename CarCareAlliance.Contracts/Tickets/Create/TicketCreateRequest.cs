namespace CarCareAlliance.Contracts.Tickets.Create
{
    public record TicketCreateRequest(
        Guid UserProfileId,
        Guid VehicleId,
        float Mileage,
        Guid ServicePartnerId,
        ICollection<Guid> ServiceIds,
        string OrderDetailsComments = "",
        string TicketDescription = "");
}
