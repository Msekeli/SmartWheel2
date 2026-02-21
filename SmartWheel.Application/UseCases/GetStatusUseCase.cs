using SmartWheel.Application.DTOs;
using SmartWheel.Application.Interfaces;

namespace SmartWheel.Application.UseCases;

public sealed class GetStatusUseCase
{
    private readonly IUserRepository _userRepository;

    public GetStatusUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<StatusResult> ExecuteAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);

        if (user is null)
            throw new InvalidOperationException("User not found.");

        var utcNow = DateTime.UtcNow;

        return new StatusResult
        {
            Balance = user.Balance,
            CanSpin = user.CanSpin(utcNow)
        };
    }
}