using Battleships.Domain.Interfaces;
using Battleships.Domain.Models;

namespace Battleships.Domain;

public class BoardGeneratorService : IBoardGeneratorService
{
    public const int BoardSize = 10;
    private static readonly int[] ShipsLengthsToGenerate = { 5, 4, 4 };

    private readonly ShipsValidatorService _shipsValidatorService;

    public BoardGeneratorService(ShipsValidatorService shipsValidatorService)
    {
        _shipsValidatorService = shipsValidatorService;
    }

    public List<Ship> GenerateShips()
    {
        List<Ship> ships;
        do
        {
            ships = GenerateRandomShips(ShipsLengthsToGenerate);
        } while (!_shipsValidatorService.IsValid(ships));

        return ships;
    }

    private static List<Ship> GenerateRandomShips(int[] shipsLengthsToGenerate)
        => shipsLengthsToGenerate.Select(shipLength =>
            {
                var isVertical = Random.Shared.NextDouble() >= 0.5;
                var x = Random.Shared.Next(0, isVertical ? BoardSize : BoardSize - shipLength + 1);
                var y = Random.Shared.Next(0, isVertical ? BoardSize - shipLength + 1 : BoardSize);

                return new Ship(
                    Enumerable.Range(0, shipLength).Select(shipCellIndex => isVertical
                            ? new ShipSection(x, y + shipCellIndex)
                            : new ShipSection(x + shipCellIndex, y))
                        .ToList());
            })
            .ToList();
}