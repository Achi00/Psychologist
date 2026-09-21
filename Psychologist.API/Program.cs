using Azure.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PsychologistSystem.API.Extensions;
using PsychologistSystem.API.Middleware;
using PsychologistSystem.Application;
using PsychologistSystem.Application.Contracts.Auth;
using PsychologistSystem.Application.Interfaces.Services.Auth;
using PsychologistSystem.Application.Services.Auth;
using PsychologistSystem.Infrastructure;
using PsychologistSystem.Persistance.Seeding;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "PsychologistSystem API", Version = "v1" });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT access token below (no need to type \"Bearer \" — Swagger adds it automatically)."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
// custom ex middleware
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
// for framework's own fallback shape
builder.Services.AddProblemDetails();

// add persistance and db context services
builder.Services.AddPersistance(builder.Configuration);
// add application layer DI
builder.Services.AddApplication();
// add infrastructure layer DI
builder.Services.AddInfrastructure(builder.Configuration);
// auth
builder.Services.AddAuth(builder.Configuration);



// TODO: move configurations in extension class
builder.Services.Configure<ClientOptions>(
    builder.Configuration.GetSection("Client")
);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

// seeding
await AdminSeeder.SeedAdminAsync(app.Services, app.Configuration);
await CategorySeeder.SeedCategoriesAsync(app.Services);

app.Run();
