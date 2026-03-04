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
        // TEMP DEBUG (helps confirm frontend answers)
        if (command.Answers == null)
            throw new Exception("Answers are NULL");

        Console.WriteLine("Answers received:");
        foreach (var a in command.Answers)
        {
            Console.WriteLine($"- {a}");
        }

        // Get user
        var user = await _userRepository.GetByIdAsync(
            command.UserId,
            cancellationToken);

        if (user is null)
            throw new InvalidOperationException("User not found.");

        var utcNow = DateTime.UtcNow;

        // Enforce 24-hour cooldown
        if (!user.CanSpin(utcNow))
            throw new InvalidOperationException("24-hour cooldown has not passed.");

        // Calculate score
        var score = _scoreCalculator.Calculate(command.Answers);

        // Calculate prize
        var prizeAmount = _prizeCalculator.Calculate(score);

        // Determine wheel index from prize
        var wheelIndex = _prizeCalculator.GetWheelIndex(prizeAmount);

        // Update user balance and last spin time
        user.ApplySpinResult(prizeAmount, utcNow);

        // Create spin history
        var spinHistory = SpinHistory.Create(
            user.Id,
            score,
            prizeAmount,
            utcNow);

        // Save history
        await _spinHistoryRepository.AddAsync(
            spinHistory,
            cancellationToken);

        // Update user
        await _userRepository.UpdateAsync(
            user,
            cancellationToken);

        // Return result to frontend
        return new SpinResult
        {
            Score = score,
            PrizeAmount = prizeAmount,
            WheelIndex = wheelIndex
        };
    }
}