namespace Battleships.Domain.Models;

public record Ship(List<ShipSection> Sections)
{
    public static Ship None { get; } = new(new List<ShipSection>());
}