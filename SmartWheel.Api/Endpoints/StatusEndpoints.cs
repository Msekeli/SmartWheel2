using SmartWheel.Application.UseCases;
using SmartWheel.Api.Contracts;

namespace SmartWheel.Api.Endpoints;

public static class StatusEndpoints
{
    public static IEndpointRouteBuilder MapStatusEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/status/{userId:guid}", async (
            Guid userId,
            GetStatusUseCase useCase,
            CancellationToken cancellationToken) =>
        {
            var result = await useCase.ExecuteAsync(userId, cancellationToken);

            return Results.Ok(new StatusResponse
            {
                Balance = result.Balance,
                CanSpin = result.CanSpin
            });
        })
        .WithName("GetStatus")
        .WithOpenApi();

        return app;
    }
}