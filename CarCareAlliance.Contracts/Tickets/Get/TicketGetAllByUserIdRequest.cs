namespace CarCareAlliance.Contracts.Tickets.Get
{
    public record TicketGetAllByUserIdRequest(
        Guid UserId,
        Guid ServicePartnerId);
}
