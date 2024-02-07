using CarCareAlliance.Domain.RepairAggregate;
using CarCareAlliance.Domain.RepairAggregate.ValueObjects;
using CarCareAlliance.Domain.ReviewAggregate.ValueObjects;
using CarCareAlliance.Domain.UserProfileAggregate.ValueObjects;
using CarCareAlliance.Domain.VehicleAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarCareAlliance.Infrastructure.Persistance.Configurations
{
    public class RepairConfiguration
        : IEntityTypeConfiguration<RepairHistory>
    {
        public void Configure(EntityTypeBuilder<RepairHistory> builder)
        {
            ConfigureRepairHistoryTable(builder);
            ConfigureRepairHistoryNotificationIdsTable(builder);
        }

        private static void ConfigureRepairHistoryTable(
            EntityTypeBuilder<RepairHistory> builder)
        {
            builder.ToTable("RepairHistories");

            builder.HasKey(rp => rp.Id);

            builder.Property(rp => rp.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => RepairHistoryId.Create(value));

            builder.Property(rp => rp.RepairStatus)
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.Property(rp => rp.Description)
                .HasMaxLength(300);

            builder.Property(rp => rp.UserProfileId)
                .HasConversion(
                    id => id.Value,
                    value => UserProfileId.Create(value));

            builder.Property(rp => rp.VehicleId)
                .HasConversion(
                    id => id.Value,
                    value => VehicleId.Create(value));

            builder.Property(rp => rp.ReviewId)
                .HasConversion(
                    id => id.Value,
                    value => ReviewId.Create(value));
        }

        private static void ConfigureRepairHistoryNotificationIdsTable(
            EntityTypeBuilder<RepairHistory> builder)
        {
            builder.OwnsMany(sm => sm.NotificationIds, nib =>
            {
                nib.ToTable("RepairHistoryNotificationIds");

                nib.WithOwner().HasForeignKey("RepairHistoryId");

                nib.HasKey("Id");

                nib.Property(ni => ni.Value)
                    .HasColumnName("NotificationId")
                    .ValueGeneratedNever();
            });
        }
    }
}