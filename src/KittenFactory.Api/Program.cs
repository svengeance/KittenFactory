using KittenFactory.Api.Features.Users.Endpoints;
using KittenFactory.Api.Features.Users.Entities;
using KittenFactory.Api.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorizationBuilder();

builder.AddNpgsqlDbContext<KittensFactoryContext>(
    connectionName: "kittens-factory-db",
    configureSettings: null,
    o => o.UseNpgsql().UseSnakeCaseNamingConvention()
);

builder.Services.AddIdentity<User, UserRole>()
    .AddEntityFrameworkStores<KittensFactoryContext>()
    .AddApiEndpoints();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseSwagger().UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapIdentityApi<User>();

GetCurrentUser.Register(app);

app.Run();
