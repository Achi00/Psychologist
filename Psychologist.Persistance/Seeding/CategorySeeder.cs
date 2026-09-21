using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PsychologistSystem.Domain.Entity;
using PsychologistSystem.Persistance.Context;

namespace PsychologistSystem.Persistance.Seeding
{
    public static class CategorySeeder
    {
        public static async Task SeedCategoriesAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // check if already seeded
            if (await context.Categories.AnyAsync())
            {
                return; 
            }

            context.Categories.AddRange(
                new Category { Id = Guid.NewGuid(), Name = "Child Psychology", Description = "Specializing in children and adolescents" },
                new Category { Id = Guid.NewGuid(), Name = "Family Therapy", Description = "Couples and family counseling" },
                new Category { Id = Guid.NewGuid(), Name = "Clinical Psychology", Description = "General clinical psychological support" }
            );

            await context.SaveChangesAsync();
        }
    }
}
