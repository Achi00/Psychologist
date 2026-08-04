using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PsychologistSystem.Domain.Entity;

namespace PsychologistSystem.Persistance.Configurations
{
    public sealed class PsychologistConfiguration : IEntityTypeConfiguration<Psychologist>
    {
        public void Configure(EntityTypeBuilder<Psychologist> builder)
        {
            builder.ToTable("Psychologist");

            builder.HasKey(x => x.UserId);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(500);
        }
    }
}
