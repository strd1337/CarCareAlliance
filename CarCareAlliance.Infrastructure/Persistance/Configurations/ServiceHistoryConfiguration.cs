using CarCareAlliance.Domain.ServiceHistoryAggregate;
using CarCareAlliance.Domain.ServiceHistoryAggregate.ValueObjects;
using CarCareAlliance.Domain.ServicePartnerAggregate.ValueObjects;
using CarCareAlliance.Domain.TicketAggregate.ValueObjects;
using CarCareAlliance.Domain.UserProfileAggregate.ValueObjects;
using CarCareAlliance.Domain.VehicleAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarCareAlliance.Infrastructure.Persistance.Configurations
{
    public class ServiceHistoryConfiguration
        : IEntityTypeConfiguration<ServiceHistory>
    {
        public void Configure(EntityTypeBuilder<ServiceHistory> builder)
        {
            ConfigureServiceHistoryTable(builder);
            ConfigureServiceHistoryTicketsTable(builder);
        }

        private static void ConfigureServiceHistoryTable(
            EntityTypeBuilder<ServiceHistory> builder)
        {
            builder.ToTable("ServiceHistories");

            builder.HasKey(sh => sh.Id);

            builder.Property(sh => sh.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => ServiceHistoryId.Create(value));

            builder.Property(sh => sh.ServicePartnerId)
                .HasConversion(
                    id => id.Value,
                    value => ServicePartnerId.Create(value));
        }

        private static void ConfigureServiceHistoryTicketsTable(
            EntityTypeBuilder<ServiceHistory> builder)
        {
            builder.OwnsMany(sh => sh.Tickets, tb =>
            {
                tb.ToTable("ServiceHistoryTickets");

                tb.WithOwner().HasForeignKey("ServiceHistoryId");

                tb.HasKey("Id", "ServiceHistoryId");

                tb.Property(mt => mt.Id)
                    .HasColumnName("ServiceHistoryTicketId")
                    .ValueGeneratedNever()
                    .HasConversion(
                        id => id.Value,
                        value => TicketId.Create(value));

                tb.Property(mt => mt.Description)
                    .HasMaxLength(100);

                tb.Property(rp => rp.RepairStatus)
                    .HasConversion<string>()
                    .HasMaxLength(50);

                tb.Property(rp => rp.PaymentStatus)
                    .HasConversion<string>()
                    .HasMaxLength(50);

                tb.Property(exp => exp.UserProfileId)
                    .HasConversion(
                        id => id.Value,
                        value => UserProfileId.Create(value));

                tb.Property(exp => exp.VehicleId)
                    .HasConversion(
                        id => id.Value,
                        value => VehicleId.Create(value));

                tb.Navigation(o => o.OrderDetails)
                    .UsePropertyAccessMode(PropertyAccessMode.Field);
            });

            builder.Metadata.FindNavigation(nameof(ServiceHistory.Tickets))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}