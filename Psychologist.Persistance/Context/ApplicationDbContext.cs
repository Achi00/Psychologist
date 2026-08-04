using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PsychologistSystem.Domain.Entity;
using PsychologistSystem.Persistance.Identity;

namespace PsychologistSystem.Persistance.Context
{
    public sealed class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public DbSet<Psychologist> Psychologists => Set<Psychologist>();

        public DbSet<Category> Categories => Set<Category>();

        public DbSet<Appointment> Appointments => Set<Appointment>();

        public DbSet<WorkingSchedule> WorkingSchedules => Set<WorkingSchedule>();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
    }
}
