using Microsoft.EntityFrameworkCore;
using SmartWheel.Application.Entities;
using SmartWheel.Application.Interfaces;
using SmartWheel.Infrastructure.Persistence;

namespace SmartWheel.Infrastructure.Persistence.Repositories;

public sealed class SpinHistoryRepository : ISpinHistoryRepository
{
    private readonly SmartWheelDbContext _context;

    public SpinHistoryRepository(SmartWheelDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        SpinHistory spinHistory,
        CancellationToken cancellationToken = default)
    {
        await _context.SpinHistories.AddAsync(spinHistory, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> GetSpinCountForUserAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await _context.SpinHistories
            .CountAsync(x => x.UserId == userId, cancellationToken);
    }
}