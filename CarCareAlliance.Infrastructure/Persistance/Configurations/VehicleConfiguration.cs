using CarCareAlliance.Domain.UserProfileAggregate.ValueObjects;
using CarCareAlliance.Domain.VehicleAggregate;
using CarCareAlliance.Domain.VehicleAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarCareAlliance.Infrastructure.Persistance.Configurations
{
    public class VehicleConfiguration
        : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            ConfigureVehicleTable(builder);
            ConfigureVehiclePhotoIdsTable(builder);
        }

        private static void ConfigureVehicleTable(
            EntityTypeBuilder<Vehicle> builder)
        {
            builder.ToTable("Vehicles");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Id)
                .ValueGeneratedNever()
            .HasConversion(
                    id => id.Value,
                    value => VehicleId.Create(value));

            builder.Property(v => v.UserProfileId)
                .HasConversion(
                    id => id.Value,
                    value => UserProfileId.Create(value));

            builder.Property(v => v.Brand)
                .HasMaxLength(50);

            builder.Property(v => v.Model)
                .HasMaxLength(50);

            builder.Property(v => v.VIN)
                .HasMaxLength(50);

            builder.Property(v => v.LicensePlate)
                .HasMaxLength(50);
        }

        private static void ConfigureVehiclePhotoIdsTable(
            EntityTypeBuilder<Vehicle> builder)
        {
            builder.OwnsMany(v => v.PhotoIds, pib =>
            {
                pib.ToTable("VehiclePhotoIds");

                pib.WithOwner().HasForeignKey("VehicleId");

                pib.HasKey("Id");

                pib.Property(ni => ni.Value)
                    .HasColumnName("PhotoId")
                    .ValueGeneratedNever();
            });
        }
    }
}
