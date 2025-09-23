using KittenFactory.Api.Features.Kittens.Entities;
using KittenFactory.Api.Features.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KittenFactory.Api.Features.Orders.Entities;

public class Order
{
    public int Id { get; set; }

    public IReadOnlyCollection<Kitten> Kittens { get; set; } = new List<Kitten>();

    public User OrderedBy { get; set; } = null!;
    public string OrderedById { get; set; } = null!;

    public DateTime? OrderedAtUtc { get; set; }
}

file class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasOne(o => o.OrderedBy).WithMany(o => o.Orders).HasForeignKey(o => o.OrderedById);
        builder.HasMany(o => o.Kittens).WithOne(o => o.Order);
    }
}
