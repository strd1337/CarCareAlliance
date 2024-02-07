using CarCareAlliance.Domain.MechanicAggregate;
using CarCareAlliance.Domain.MechanicAggregate.ValueObjects;
using CarCareAlliance.Domain.ServicePartnerAggregate.ValueObjects;
using CarCareAlliance.Domain.UserProfileAggregate.ValueObjects;
using CarCareAlliance.Domain.WorkScheduleAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarCareAlliance.Infrastructure.Persistance.Configurations
{
    public class MechanicConfiguration
        : IEntityTypeConfiguration<MechanicProfile>
    {
        public void Configure(EntityTypeBuilder<MechanicProfile> builder)
        {
            ConfigureMechanicProfileTable(builder);
            ConfigureMechanicProfileReviewIdsTable(builder);
        }

        private static void ConfigureMechanicProfileTable(
            EntityTypeBuilder<MechanicProfile> builder)
        {
            builder.ToTable("MechanicProfiles");

            builder.HasKey(mec => mec.Id);

            builder.Property(mec => mec.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => MechanicProfileId.Create(value));

            builder.Property(mec => mec.Experience)
                .HasColumnType("decimal(3, 1)");

            builder.Property(mec => mec.UserProfileId)
                .HasConversion(
                    id => id.Value,
                    value => UserProfileId.Create(value));

            builder.Property(mec => mec.ServicePartnerId)
                .HasConversion(
                    id => id.Value,
                    value => ServicePartnerId.Create(value));

            builder.Property(mec => mec.WorkScheduleId)
                .HasConversion(
                    id => id.Value,
                    value => WorkScheduleId.Create(value));
        }

        private static void ConfigureMechanicProfileReviewIdsTable(
            EntityTypeBuilder<MechanicProfile> builder)
        {
            builder.OwnsMany(mec => mec.ReviewIds, rib =>
            {
                rib.ToTable("MechanicProfileReviewIds");

                rib.WithOwner().HasForeignKey("MechanicProfileId");

                rib.HasKey("Id");

                rib.Property(ri => ri.Value)
                    .HasColumnName("ReviewId")
                    .ValueGeneratedNever();
            });
        }
    }
}