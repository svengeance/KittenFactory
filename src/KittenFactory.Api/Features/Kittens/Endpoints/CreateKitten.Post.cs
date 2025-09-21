using System.Security.Claims;
using KittenFactory.Api.Features.Kittens.Entities;
using KittenFactory.Api.Features.Kittens.Models;
using KittenFactory.Api.Features.Users.Entities;
using Microsoft.AspNetCore.Identity;

namespace KittenFactory.Api.Features.Kittens.Endpoints;

public record CreateKittenRequest(
    CustomizationV1Dto Customization
);

public static class CreateKitten
{
    public static T Register<T>(T app) where T : IEndpointRouteBuilder
    {
        app.MapPost("/kittens", (CreateKittenRequest createKittenRequest, ClaimsPrincipal principal, UserManager<User> userManager) =>
            {
                var userId = userManager.GetUserId(principal);

                if (userId is null)
                    return Results.Forbid();

                var kitten = new Kitten
                {
                    DesignedById = userId,
                    Customization = new KittenCustomizationV1
                    {
                        Name = createKittenRequest.Customization.Name,
                        Age = createKittenRequest.Customization.Age,
                        Color = createKittenRequest.Customization.Color,
                        Weight = createKittenRequest.Customization.Weight
                    }
                };

                return Results.Ok(new KittenResponse(
                    kitten.Id,
                    new CustomizationV1Dto(
                        kitten.Customization.Name,
                        kitten.Customization.Age,
                        kitten.Customization.Color,
                        kitten.Customization.Weight
                    )
                ));
            })
            .RequireAuthorization()
            .WithTags("Kittens")
            .WithName("CreateKitten")
            .WithSummary("Creates a new kitten")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status404NotFound);

        return app;
    }
}
