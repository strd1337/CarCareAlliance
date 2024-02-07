using CarCareAlliance.Domain.PartsCategoryAggregate.ValueObjects;
using CarCareAlliance.Domain.SparePartAggregate;
using CarCareAlliance.Domain.SparePartAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarCareAlliance.Infrastructure.Persistance.Configurations
{
    public class SparePartConfiguration
        : IEntityTypeConfiguration<SparePart>
    {
        public void Configure(EntityTypeBuilder<SparePart> builder)
        {
            builder.HasKey(sp => sp.Id);

            builder.Property(sp => sp.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => SparePartId.Create(value));

            builder.Property(sp => sp.SparePartsCategoryId)
                .HasConversion(
                    id => id.Value,
                    value => SparePartsCategoryId.Create(value));

            builder.Property(sp => sp.Name)
                .HasMaxLength(100);

            builder.Property(sp => sp.Description)
                .HasMaxLength(500);

            builder.Property(sp => sp.Manufacturer)
                .HasMaxLength(100);

            builder.Property(sp => sp.Price)
                .HasColumnType("decimal(10, 2)");
        }
    }
}