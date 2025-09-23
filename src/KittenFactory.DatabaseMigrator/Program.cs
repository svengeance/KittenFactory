using KittenFactory.Api.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.AddKittenFactoryLogging();
builder.AddNpgsqlDbContext<KittensFactoryContext>(
    connectionName: "kittens-factory-db",
    configureSettings: null,
    o => o.UseNpgsql(n => n.MigrationsAssembly("KittenFactory.DatabaseMigrator")).UseSnakeCaseNamingConvention()
);
// builder.Services.Configure<NpgsqlDbContextOptionsBuilder>(n => n.MigrationsAssembly("KittenFactory.DatabaseMigrator"));

builder.Services.AddSingleton<KittenFactoryContextMigrator>();
builder.Services.AddHostedService<KittenFactoryContextMigrator>();

var app = builder.Build();
await app.RunAsync();

file class KittenFactoryContextMigrator(IServiceProvider serviceProvider, IHostApplicationLifetime hostApplicationLifetime) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        await using var context = scope.ServiceProvider.GetRequiredService<KittensFactoryContext>();

        await context.Database.MigrateAsync(cancellationToken);
        hostApplicationLifetime.StopApplication();
    }
}
