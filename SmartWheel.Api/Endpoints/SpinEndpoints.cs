using SmartWheel.Application.DTOs;
using SmartWheel.Application.UseCases;
using SmartWheel.Api.Contracts;

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

            var response = new SpinResponse
            {
                Score = result.Score,
                PrizeAmount = result.PrizeAmount,
                WheelIndex = result.WheelIndex
            };

            return Results.Ok(response);
        })
        .WithName("ProcessSpin")
        .Produces<SpinResponse>(StatusCodes.Status200OK)
        .WithOpenApi();

        return app;
    }
}