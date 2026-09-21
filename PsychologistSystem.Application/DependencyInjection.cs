using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PsychologistSystem.Application.Interfaces.Services.Psychologists;
using PsychologistSystem.Application.Services.Psychologists;
using PsychologistSystem.Application.Validators.Auth;

namespace PsychologistSystem.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<RegisterUserRequestValidator>();

            services.AddScoped<IPsychologistApplicationService, PsychologistApplicationService>();

            return services;
        }
    }
}
