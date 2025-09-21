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
}
