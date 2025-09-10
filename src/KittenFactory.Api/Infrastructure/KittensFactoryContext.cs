using System.Reflection;
using KittenFactory.Api.Features.Kittens.Entities;
using KittenFactory.Api.Features.Orders.Entities;
using KittenFactory.Api.Features.Users.Entities;
using Microsoft.AspNetCore.Identity;
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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);

        builder.Entity<User>().ToTable("aspnet_users");
        builder.Entity<UserRole>().ToTable("aspnet_roles");
        builder.Entity<IdentityUserRole<string>>().ToTable("aspnet_user_roles");
        builder.Entity<IdentityUserClaim<string>>().ToTable("aspnet_user_claims");
        builder.Entity<IdentityUserLogin<string>>().ToTable("aspnet_user_logins");
        builder.Entity<IdentityRoleClaim<string>>().ToTable("aspnet_role_claims");
        builder.Entity<IdentityUserToken<string>>().ToTable("aspnet_user_tokens");
    }
}
