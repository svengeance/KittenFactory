using KittenFactory.Api.Features.Kittens.Endpoints;
using KittenFactory.Api.Features.Users.Endpoints;

namespace KittenFactory.Api.Infrastructure;

public static class EndpointsHostBuilderExtensions
{
    public static void AddKittenFactoryEndpoints(this WebApplicationBuilder builder)
        => builder.Services.AddControllers();

    public static void UseKittenFactoryEndpoints(this WebApplication app)
    {
        app.UseRouting();

        GetCurrentUser.Register(app);
        CreateKitten.Register(app);
    }
}
