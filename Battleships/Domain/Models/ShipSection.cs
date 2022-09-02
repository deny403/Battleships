namespace Battleships.Domain.Models;

public record ShipSection(Point Point)
{
    public ShipSection(int x, int y) : this(new Point(x, y))
    {
    }

    public bool IsHit { get; set; }
}