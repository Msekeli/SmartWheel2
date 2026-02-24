namespace SmartWheel.Application.DTOs;

public sealed class ResolveUserByEmailRequest
{
    public string Email { get; set; } = default!;
}