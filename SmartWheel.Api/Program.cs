using Microsoft.EntityFrameworkCore;
using SmartWheel.Application.Interfaces;
using SmartWheel.Application.Services;
using SmartWheel.Application.UseCases;
using SmartWheel.Infrastructure.Persistence;
using SmartWheel.Infrastructure.Persistence.Repositories;
using SmartWheel.Infrastructure.Services;
using SmartWheel.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

//
// 📁 SQLite Path (Azure-safe)
//
var home = Environment.GetEnvironmentVariable("HOME");
var dataFolder = Path.Combine(home ?? builder.Environment.ContentRootPath, "Data");

Directory.CreateDirectory(dataFolder);

var dbPath = Path.Combine(dataFolder, "smartwheel.db");

builder.Services.AddDbContext<SmartWheelDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//
// Repositories
//
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISpinHistoryRepository, SpinHistoryRepository>();

//
// Domain Services
//
builder.Services.AddScoped<IScoreCalculator, ScoreCalculator>();
builder.Services.AddScoped<IPrizeCalculator, PrizeCalculator>();

//
// Use Cases
//
builder.Services.AddScoped<GetOrCreateUserByEmailUseCase>();
builder.Services.AddScoped<ProcessSpinUseCase>();
builder.Services.AddScoped<GetStatusUseCase>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

//
// Apply Migrations Automatically
//
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SmartWheelDbContext>();
    db.Database.Migrate();
}

// Swagger
app.UseSwagger();
app.UseSwaggerUI();

// Root health endpoint
app.MapGet("/", () => "SmartWheel API is running!");

// HTTPS
app.UseHttpsRedirection();

//
// Business Endpoints
//
app.MapIdentityEndpoints();
app.MapSpinEndpoints();
app.MapStatusEndpoints();

app.Run();