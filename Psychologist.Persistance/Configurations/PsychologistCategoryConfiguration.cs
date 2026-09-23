using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PsychologistSystem.Domain.Entity;

namespace PsychologistSystem.Persistance.Configurations
{
    public sealed class PsychologistCategoryConfiguration : IEntityTypeConfiguration<PsychologistCategory>
    {
        public void Configure(EntityTypeBuilder<PsychologistCategory> builder)
        {
            builder.ToTable(nameof(PsychologistCategory));

            builder.HasKey(x => new { x.PsychologistId, x.CategoryId });

            builder.HasOne(x => x.Psychologist)
                .WithMany(p => p.Categories)
                .HasForeignKey(x => x.PsychologistId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Category)
                .WithMany()
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
