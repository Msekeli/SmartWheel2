namespace SmartWheel.Application.DTOs;

public sealed class ResolveUserByEmailResult
{
    public Guid UserId { get; set; }

    public decimal Balance { get; set; }

    public bool CanSpin { get; set; }
}