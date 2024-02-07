using CarCareAlliance.Domain.MaintenanceAggregate.ValueObjects;
using CarCareAlliance.Domain.NotificationAggregate;
using CarCareAlliance.Domain.NotificationAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarCareAlliance.Infrastructure.Persistance.Configurations
{
    public class NotificationConfiguration
        : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(n => n.Id);

            builder.Property(n => n.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => NotificationId.Create(value));

            builder.Property(n => n.TextMessage)
                .HasMaxLength(2000);

            builder.Property(n => n.ScheduledMaintenanceId)
                .HasConversion(
                    id => id.Value,
                    value => ScheduledMaintenanceId.Create(value));
        }
    }
}
