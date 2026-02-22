using Microsoft.EntityFrameworkCore;
using SmartWheel.Application.Entities;

namespace SmartWheel.Infrastructure.Persistence;

public sealed class SmartWheelDbContext : DbContext
{
    public SmartWheelDbContext(DbContextOptions<SmartWheelDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<SpinHistory> SpinHistories => Set<SpinHistory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SmartWheelDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}