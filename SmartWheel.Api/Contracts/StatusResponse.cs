namespace SmartWheel.Api.Contracts;

public sealed class StatusResponse
{
    public decimal Balance { get; init; }
    public bool CanSpin { get; init; }
}