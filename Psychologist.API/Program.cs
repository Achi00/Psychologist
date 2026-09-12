using Azure.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PsychologistSystem.API.Extensions;
using PsychologistSystem.Application;
using PsychologistSystem.Application.Contracts.Auth;
using PsychologistSystem.Infrastructure;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// add persistance and db context services
builder.Services.AddPersistance(builder.Configuration);
// add application layer DI
builder.Services.AddApplication();
// add infrastructure layer DI
builder.Services.AddInfrastructure(builder.Configuration);

// auth
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>()!;
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = jwtSettings.Issuer,
//        ValidAudience = jwtSettings.Audience,
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
//        ClockSkew = TimeSpan.Zero
//    };
//});
//builder.Services.AddAuthorization();

builder.Services.AddAuth(builder.Configuration);
builder.Services.AddHttpContextAccessor();

// TODO: move configurations in extension class
builder.Services.Configure<ClientOptions>(
    builder.Configuration.GetSection("Client")
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
