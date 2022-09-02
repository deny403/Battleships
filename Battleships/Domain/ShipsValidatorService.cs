using Battleships.Domain.Models;

namespace Battleships.Domain;

public class ShipsValidatorService
{
    private readonly NeighboursService _neighboursService;

    public ShipsValidatorService(NeighboursService neighboursService)
    {
        _neighboursService = neighboursService;
    }

    public bool IsValid(List<Ship> ships)
        => ships
            .All(ship =>
                ship.Sections
                    .SelectMany(section => _neighboursService.GetNeighboursAndItself(section.Point))
                    .All(neighbourPoint => ships
                        .Where(potentialConflictShip => potentialConflictShip != ship)
                        .SelectMany(potentialConflictShip => potentialConflictShip.Sections)
                        .All(potentialConflictSection => potentialConflictSection.Point != neighbourPoint)));
}