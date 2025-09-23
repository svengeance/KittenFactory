using KittenFactory.Api.Features.Kittens.Entities;
using KittenFactory.Api.Features.Orders.Entities;

namespace KittenFactory.Api.Infrastructure.Constants;

public static partial class SeedData
{
    public static class Orders
    {
        public static Order Sven_Sagwa = new()
        {
            OrderedBy = Users.Sven,
            Kittens = new List<Kitten> { Kittens.Sagwa },
            OrderedAtUtc = DateTime.UtcNow.Date
        };
    }
}
