using Microsoft.EntityFrameworkCore;
using PsychologistSystem.Persistance;
using PsychologistSystem.Persistance.Context;

namespace PsychologistSystem.API.Extensions
{
    public static class PersistenceExtensions
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(nameof(ConnectionString.DefaultConnection));

            services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlServer(connectionString)
            );

            return services;
        }
    }
}
