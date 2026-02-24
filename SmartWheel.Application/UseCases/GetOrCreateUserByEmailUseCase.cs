using SmartWheel.Application.DTOs;
using SmartWheel.Application.Entities;
using SmartWheel.Application.Interfaces;

namespace SmartWheel.Application.UseCases;

public sealed class GetOrCreateUserByEmailUseCase
{
    private readonly IUserRepository _userRepository;

    public GetOrCreateUserByEmailUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ResolveUserByEmailResult> ExecuteAsync(
        ResolveUserByEmailRequest request,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.Email))
            throw new ArgumentException("Email is required.");

        // 1️⃣ Normalize email
        var normalizedEmail = request.Email.Trim().ToLowerInvariant();

        // 2️⃣ Try find existing user
        var user = await _userRepository
            .GetByEmailAsync(normalizedEmail, cancellationToken);

        // 3️⃣ Create if not found
        if (user is null)
        {
            user = User.Create(normalizedEmail);

            await _userRepository.AddAsync(user, cancellationToken);
        }

        // 4️⃣ Return current state
        var utcNow = DateTime.UtcNow;

        return new ResolveUserByEmailResult
        {
            UserId = user.Id,
            Balance = user.Balance,
            CanSpin = user.CanSpin(utcNow)
        };
    }
}