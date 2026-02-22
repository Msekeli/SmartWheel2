using Microsoft.EntityFrameworkCore;
using SmartWheel.Application.Interfaces;
using SmartWheel.Application.Services;
using SmartWheel.Application.UseCases;
using SmartWheel.Infrastructure.Persistence;
using SmartWheel.Infrastructure.Persistence.Repositories;
using SmartWheel.Infrastructure.Services;
using SmartWheel.Api.Endpoints;

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

//Swagger Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Test Endpoint with Swagger 
app.MapGet("/", () => "SmartWheel API is running!");

app.UseHttpsRedirection();

app.MapSpinEndpoints();
app.MapStatusEndpoints();

app.Run();