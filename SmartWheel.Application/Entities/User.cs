namespace SmartWheel.Application.Entities;

public sealed class User
{
    public Guid Id { get; private set; }
    public string Email { get; private set; } = default!;
    public decimal Balance { get; private set; }
    public DateTime LastSpinUtc { get; private set; }

    private User() { } // Required for EF

    private User(Guid id, string email)
    {
        Id = id;
        Email = email;
        Balance = 0;
        LastSpinUtc = DateTime.MinValue;
    }

    public static User Create(string email)
    {
        return new User(Guid.NewGuid(), email);
    }

    public bool CanSpin(DateTime utcNow)
    {
        return utcNow >= LastSpinUtc.AddHours(24);
    }

    public void ApplySpinResult(decimal prizeAmount, DateTime utcNow)
    {
        Balance += prizeAmount;
        LastSpinUtc = utcNow;
    }
}