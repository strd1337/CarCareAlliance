using CarCareAlliance.Domain.Common.Models;
using CarCareAlliance.Domain.MaintenanceAggregate.ValueObjects;
using CarCareAlliance.Domain.NotificationAggregate.ValueObjects;
using CarCareAlliance.Domain.VehicleAggregate.ValueObjects;

namespace CarCareAlliance.Domain.MaintenanceAggregate
{
    public sealed class ScheduledMaintenance : AggregateRoot<ScheduledMaintenanceId, Guid>
    {
        private readonly List<NotificationId> notificationIds = [];
        private readonly List<MaintenanceTypeId> maintenanceTypeIds = [];

        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public VehicleId VehicleId { get; private set; }

        public IReadOnlyList<NotificationId> NotificationIds =>
            notificationIds.AsReadOnly();

        public IReadOnlyList<MaintenanceTypeId> MaintenanceTypeIds =>
            maintenanceTypeIds.AsReadOnly();

        private ScheduledMaintenance(
            ScheduledMaintenanceId id,
            DateTime startDate,
            DateTime endDate,
            VehicleId vehicleId) : base(id)
        {
            StartDate = startDate;
            EndDate = endDate;
            VehicleId = vehicleId;
        }

        public static ScheduledMaintenance Create(
            DateTime startDate,
            DateTime endDate,
            VehicleId vehicleId)
        {
            return new ScheduledMaintenance(
                ScheduledMaintenanceId.CreateUnique(),
                startDate,
                endDate,
                vehicleId);
        }

#pragma warning disable CS8618
        private ScheduledMaintenance()
        {
        }
#pragma warning restore CS8618
    }
}
