using Battleships.Domain.Models;

namespace Battleships.Domain;

public class NeighboursService
{
    public List<Point> GetNeighboursAndItself(Point itself)
        => GetNeighbours(itself)
            .Union(new[] { new Point(itself.X, itself.Y) })
            .ToList();
    
    public List<Point> GetNeighbours(Point itself)
        =>  new Point[]
            {
                new(itself.X - 1, itself.Y - 1),
                new(itself.X - 1, itself.Y),
                new(itself.X - 1, itself.Y + 1),
                new(itself.X, itself.Y - 1),
                new(itself.X, itself.Y + 1),
                new(itself.X + 1, itself.Y - 1),
                new(itself.X + 1, itself.Y),
                new(itself.X + 1, itself.Y + 1)
            }.Where(field =>
                field.X is >= 0 and < BoardGeneratorService.BoardSize &&
                field.Y is >= 0 and < BoardGeneratorService.BoardSize)
            .ToList();
}