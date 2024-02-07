using CarCareAlliance.Domain.ReviewAggregate;
using CarCareAlliance.Domain.ReviewAggregate.ValueObjects;
using CarCareAlliance.Domain.UserProfileAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarCareAlliance.Infrastructure.Persistance.Configurations
{
    public class ReviewConfiguration
        : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => ReviewId.Create(value));

            builder.Property(r => r.ObjectType)
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.Property(r => r.Text)
                .HasMaxLength(300);

            builder.Property(r => r.Rating)
                .HasColumnType("decimal(3, 1)");

            builder.Property(r => r.UserProfileId)
                .HasConversion(
                    id => id.Value,
                    value => UserProfileId.Create(value));
        }
    }
}