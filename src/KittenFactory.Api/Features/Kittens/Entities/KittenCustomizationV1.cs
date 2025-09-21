using Microsoft.EntityFrameworkCore;

namespace KittenFactory.Api.Features.Kittens.Entities;

[Owned]
public class KittenCustomizationV1
{
    public int Version { get; } = 1;

    public required string Name { get; init; }
    public required int Age { get; init; }
    public required string Color { get; init; }
    public required double Weight { get; init; }
}
