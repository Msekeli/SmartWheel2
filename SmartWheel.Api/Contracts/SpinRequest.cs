namespace SmartWheel.Api.Contracts;

public sealed class SpinRequest
{
    public Guid UserId { get; init; }
    public List<string> Answers { get; init; } = new();
}