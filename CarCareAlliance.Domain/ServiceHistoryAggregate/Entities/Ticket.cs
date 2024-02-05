using CarCareAlliance.Domain.Common.Models;
using CarCareAlliance.Domain.ServiceHistoryAggregate.Enums;
using CarCareAlliance.Domain.TicketAggregate.Enums;
using CarCareAlliance.Domain.TicketAggregate.ValueObjects;
using CarCareAlliance.Domain.UserProfileAggregate.ValueObjects;
using CarCareAlliance.Domain.VehicleAggregate.ValueObjects;

namespace CarCareAlliance.Domain.ServiceHistoryAggregate.Entities
{
    public sealed class Ticket : Entity<TicketId>
    {
        public string Description { get; private set; }
        public DateTime DateSubmitted { get; private set; }
        public RepairStatus RepairStatus { get; private set; }
        public PaymentStatus PaymentStatus { get; private set; }
        public UserProfileId UserProfileId { get; private set; }
        public VehicleId VehicleId { get; private set; }
        public OrderDetailsId OrderDetailsId { get; private set; }

        private Ticket(
            TicketId id,
            string description,
            DateTime dateSubmitted,
            RepairStatus repairStatus,
            PaymentStatus paymentStatus,
            UserProfileId userProfileId,
            VehicleId vehicleId,
            OrderDetailsId orderDetailsId) : base(id)
        {
            Description = description;
            DateSubmitted = dateSubmitted;
            RepairStatus = repairStatus;
            PaymentStatus = paymentStatus;
            UserProfileId = userProfileId;
            VehicleId = vehicleId;
            OrderDetailsId = orderDetailsId;
        }

        public static Ticket Create(
            string description,
            DateTime dateSubmitted,
            RepairStatus repairStatus,
            PaymentStatus paymentStatus,
            UserProfileId userProfileId,
            VehicleId vehicleId,
            OrderDetailsId orderDetailsId)
        {
            return new(
                TicketId.CreateUnique(),
                description,
                dateSubmitted,
                repairStatus,
                paymentStatus,
                userProfileId,
                vehicleId,
                orderDetailsId);
        }
    }
}
