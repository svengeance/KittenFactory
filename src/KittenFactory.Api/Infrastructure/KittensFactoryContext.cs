using KittenFactory.Api.Features.Kittens.Entities;
using KittenFactory.Api.Features.Orders.Entities;
using KittenFactory.Api.Features.Users.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KittenFactory.Api.Infrastructure;

public class KittensFactoryContext : IdentityDbContext<User, UserRole, string>
{
    public DbSet<Kitten> Kittens { get; init; }
    public DbSet<Order> Orders { get; init; }

    public KittensFactoryContext(DbContextOptions<KittensFactoryContext> options) : base(options)
    {
    }
}
