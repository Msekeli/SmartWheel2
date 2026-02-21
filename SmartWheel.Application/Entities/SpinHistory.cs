namespace SmartWheel.Application.Entities;

public sealed class SpinHistory
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public int Score { get; private set; }
    public decimal PrizeAmount { get; private set; }
    public DateTime CreatedUtc { get; private set; }

    private SpinHistory() { } // For persistence (EF later)

    private SpinHistory(
        Guid id,
        Guid userId,
        int score,
        decimal prizeAmount,
        DateTime createdUtc)
    {
        Id = id;
        UserId = userId;
        Score = score;
        PrizeAmount = prizeAmount;
        CreatedUtc = createdUtc;
    }

    public static SpinHistory Create(
        Guid userId,
        int score,
        decimal prizeAmount,
        DateTime utcNow)
    {
        return new SpinHistory(
            Guid.NewGuid(),
            userId,
            score,
            prizeAmount,
            utcNow);
    }
}