using Microsoft.EntityFrameworkCore;
using SmartWheel.Application.Entities;
using SmartWheel.Application.Interfaces;
using SmartWheel.Infrastructure.Persistence;

namespace SmartWheel.Infrastructure.Persistence.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly SmartWheelDbContext _context;

    public UserRepository(SmartWheelDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync(cancellationToken);
    }
}