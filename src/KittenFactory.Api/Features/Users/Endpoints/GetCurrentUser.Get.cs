using System.Security.Claims;
using KittenFactory.Api.Features.Users.Entities;
using Microsoft.AspNetCore.Identity;

namespace KittenFactory.Api.Features.Users.Endpoints;

public record GetCurrentUserResponse(
    string Id,
    string UserName,
    string Email
);

public static class GetCurrentUser
{
    public static T Register<T>(T app) where T : IEndpointRouteBuilder
    {
        app.MapGet("/user", async (ClaimsPrincipal principal, UserManager<User> userManager) =>
            {
                var user = await userManager.GetUserAsync(principal);

                if (user is null)
                    return Results.Unauthorized();

                return Results.Ok(new
                {
                    user.Id,
                    user.UserName,
                    user.Email
                });
            })
            .RequireAuthorization()
            .WithTags("Users")
            .WithName("GetCurrentUser")
            .WithSummary("Get Current User")
            .WithDescription("Gets the currently authenticated user's details.")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status404NotFound);

        return app;
    }
}
