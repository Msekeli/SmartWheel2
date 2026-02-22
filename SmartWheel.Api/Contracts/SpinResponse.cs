namespace SmartWheel.Api.Contracts;

public sealed class SpinResponse
{
    public int Score { get; init; }
    public decimal PrizeAmount { get; init; }
    public int WheelIndex { get; init; }
}