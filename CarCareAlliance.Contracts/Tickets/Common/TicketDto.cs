using CarCareAlliance.Domain.ServiceHistoryAggregate.Enums;
using CarCareAlliance.Domain.TicketAggregate.Enums;

namespace CarCareAlliance.Contracts.Tickets.Common
{
    public record TicketDto(
        Guid TicketId,
        string Description,
        DateTime DateSubmitted,
        RepairStatus RepairStatus,
        PaymentStatus PaymentStatus,
        Guid UserProfileId,
        Guid VehicleId,
        OrderDetailsDto OrderDetails);
}
