using Duende.IdentityServer;
using KittenFactory.Api.Infrastructure.Constants;
using Microsoft.OpenApi.Models;

namespace KittenFactory.Api.Infrastructure;

public static class OpenApiHostBuilderExtensions
{
    public static void AddKittenFactoryOpenApi(this WebApplicationBuilder builder, string apiUrl)
    {
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen(a =>
        {
            a.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    Password = new OpenApiOAuthFlow
                    {
                        TokenUrl = new Uri($"{apiUrl}/connect/token"),
                        Scopes = new Dictionary<string, string>
                        {
                            { IdentityServerConstants.StandardScopes.OpenId, "OpenID" },
                            { IdentityServerConstants.StandardScopes.Profile, "Profile" },
                            { SeedData.Identity.ApiScopes.KittensFactoryApiScope.Name, SeedData.Identity.ApiScopes.KittensFactoryApiScope.DisplayName! }
                        }
                    }
                }
            });

            a.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "oauth2"
                        }
                    },
                    []
                }
            });
        });
    }

    public static void UseKittenFactoryOpenApi(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
            return;

        app.UseSwagger();
        app.UseSwaggerUI(s =>
        {
            s.OAuthConfigObject.ClientId = SeedData.Identity.Clients.Swagger.ClientId;
            s.OAuthConfigObject.ClientSecret = SeedData.Identity.Clients.SwaggerClientSecret;
            s.OAuthConfigObject.Username = SeedData.Users.TestUser.UserName;
        });
    }
}
