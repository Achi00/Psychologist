using Azure.Core;
using PsychologistSystem.API.Extensions;
using PsychologistSystem.Application;
using PsychologistSystem.Infrastructure;

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
