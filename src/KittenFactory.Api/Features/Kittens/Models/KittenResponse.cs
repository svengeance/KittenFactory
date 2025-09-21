namespace KittenFactory.Api.Features.Kittens.Models;

public record KittenResponse(
    int Id,
    CustomizationV1Dto Customization
);
