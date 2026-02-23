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

            var response = new StatusResponse
            {
                Balance = result.Balance,
                CanSpin = result.CanSpin
            };

            return Results.Ok(response);
        })
        .WithName("GetStatus")
        .Produces<StatusResponse>(StatusCodes.Status200OK)
        .WithOpenApi();

        return app;
    }
}