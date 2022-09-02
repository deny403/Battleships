using Battleships.Domain.Models;

namespace Battleships.Domain.Interfaces;

public interface IBoardGeneratorService
{
    List<Ship> GenerateShips();
}