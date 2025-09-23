using KittenFactory.Api.Features.Users.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KittenFactory.Api.Infrastructure;

public static class DatabaseHostBuilderExtensions
{
    public static void AddKittenFactoryDatabase(this WebApplicationBuilder builder)
        => builder.AddNpgsqlDbContext<KittensFactoryContext>(
            connectionName: "kittens-factory-db",
            configureSettings: null,
            o => o.UseNpgsql().UseSnakeCaseNamingConvention()
        );

    public static async Task SeedData(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();

        var context = scope.ServiceProvider.GetRequiredService<KittensFactoryContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

        var strategy = context.Database.CreateExecutionStrategy();
        await strategy.ExecuteInTransactionAsync(async () =>
        {
            await SeedUsers(userManager);
            await SeedKittens(context);
            await SeedOrders(context);
        }, () => Task.FromResult(true));
    }

    private static async Task SeedUsers(UserManager<User> userManager)
    {
        if (await userManager.FindByNameAsync(Constants.SeedData.Users.TestUser.UserName!) == null)
            await userManager.CreateAsync(Constants.SeedData.Users.TestUser, Constants.SeedData.Users.TestUserPassword);

        if (await userManager.FindByNameAsync(Constants.SeedData.Users.Sven.UserName!) == null)
            await userManager.CreateAsync(Constants.SeedData.Users.Sven, Constants.SeedData.Users.SvenUserPassword);
    }

    private static async Task SeedKittens(KittensFactoryContext context)
    {
        if (await context.Kittens.AnyAsync())
            return;

        context.Kittens.AddRange(Constants.SeedData.Kittens.Sagwa, Constants.SeedData.Kittens.Shadow);

        await context.SaveChangesAsync();
    }

    private static async Task SeedOrders(KittensFactoryContext context)
    {
        if (await context.Orders.AnyAsync())
            return;

        context.Orders.AddRange(Constants.SeedData.Orders.Sven_Sagwa);

        await context.SaveChangesAsync();
    }
}
