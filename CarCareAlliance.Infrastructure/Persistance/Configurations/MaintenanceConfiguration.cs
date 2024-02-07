using CarCareAlliance.Domain.MaintenanceAggregate;
using CarCareAlliance.Domain.MaintenanceAggregate.ValueObjects;
using CarCareAlliance.Domain.VehicleAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarCareAlliance.Infrastructure.Persistance.Configurations
{
    public class MaintenanceConfiguration
        : IEntityTypeConfiguration<ScheduledMaintenance>
    {
        public void Configure(EntityTypeBuilder<ScheduledMaintenance> builder)
        {
            ConfigureScheduledMaintenanceTable(builder);
            ConfigureScheduledMaintenanceNotificationIdsTable(builder);
            ConfigureScheduledMaintenanceTypesTable(builder);
        }

        private static void ConfigureScheduledMaintenanceTable(
            EntityTypeBuilder<ScheduledMaintenance> builder)
        {
            builder.ToTable("ScheduledMaintenances");

            builder.HasKey(sm => sm.Id);

            builder.Property(sm => sm.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => ScheduledMaintenanceId.Create(value));

            builder.Property(sm => sm.VehicleId)
                .HasConversion(
                    id => id.Value,
                    value => VehicleId.Create(value));
        }
        
        private static void ConfigureScheduledMaintenanceNotificationIdsTable(
            EntityTypeBuilder<ScheduledMaintenance> builder)
        {
            builder.OwnsMany(sm => sm.NotificationIds, nib =>
            {
                nib.ToTable("ScheduledMaintenanceNotificationIds");

                nib.WithOwner().HasForeignKey("ScheduledMaintenanceId");

                nib.HasKey("Id");

                nib.Property(ni => ni.Value)
                    .HasColumnName("NotificationId")
                    .ValueGeneratedNever();
            });
        }

        private static void ConfigureScheduledMaintenanceTypesTable(
            EntityTypeBuilder<ScheduledMaintenance> builder)
        {
            builder.OwnsMany(sm => sm.MaintenanceTypes, mtb =>
            {
                mtb.ToTable("ScheduledMaintenanceTypes");

                mtb.WithOwner().HasForeignKey("ScheduledMaintenanceId");

                mtb.HasKey("Id", "ScheduledMaintenanceId");

                mtb.Property(mt => mt.Id)
                    .HasColumnName("ScheduledMaintenanceTypeId")
                    .ValueGeneratedNever()
                    .HasConversion(
                        id => id.Value,
                        value => MaintenanceTypeId.Create(value));

                mtb.Property(mt => mt.Name)
                    .HasMaxLength(100);

                mtb.Property(mt => mt.Description)
                    .HasMaxLength(300);
            });

            builder.Metadata.FindNavigation(nameof(ScheduledMaintenance.MaintenanceTypes))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}