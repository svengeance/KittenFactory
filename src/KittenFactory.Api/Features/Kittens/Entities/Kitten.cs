using KittenFactory.Api.Features.Orders.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KittenFactory.Api.Features.Kittens.Entities;

public class Kitten
{
    public int Id { get; set; }

    public required KittenCustomizationV1 Customization { get; set; }

    public Order? Order { get; set; }
    public int? OrderId { get; set; }
}

file class KittenConfiguration : IEntityTypeConfiguration<Kitten>
{
    public void Configure(EntityTypeBuilder<Kitten> builder)
        => builder.OwnsOne(o => o.Customization, c =>
        {
            c.ToJson();
            c.Property(p => p.Name).HasMaxLength(128);
            c.Property(p => p.Color).HasMaxLength(64);
        });
}
