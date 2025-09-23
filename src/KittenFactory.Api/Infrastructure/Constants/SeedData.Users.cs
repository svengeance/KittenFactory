using KittenFactory.Api.Features.Users.Entities;

namespace KittenFactory.Api.Infrastructure.Constants;

public static partial class SeedData
{
    public static class Users
    {
        public static string TestUserPassword = "Kittens123!";

        public static User TestUser = new()
        {
            Id = Guid.Empty.ToString(),
            UserName = "TestUser",
            Email = "TestUser@kittens.com"
        };

        public static string SvenUserPassword = "Sven123!";

        public static User Sven = new()
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "Sven",
            Email = "Sven@kittens.com"
        };
    }
}
