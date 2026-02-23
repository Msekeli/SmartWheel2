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
// Ensure the Data directory exists
var dataPath = Path.Combine(builder.Environment.ContentRootPath, "Data");
Directory.CreateDirectory(dataPath);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Database 
builder.Services.AddDbContext<SmartWheelDbContext>(options =>
   options.UseSqlite(
    builder.Configuration.GetConnectionString("DefaultConnection")));

//Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISpinHistoryRepository, SpinHistoryRepository>();

//Domain Services
builder.Services.AddScoped<IScoreCalculator, ScoreCalculator>();
builder.Services.AddScoped<IPrizeCalculator, PrizeCalculator>();

//Use Cases
builder.Services.AddScoped<ProcessSpinUseCase>();
builder.Services.AddScoped<GetStatusUseCase>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<SmartWheelDbContext>();
    dbContext.Database.Migrate();
}

//Swagger Middleware
app.UseSwagger();
app.UseSwaggerUI();

//Test Endpoint with Swagger 
app.MapGet("/", () => "SmartWheel API is running!");

app.UseHttpsRedirection();

app.MapSpinEndpoints();
app.MapStatusEndpoints();

app.MapPost("/debug/create-user", async (SmartWheelDbContext db) =>
{
    var email = "test@test.com";

    // Check if already exists
    var existingUser = db.Users.FirstOrDefault(u => u.Email == email);
    if (existingUser is not null)
        return Results.Ok(new { existingUser.Id, Message = "User already exists" });

    var user = User.Create(email);

    db.Users.Add(user);
    await db.SaveChangesAsync();

    return Results.Ok(new { user.Id, Message = "User created successfully" });
});

app.Run();