using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using PsychologistSystem.Infrastructure.Validators;
using PsychologistSystem.Persistance.Identity;

namespace PsychologistSystem.API.Extensions
{
    public static class ValidationExtensions
    {
        public static IServiceCollection AddValidation(this IServiceCollection services)
        {
    //        services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
    //                .AddPasswordValidator<PreventCurrentPasswordValidator>()
    //.AddEntityFrameworkStores<AppDbCont>()
    //.AddDefaultTokenProviders();
    //        services.AddFluentValidationAutoValidation();

            return services;
        }
    }
}
