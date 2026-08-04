using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Psychologist.Persistance.Configurations
{
    public sealed class PsychologistConfiguration : IEntityTypeConfiguration<Domain.Entity.Psychologist>
    {
        public void Configure(EntityTypeBuilder<Domain.Entity.Psychologist> builder)
        {
            builder.ToTable("Psychologist");

            builder.HasKey(x => x.UserId);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(500);
        }
    }
}
