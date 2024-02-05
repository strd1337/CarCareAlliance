using CarCareAlliance.Domain.Common.Models;
using CarCareAlliance.Domain.MaintenanceAggregate.ValueObjects;
using CarCareAlliance.Domain.NotificationAggregate.ValueObjects;

namespace CarCareAlliance.Domain.NotificationAggregate
{
    public sealed class Notification : AggregateRoot<NotificationId, Guid>
    {
        public string TextMessage { get; private set; }
        public DateTime DateTimeSent { get; private set; }
        public bool IsRead { get; private set; } = false;
        public ScheduledMaintenanceId ScheduledMaintenanceId { get; private set; }
        
        private Notification(
            NotificationId id,
            string textMessage,
            DateTime dateTimeSent,
            ScheduledMaintenanceId scheduledMaintenanceId) : base(id)
        {
            TextMessage = textMessage;
            DateTimeSent = dateTimeSent;
            ScheduledMaintenanceId = scheduledMaintenanceId;
        }

        public static Notification Create(
            string textMessage,
            DateTime dateTimeSent,
            ScheduledMaintenanceId scheduledMaintenanceId)
        {
            return new Notification(
                NotificationId.CreateUnique(),
                textMessage,
                dateTimeSent,
                scheduledMaintenanceId);
        }

        public void MakeAsRead()
        {
            IsRead = true;
        }

#pragma warning disable CS8618
        private Notification()
        {
        }
#pragma warning restore CS8618
    }
}
