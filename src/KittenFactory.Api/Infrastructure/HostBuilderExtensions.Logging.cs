using Npgsql;
using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace KittenFactory.Api.Infrastructure;

public static class LoggingHostBuilderExtensions
{
    public static void AddKittenFactoryLogging(this WebApplicationBuilder builder)
        => builder.Services.AddOpenTelemetry()
            .ConfigureResource(c => c.AddService("KittenFactory.Api"))
            .WithTracing(t =>
            {
                t.AddAspNetCoreInstrumentation();
                t.AddHttpClientInstrumentation();
                t.AddEntityFrameworkCoreInstrumentation();
                t.AddNpgsql();

                t.AddConsoleExporter();
            })
            .WithMetrics(m =>
            {
                m.AddAspNetCoreInstrumentation();
                m.AddHttpClientInstrumentation();
                m.AddNpgsqlInstrumentation();
            })
            .WithLogging(l => l.AddConsoleExporter(), l =>
            {
                l.IncludeFormattedMessage = true;
                l.IncludeScopes = true;
            })
            .UseOtlpExporter();
}
