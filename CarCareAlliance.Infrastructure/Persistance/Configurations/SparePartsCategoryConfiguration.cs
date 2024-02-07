using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CarCareAlliance.Domain.PartsCategoryAggregate;
using CarCareAlliance.Domain.PartsCategoryAggregate.ValueObjects;

namespace CarCareAlliance.Infrastructure.Persistance.Configurations
{
    public class SparePartsCategoryConfiguration
        : IEntityTypeConfiguration<SparePartsCategory>
    {
        public void Configure(EntityTypeBuilder<SparePartsCategory> builder)
        {
            builder.HasKey(spc => spc.Id);

            builder.Property(spc => spc.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => SparePartsCategoryId.Create(value));

            builder.Property(spc => spc.Name)
                .HasMaxLength(100);

            builder.Property(spc => spc.Description)
                .HasMaxLength(400); 
        }
    }
}
