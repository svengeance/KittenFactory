using Duende.IdentityServer.Models;
using KittenFactory.Api.Features.Users.Entities;
using KittenFactory.Api.Infrastructure.Constants;
using Microsoft.AspNetCore.Identity;

namespace KittenFactory.Api.Infrastructure;

public static class IdentityHostBuilderExtensions
{
    public static void AddKittenFactoryIdentity(this WebApplicationBuilder builder)
    {
        builder.Services.AddIdentity<User, UserRole>()
            .AddEntityFrameworkStores<KittensFactoryContext>()
            .AddDefaultTokenProviders();

        builder.Services
            .AddIdentityServer(c =>
            {
                c.Events.RaiseErrorEvents = true;
                c.Events.RaiseInformationEvents = true;
                c.Events.RaiseFailureEvents = true;
                c.Events.RaiseSuccessEvents = true;
            })
            .AddDeveloperSigningCredential()
            .AddInMemoryApiResources([SeedData.Identity.ApiResources.KittensFactoryApi])
            .AddInMemoryApiScopes([SeedData.Identity.ApiScopes.KittensFactoryApiScope])
            .AddInMemoryIdentityResources([new IdentityResources.OpenId(), new IdentityResources.Profile()])
            .AddInMemoryClients([SeedData.Identity.Clients.Swagger])
            .AddAspNetIdentity<User>();
    }

    public static void UseKittenFactoryIdentity(this WebApplication app)
        => app.UseIdentityServer();
}
