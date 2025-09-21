using KittenFactory.Api.Features.Users.Entities;
using KittenFactory.Api.Infrastructure.Constants;
using Microsoft.AspNetCore.Identity;

namespace KittenFactory.Api.Infrastructure;

public static class SeedHostBuilderExtensions
{
    public static async Task SeedTestUsers(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

        if (await userManager.FindByNameAsync(SeedData.Users.TestUser.UserName!) == null)
            await userManager.CreateAsync(SeedData.Users.TestUser, SeedData.Users.TestUserPassword);
    }
}
