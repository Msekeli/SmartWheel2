using SmartWheel.Application.DTOs;
using SmartWheel.Application.UseCases;

namespace SmartWheel.Api.Endpoints;

public static class IdentityEndpoints
{
    public static IEndpointRouteBuilder MapIdentityEndpoints(
        this IEndpointRouteBuilder app)
    {
        app.MapPost("/identity", async (
            ResolveUserByEmailRequest request,
            GetOrCreateUserByEmailUseCase useCase,
            CancellationToken cancellationToken) =>
        {
            var result = await useCase.ExecuteAsync(request, cancellationToken);

            return Results.Ok(result);
        })
        .WithName("ResolveIdentity")
        .Produces<ResolveUserByEmailResult>(StatusCodes.Status200OK)
        .WithOpenApi();

        return app;
    }
}