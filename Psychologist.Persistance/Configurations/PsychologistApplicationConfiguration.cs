using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PsychologistSystem.Domain.Entity;

namespace PsychologistSystem.Persistance.Configurations
{
    public sealed class PsychologistApplicationConfiguration : IEntityTypeConfiguration<PsychologistApplication>
    {
        public void Configure(EntityTypeBuilder<PsychologistApplication> builder)
        {
            builder.ToTable(nameof(PsychologistApplication));

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Description)
                .HasMaxLength(500)
                .IsRequired();

            builder.HasIndex(x => new { x.UserId, x.Status })
                .HasDatabaseName("IX_Users_UserId_Status");
        }
    }
}
