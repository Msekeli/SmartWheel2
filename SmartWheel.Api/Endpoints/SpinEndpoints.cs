using SmartWheel.Application.DTOs;
using SmartWheel.Application.UseCases;
using SmartWheel.Api.Contracts;

namespace SmartWheel.Api.Endpoints;

public static class SpinEndpoints
{
    public static IEndpointRouteBuilder MapSpinEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/spin", async (
            SpinRequest request,
            ProcessSpinUseCase useCase,
            CancellationToken cancellationToken) =>
        {
            var command = new SpinCommand
            {
                UserId = request.UserId,
                Answers = request.Answers
            };

            var result = await useCase.ExecuteAsync(command, cancellationToken);

            return Results.Ok(new SpinResponse
            {
                Score = result.Score,
                PrizeAmount = result.PrizeAmount,
                WheelIndex = result.WheelIndex
            });
        })
        .WithName("ProcessSpin")
        .WithOpenApi();

        return app;
    }
}