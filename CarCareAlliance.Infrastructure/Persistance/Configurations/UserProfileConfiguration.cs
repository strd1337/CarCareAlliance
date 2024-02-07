using CarCareAlliance.Domain.PhotoAggregate.ValueObjects;
using CarCareAlliance.Domain.UserProfileAggregate;
using CarCareAlliance.Domain.UserProfileAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarCareAlliance.Infrastructure.Persistance.Configurations
{
    public class UserProfileConfiguration
        : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            ConfigureUserProfileTable(builder);
            ConfigureUserProfileReviewIdsTable(builder);
        }

        private static void ConfigureUserProfileTable(
            EntityTypeBuilder<UserProfile> builder)
        {
            builder.ToTable("UserProfiles");

            builder.HasKey(up => up.Id);

            builder.Property(up => up.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => UserProfileId.Create(value));

            builder.Property(up => up.PhotoId)
                .HasConversion(
                    id => id.Value,
                    value => PhotoId.Create(value));

            builder.OwnsOne(up => up.Information, ib =>
            {
                ib.Property(i => i.FirstName)
                    .HasColumnName("FirstName")
                    .HasMaxLength(50);

                ib.Property(i => i.LastName)
                    .HasColumnName("LastName")
                    .HasMaxLength(50);

                ib.Property(i => i.PhoneNumber)
                    .HasColumnName("PhoneNumber")
                    .HasMaxLength(30);

                ib.Property(i => i.Address)
                    .HasColumnName("Address")
                    .HasMaxLength(200);

                ib.Property(i => i.Country)
                    .HasColumnName("Country")
                    .HasMaxLength(50);

                ib.Property(i => i.City)
                    .HasColumnName("City")
                    .HasMaxLength(50);

                ib.Property(i => i.PostCode)
                    .HasColumnName("PostCode")
                    .HasMaxLength(15);
            });

            builder.Property(r => r.RoleType)
                .HasConversion<string>()
                .HasMaxLength(50);
        }

        private static void ConfigureUserProfileReviewIdsTable(
            EntityTypeBuilder<UserProfile> builder)
        {
            builder.OwnsMany(sp => sp.ReviewIds, pib =>
            {
                pib.ToTable("UserProfileReviewIds");

                pib.WithOwner().HasForeignKey("UserProfileId");

                pib.HasKey("Id");

                pib.Property(ni => ni.Value)
                    .HasColumnName("ReviewId")
                    .ValueGeneratedNever();
            });
        }
    }
}