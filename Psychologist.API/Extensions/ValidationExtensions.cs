using FluentValidation;
using FluentValidation.AspNetCore;

namespace PsychologistSystem.API.Extensions
{
    public static class ValidationExtensions
    {
        public static IServiceCollection AddValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();

            return services;
        }
    }
}
