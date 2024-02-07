using CarCareAlliance.Domain.ExpenseHistoryAggregate;
using CarCareAlliance.Domain.ExpenseHistoryAggregate.ValueObjects;
using CarCareAlliance.Domain.PhotoAggregate.ValueObjects;
using CarCareAlliance.Domain.UserProfileAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarCareAlliance.Infrastructure.Persistance.Configurations
{
    public class ExpenseConfiguration
        : IEntityTypeConfiguration<ExpenseHistory>
    {
        public void Configure(EntityTypeBuilder<ExpenseHistory> builder)
        {
            builder.HasKey(exp => exp.Id);

            builder.Property(exp => exp.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => ExpenseHistoryId.Create(value));

            builder.Property(exp => exp.Amount)
                .HasColumnType("decimal(10, 2)");

            builder.Property(exp => exp.Description)
                .HasMaxLength(300);

            builder.Property(exp => exp.UserProfileId)
                .HasConversion(
                    id => id.Value,
                    value => UserProfileId.Create(value));

            builder.Property(exp => exp.ReceiptImage)
                .HasConversion(
                    id => id.Value,
                    value => PhotoId.Create(value));
        }
    }
}