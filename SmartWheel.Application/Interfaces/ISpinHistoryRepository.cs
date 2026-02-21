using SmartWheel.Application.Entities;

namespace SmartWheel.Application.Interfaces;

public interface ISpinHistoryRepository
{
    Task AddAsync(SpinHistory spinHistory, CancellationToken cancellationToken = default);

    Task<int> GetSpinCountForUserAsync(Guid userId, CancellationToken cancellationToken = default);
}