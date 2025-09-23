using KittenFactory.Api.Infrastructure;

const string ApiBaseUrl = "https://localhost:12111";

var builder = WebApplication.CreateBuilder(args);

builder.AddKittenFactoryLogging();
builder.AddKittenFactoryOpenApi(ApiBaseUrl);
builder.AddKittenFactoryAuth(ApiBaseUrl);
builder.AddKittenFactoryIdentity();
builder.AddKittenFactoryDatabase();
builder.AddKittenFactoryEndpoints();

var app = builder.Build();

app.UseKittenFactoryEndpoints();
app.UseHttpsRedirection();
app.UseKittenFactoryAuth();
app.UseKittenFactoryIdentity();
app.UseKittenFactoryOpenApi();

await app.SeedData();

app.Run();
