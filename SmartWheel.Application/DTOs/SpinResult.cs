namespace SmartWheel.Application.DTOs;

public sealed class SpinResult
{
    public int Score { get; init; }
    public decimal PrizeAmount { get; init; }
    public int WheelIndex { get; init; }
}