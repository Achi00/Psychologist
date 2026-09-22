using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PsychologistSystem.Application.Contracts.Auth;
using PsychologistSystem.Application.Contracts.Email;
using PsychologistSystem.Application.Interfaces;
using PsychologistSystem.Application.Interfaces.JWT;
using PsychologistSystem.Application.Interfaces.Repositories;
using PsychologistSystem.Application.Interfaces.Repositories.Categories;
using PsychologistSystem.Application.Interfaces.Repositories.Psychologists;
using PsychologistSystem.Application.Interfaces.Services.Auth;
using PsychologistSystem.Application.Interfaces.Services.Email;
using PsychologistSystem.Application.Services.Auth;
using PsychologistSystem.Infrastructure.Auth;
using PsychologistSystem.Infrastructure.Email;
using PsychologistSystem.Infrastructure.Repositories;
using PsychologistSystem.Infrastructure.Repositories.Psychologists;
using PsychologistSystem.Infrastructure.Validators;
using PsychologistSystem.Persistance.Context;
using PsychologistSystem.Persistance.Identity;

namespace PsychologistSystem.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options => 
            {
                // because UserName = Email, should allow certain characters to avoid validation issues
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 6;
                options.SignIn.RequireConfirmedEmail = true;
            })
           .AddPasswordValidator<PreventCurrentPasswordValidator>()
           .AddEntityFrameworkStores < ApplicationDbContext>()
           .AddDefaultTokenProviders();

            services.Configure<EmailSettings>(config.GetSection("EmailSettings"));

            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IEmailService, MailKitEmailService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();

            services.AddScoped<IPsychologistRepository, PsychologistRepository>();
            services.AddScoped<IPsychologistApplicationRepository, PsychologistApplicationRepository>();
            services.AddScoped<IRefreshTokenRepository,  RefreshTokenRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IUnitOfWork,  UnitOfWork>();

            return services;
        }
    }
}
