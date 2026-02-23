using Microsoft.EntityFrameworkCore;
using SmartWheel.Application.Interfaces;
using SmartWheel.Application.Services;
using SmartWheel.Application.UseCases;
using SmartWheel.Infrastructure.Persistence;
using SmartWheel.Infrastructure.Persistence.Repositories;
using SmartWheel.Infrastructure.Services;
using SmartWheel.Api.Endpoints;
using SmartWheel.Application.Entities;

var builder = WebApplication.CreateBuilder(args);

//
// 🔥 FORCE SQLITE TO USE AZURE-PERSISTENT /home DIRECTORY
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

// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISpinHistoryRepository, SpinHistoryRepository>();

// Domain Services
builder.Services.AddScoped<IScoreCalculator, ScoreCalculator>();
builder.Services.AddScoped<IPrizeCalculator, PrizeCalculator>();

// Use Cases
builder.Services.AddScoped<ProcessSpinUseCase>();
builder.Services.AddScoped<GetStatusUseCase>();

var app = builder.Build();

//
// 🔥 APPLY MIGRATIONS AUTOMATICALLY ON STARTUP
//
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SmartWheelDbContext>();
    db.Database.Migrate();
}

// Swagger Middleware
app.UseSwagger();
app.UseSwaggerUI();

// Root Test Endpoint
app.MapGet("/", () => "SmartWheel API is running!");

// HTTPS
app.UseHttpsRedirection();

// Business Endpoints
app.MapSpinEndpoints();
app.MapStatusEndpoints();

//
// 🧪 DEBUG USER CREATION (TEMPORARY)
//
app.MapPost("/debug/create-user", async (SmartWheelDbContext db) =>
{
    var email = "test@test.com";

    var existingUser = await db.Users.FirstOrDefaultAsync(u => u.Email == email);

    if (existingUser is not null)
    {
        return Results.Ok(new
        {
            existingUser.Id,
            Message = "User already exists"
        });
    }

    var user = User.Create(email);

    db.Users.Add(user);
    await db.SaveChangesAsync();

    return Results.Ok(new
    {
        user.Id,
        Message = "User created successfully"
    });
});

app.Run();