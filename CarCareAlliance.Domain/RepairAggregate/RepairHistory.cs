using CarCareAlliance.Domain.Common.Models;
using CarCareAlliance.Domain.NotificationAggregate.ValueObjects;
using CarCareAlliance.Domain.RepairAggregate.ValueObjects;
using CarCareAlliance.Domain.ReviewAggregate.ValueObjects;
using CarCareAlliance.Domain.TicketAggregate.Enums;
using CarCareAlliance.Domain.UserProfileAggregate.ValueObjects;
using CarCareAlliance.Domain.VehicleAggregate.ValueObjects;

namespace CarCareAlliance.Domain.RepairAggregate
{
    public sealed class RepairHistory : AggregateRoot<RepairHistoryId, Guid>
    {
        private readonly List<NotificationId> notificationIds = [];

        public DateTime Date { get; private set; }
        public RepairStatus RepairStatus { get; private set; }
        public string Description { get; private set; }
        public UserProfileId UserProfileId { get; private set; }
        public VehicleId VehicleId { get; private set; }
        public ReviewId ReviewId { get; private set; }

        public IReadOnlyList<NotificationId> NotificationIds => notificationIds.AsReadOnly();

        private RepairHistory(
            RepairHistoryId id,
            DateTime date,
            RepairStatus repairStatus,
            string description,
            UserProfileId userProfileId,
            VehicleId vehicleId,
            ReviewId reviewId) : base(id)
        {
            Date = date;
            RepairStatus = repairStatus;
            Description = description;
            UserProfileId = userProfileId;
            VehicleId = vehicleId;
            ReviewId = reviewId;
        }

        public static RepairHistory Create(
            DateTime date,
            RepairStatus repairStatus,
            string description,
            UserProfileId userProfileId,
            VehicleId vehicleId,
            ReviewId reviewId)
        {
            return new RepairHistory(
                RepairHistoryId.CreateUnique(),
                date,
                repairStatus,
                description,
                userProfileId,
                vehicleId,
                reviewId);
        }

        public void AddNotification(NotificationId notificationId)
        {
            notificationIds.Add(notificationId);
        }

#pragma warning disable CS8618
        private RepairHistory()
        {
        }
#pragma warning restore CS8618
    }
}
