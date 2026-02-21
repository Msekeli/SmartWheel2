using SmartWheel.Application.DTOs;
using SmartWheel.Application.Entities;
using SmartWheel.Application.Interfaces;
using SmartWheel.Application.Services;

namespace SmartWheel.Application.UseCases;

public sealed class ProcessSpinUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly ISpinHistoryRepository _spinHistoryRepository;
    private readonly IScoreCalculator _scoreCalculator;
    private readonly IPrizeCalculator _prizeCalculator;

    public ProcessSpinUseCase(
        IUserRepository userRepository,
        ISpinHistoryRepository spinHistoryRepository,
        IScoreCalculator scoreCalculator,
        IPrizeCalculator prizeCalculator)
    {
        _userRepository = userRepository;
        _spinHistoryRepository = spinHistoryRepository;
        _scoreCalculator = scoreCalculator;
        _prizeCalculator = prizeCalculator;
    }

    public async Task<SpinResult> ExecuteAsync(
        SpinCommand command,
        CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(command.UserId, cancellationToken);

        if (user is null)
            throw new InvalidOperationException("User not found.");

        var utcNow = DateTime.UtcNow;

        if (!user.CanSpin(utcNow))
            throw new InvalidOperationException("24-hour cooldown has not passed.");

        // 1️⃣ Calculate score
        var score = _scoreCalculator.Calculate(command.Answers);

        // 2️⃣ Calculate prize
        var prizeAmount = _prizeCalculator.Calculate(score);

        // 3️⃣ Update user balance + last spin
        user.ApplySpinResult(prizeAmount, utcNow);

        // 4️⃣ Create spin history record
        var spinHistory = SpinHistory.Create(
            user.Id,
            score,
            prizeAmount,
            utcNow);

        // 5️⃣ Persist changes
        await _spinHistoryRepository.AddAsync(spinHistory, cancellationToken);
        await _userRepository.UpdateAsync(user, cancellationToken);

        // 6️⃣ Return result
        return new SpinResult
        {
            Score = score,
            PrizeAmount = prizeAmount,
            WheelIndex = score // temporary mapping (we refine later)
        };
    }
}