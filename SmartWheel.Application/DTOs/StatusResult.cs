namespace SmartWheel.Application.DTOs;

public sealed class StatusResult
{
    public decimal Balance { get; init; }
    public bool CanSpin { get; init; }
}