using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.Tickets.Common;

namespace CarCareAlliance.Application.Tickets.Commands.Create
{
    public record TicketCreateCommand(
        Guid UserProfileId,
        Guid VehicleId,
        float Mileage,
        Guid ServicePartnerId,
        ICollection<Guid> ServiceIds,
        string OrderDetailsComments = "",
        string TicketDescription = "") : ICommand<TicketCreateResult>;
}
