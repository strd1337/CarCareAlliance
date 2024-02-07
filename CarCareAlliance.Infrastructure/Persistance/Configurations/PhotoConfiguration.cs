using CarCareAlliance.Domain.PhotoAggregate;
using CarCareAlliance.Domain.PhotoAggregate.ValueObjects;
using CarCareAlliance.Domain.ServicePartnerAggregate.ValueObjects;
using CarCareAlliance.Domain.UserProfileAggregate.ValueObjects;
using CarCareAlliance.Domain.VehicleAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarCareAlliance.Infrastructure.Persistance.Configurations
{
    public class PhotoConfiguration
        : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => PhotoId.Create(value));

            builder.Property(p => p.Format)
               .HasMaxLength(50);

            builder.Property(p => p.Description)
                .HasMaxLength(500);

            builder.Property(p => p.ImageData)
                .IsRequired()
                .HasColumnType("VARBINARY(MAX)");

            builder.Property(p => p.UserProfileId)
                .HasConversion(
                    id => id.Value,
                    value => UserProfileId.Create(value));

            builder.Property(p => p.VehicleId)
                .HasConversion(
                    id => id.Value,
                    value => VehicleId.Create(value));

            builder.Property(p => p.ServicePartnerId)
                .HasConversion(
                    id => id.Value,
                    value => ServicePartnerId.Create(value));
        }
    }
}
