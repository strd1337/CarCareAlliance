using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CarCareAlliance.Domain.AuthenticationAggregate;
using CarCareAlliance.Domain.AuthenticationAggregate.ValueObjects;
using CarCareAlliance.Domain.UserProfileAggregate.ValueObjects;

namespace CarCareAlliance.Infrastructure.Persistance.Configurations
{
    public class AuthenticationConfiguration
        : IEntityTypeConfiguration<Authentication>
    {
        public void Configure(EntityTypeBuilder<Authentication> builder)
        {
            builder.HasKey(auth => auth.Id);

            builder.Property(auth => auth.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => AuthenticationId.Create(value));

            builder.Property(auth => auth.UserName)
                .HasMaxLength(50);

            builder.Property(auth => auth.Email)
                .HasMaxLength(100);

            builder.Property(auth => auth.PasswordHash)
                .HasMaxLength(500);

            builder.Property(auth => auth.Salt)
                .HasMaxLength(500);

            builder.Property(auth => auth.UserProfileId)
                .HasConversion(
                    id => id.Value,
                    value => UserProfileId.Create(value));
        }
    }
}
