using KittenFactory.Api.Features.Orders.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KittenFactory.Api.Features.Users.Entities;

public class User : IdentityUser
{
    public IReadOnlyCollection<Order> Orders { get; set; } = [];
}

file class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
        => builder.HasMany(o => o.Orders).WithOne(o => o.OrderedBy);
}
