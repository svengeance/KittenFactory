using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithPgAdmin(w => w.WithHostPort(7777));

var kittensFactoryDb = postgres.AddDatabase("kittens-factory-db", "kittens_factory");

var kittensFactoryDbMigrator = builder.AddProject<KittenFactory_DatabaseMigrator>("kitten-factory-db-migrator")
    .WithReference(kittensFactoryDb)
    .WaitFor(kittensFactoryDb);

var kittenFactoryApi = builder.AddProject<KittenFactory_Api>("kitten-factory-api")
    .WithReference(kittensFactoryDb)
    .WaitForCompletion(kittensFactoryDbMigrator);

builder.Build().Run();
