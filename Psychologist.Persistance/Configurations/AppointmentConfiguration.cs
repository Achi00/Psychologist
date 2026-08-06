using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PsychologistSystem.Domain.Entity;

namespace PsychologistSystem.Persistance.Configurations
{
    public sealed class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable(nameof(Appointment));

            builder.HasKey(x => x.Id);

            // save some space with (0)
            builder.Property(x => x.StartTime)
                .HasColumnType("datetimeoffset(0)");
            
            builder.Property(x => x.EndTime)
                .HasColumnType("datetimeoffset(0)");


            builder.HasIndex(x => x.UserId);
            builder.HasIndex(x => x.PsychologistId);

            builder.Property(x => x.Status)
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()");

            // index for overlapping appointment lookups
            builder.HasIndex(x => new { x.PsychologistId, x.StartTime, x.EndTime });

        }
    }
}
