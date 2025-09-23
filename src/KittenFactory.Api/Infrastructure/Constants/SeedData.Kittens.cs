using KittenFactory.Api.Features.Kittens.Entities;

namespace KittenFactory.Api.Infrastructure.Constants;

public static partial class SeedData
{
    public static class Kittens
    {
        public static Kitten Sagwa = new()
        {
            DesignedById = Users.Sven.Id,
            Customization = new KittenCustomizationV1
            {
                Name = "Sagwa",
                Age = 6,
                Color = "Dusty Snow",
                Weight = 9.0
            }
        };

        public static Kitten Shadow = new()
        {
            DesignedById = Users.TestUser.Id,
            Customization = new KittenCustomizationV1
            {
                Name = "Shadow",
                Age = 4,
                Color = "Black",
                Weight = 13.5
            }
        };
    }
}
