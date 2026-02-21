namespace SmartWheel.Application.DTOs;

public sealed class SpinCommand
{
    public Guid UserId { get; init; }
    public List<string> Answers { get; init; } = new();
}