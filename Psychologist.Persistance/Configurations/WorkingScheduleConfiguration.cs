using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PsychologistSystem.Domain.Entity;

namespace PsychologistSystem.Persistance.Configurations
{
    public sealed class WorkingScheduleConfiguration : IEntityTypeConfiguration<WorkingSchedule>
    {
        public void Configure(EntityTypeBuilder<WorkingSchedule> builder)
        {
            builder.ToTable(nameof(WorkingSchedule));

            builder.HasKey(x => x.Id);

            builder.Property(x => x.DayOfWeek)
                .HasConversion<string>()
                .HasMaxLength(10);

            // (0) stores hh:mm:ss without fractional seconds
            builder.Property(x => x.StartTime)
                .HasColumnType("time(0)"); 

            builder.Property(x => x.EndTime)
                .HasColumnType("time(0)");

            builder.HasIndex(x => new { x.PsychologistId, x.DayOfWeek });
        }
    }
}
