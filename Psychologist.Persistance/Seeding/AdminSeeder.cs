using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PsychologistSystem.Domain.Enums;
using PsychologistSystem.Persistance.Identity;

namespace PsychologistSystem.Persistance.Seeding
{
    public static class AdminSeeder
    {
        public static async Task SeedAdminAsync(IServiceProvider services, IConfiguration configuration)
        {
            using var scope = services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            // add roles before admin seeing
            foreach (var role in Enum.GetNames<Role>())
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>(role));
                }
            }

            // unsure that admin role exists
            if (!await roleManager.RoleExistsAsync(nameof(Role.Admin)))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(nameof(Role.Admin)));
            }

            var adminEmail = configuration["AdminSeed:Email"];
            var adminPassword = configuration["AdminSeed:Password"];

            if (string.IsNullOrEmpty(adminEmail) || string.IsNullOrEmpty(adminPassword))
            {
                // nothing configured, skip silently
                return;
            }

            var existingAdmin = await userManager.FindByEmailAsync(adminEmail);
            if (existingAdmin is not null)
            {
                // admin alreade seeded, dont create new
                return;
            }

            // create validated admin
            var admin = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FirstName = "Admin",
                LastName = "User",
                EmailConfirmed = true,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow,
            };

            var result = await userManager.CreateAsync(admin, adminPassword);

            if (result.Succeeded)
            {
                Console.WriteLine("Seeding succeed");
                await userManager.AddToRoleAsync(admin, nameof(Role.Admin));
            }
        }
    }
}
