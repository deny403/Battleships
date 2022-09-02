using Battleships.Domain.Models;

namespace Battleships.Domain;

public class HitMap
{
    private readonly NeighboursService _neighboursService;

    public enum HitResult
    {
        None,
        Miss,
        Hit
    }

    public HitMap(NeighboursService neighboursService)
    {
        _neighboursService = neighboursService;
    }

    public readonly HitResult[,] Map = new HitResult[BoardGeneratorService.BoardSize, BoardGeneratorService.BoardSize];

    public void AddHit(Point firePoint, GameService.FireResult fireResult, Ship ship)
    {
        Map[firePoint.X, firePoint.Y] = fireResult switch
        {
            GameService.FireResult.Misses => HitResult.Miss,
            GameService.FireResult.Hits => HitResult.Hit,
            GameService.FireResult.Sinks => HitResult.Hit,
            _ => throw new ArgumentOutOfRangeException()
        };

        if (fireResult == GameService.FireResult.Sinks)
        {
            var pointsAroundSunkShip = ship.Sections
                .SelectMany(s => _neighboursService.GetNeighbours(s.Point))
                .Where(neighbour => ship.Sections.All(section => section.Point != neighbour));
            pointsAroundSunkShip.ToList().ForEach(point => { Map[point.X, point.Y] = HitResult.Miss; });
        }
    }
}