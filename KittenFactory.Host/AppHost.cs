using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var kittenFactoryApi = builder.AddProject<KittenFactory_Api>("kitten-factory-api");

builder.Build().Run();
