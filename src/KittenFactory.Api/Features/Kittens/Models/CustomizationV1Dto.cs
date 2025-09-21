namespace KittenFactory.Api.Features.Kittens.Models;

public record CustomizationV1Dto(
    string Name,
    int Age,
    string Color,
    double Weight
);
