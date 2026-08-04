using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Psychologist.Domain.Entity;
using Psychologist.Persistance.Identity;

namespace Psychologist.Persistance.Context
{
    public sealed class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public DbSet<Domain.Entity.Psychologist> Psychologists => Set<Domain.Entity.Psychologist>();

        public DbSet<Category> Categories => Set<Category>();

        public DbSet<Appointment> Appointments => Set<Appointment>();

        public DbSet<WorkingSchedule> WorkingSchedules => Set<WorkingSchedule>();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
    }
}
