using CarCareAlliance.Domain.WorkScheduleAggregate;
using CarCareAlliance.Domain.WorkScheduleAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarCareAlliance.Infrastructure.Persistance.Configurations
{
    public class WorkScheduleConfiguration
        : IEntityTypeConfiguration<WorkSchedule>
    {
        public void Configure(EntityTypeBuilder<WorkSchedule> builder)
        {
            builder.HasKey(ws => ws.Id);

            builder.Property(ws => ws.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => WorkScheduleId.Create(value));

            builder.Property(e => e.DayOfWeek)
                .HasColumnName("DayOfWeek")
                .HasConversion(
                    v => v.ToString(),
                    v => Enum.Parse<DayOfWeek>(v)
                );

            builder.Property(ws => ws.StartTime)
                .HasColumnName("StartTime")
                .HasColumnType("time");

            builder.Property(ws => ws.EndTime)
                .HasColumnName("EndTime")
                .HasColumnType("time");

            builder.OwnsMany(ws => ws.BreakTimes, bt =>
            {
                bt.ToTable("BreakTimes");

                bt.WithOwner().HasForeignKey("WorkScheduleId");

                bt.HasKey("Id");

                bt.Property(b => b.StartTime)
                    .HasColumnName("StartTime")
                    .HasColumnType("time");

                bt.Property(b => b.EndTime)
                    .HasColumnName("EndTime")
                    .HasColumnType("time");
            });

            builder.Property(e => e.Weekends)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(d => Enum.Parse<DayOfWeek>(d)).ToList()
                );
        }
    }
}