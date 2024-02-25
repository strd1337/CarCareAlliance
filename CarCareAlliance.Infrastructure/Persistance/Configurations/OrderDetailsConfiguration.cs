using CarCareAlliance.Domain.MechanicAggregate.ValueObjects;
using CarCareAlliance.Domain.ServiceHistoryAggregate;
using CarCareAlliance.Domain.ServiceHistoryAggregate.Entities;
using CarCareAlliance.Domain.TicketAggregate.Entities;
using CarCareAlliance.Domain.TicketAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarCareAlliance.Infrastructure.Persistance.Configurations
{
    public class OrderDetailsConfiguration
        : IEntityTypeConfiguration<OrderDetails>
    {
        public void Configure(EntityTypeBuilder<OrderDetails> builder)
        {
            builder.HasKey(od => od.Id);

            builder.Property(od => od.Id)
                .HasColumnName("OrderDetailsId")
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => OrderDetailsId.Create(value));

            builder.Property(od => od.Mileage)
                .HasColumnName("Mileage")
                .HasColumnType("decimal(10, 2)");

            builder.Property(od => od.FinalPrice)
                .HasColumnName("FinalPrice")
                .HasColumnType("decimal(10, 2)");

            builder.Property(od => od.PrepaymentAmount)
                .HasColumnName("PrepaymentAmount")
                .HasColumnType("decimal(10, 2)");

            builder.Property(od => od.Comments)
                .HasColumnName("Comments")
                .HasMaxLength(300);

            builder.Property(od => od.AssignedMechanicId)
                .HasConversion(
                    id => id.Value,
                    value => MechanicProfileId.Create(value));

            builder.OwnsMany(od => od.SparePartIds, spb =>
            {
                spb.ToTable("OrderDetailsSparePartIds");

                spb.WithOwner().HasForeignKey("OrderDetailsId");

                spb.HasKey("Id");

                spb.Property(sp => sp.Value)
                    .HasColumnName("SparePartId")
                    .ValueGeneratedNever();
            });

            builder.OwnsMany(od => od.ServiceIds, sb =>
            {
                sb.ToTable("OrderDetailsServiceIds");

                sb.WithOwner().HasForeignKey("OrderDetailsId");

                sb.HasKey("Id");

                sb.Property(s => s.Value)
                    .HasColumnName("ServiceId")
                    .ValueGeneratedNever();
            });
        }
    }
}
