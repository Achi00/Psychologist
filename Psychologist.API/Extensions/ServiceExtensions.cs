using PsychologistSystem.Application.Interfaces.Services.Auth;
using PsychologistSystem.Application.Services.Auth;

namespace PsychologistSystem.API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddHttpContextAccessor();

            return services;
        }
    }
}
