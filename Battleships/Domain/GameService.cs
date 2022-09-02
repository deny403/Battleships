using Battleships.Domain.Interfaces;
using Battleships.Domain.Models;

namespace Battleships.Domain;

public class GameService
{
    private readonly HitMap _hitMap;
    private readonly Lazy<List<Ship>> _ships;
    private List<Ship> Ships => _ships.Value;

    public enum FireResult
    {
        Misses,
        Hits,
        Sinks
    }

    public GameService(IBoardGeneratorService boardGeneratorService, HitMap hitMap)
    {
        _hitMap = hitMap;
        _ships = new Lazy<List<Ship>>(boardGeneratorService.GenerateShips);
    }

    public FireResult Shoot(Point firePoint)
    {
        foreach (var ship in Ships)
        {
            var hitSection = ship.Sections.SingleOrDefault(section => section.Point == firePoint);
            if (hitSection != null)
            {
                hitSection.IsHit = true;
                var fireResult = ship.Sections.All(section => section.IsHit) ? FireResult.Sinks : FireResult.Hits;
                _hitMap.AddHit(firePoint, fireResult, ship);
                return fireResult;
            }
        }

        _hitMap.AddHit(firePoint, FireResult.Misses, Ship.None);
        return FireResult.Misses;
    }
    
    public bool IsGameOver() => Ships.All(ship => ship.Sections.All(section => section.IsHit));
}