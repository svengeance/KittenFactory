using KittenFactory.Api.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.AddNpgsqlDbContext<KittensFactoryContext>(
    connectionName: "kittens-factory-db",
    configureSettings: null,
    configureDbContextOptions: o => o.UseNpgsql(n => n.MigrationsAssembly(typeof(Program).Assembly)).UseSnakeCaseNamingConvention()
);

builder.Services.AddSingleton<KittenFactoryContextMigrator>();
builder.Services.AddHostedService<KittenFactoryContextMigrator>();

var app = builder.Build();
await app.RunAsync();

internal class KittenFactoryContextMigrator(IServiceProvider serviceProvider, IHostApplicationLifetime hostApplicationLifetime) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        await using var context = scope.ServiceProvider.GetRequiredService<KittensFactoryContext>();

        await context.Database.MigrateAsync(cancellationToken);
        hostApplicationLifetime.StopApplication();
    }
}
