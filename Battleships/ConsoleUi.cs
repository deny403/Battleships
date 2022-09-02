using Battleships.Domain;
using Battleships.Domain.Models;

namespace Battleships;

internal class ConsoleUi
{
    readonly GameService _gameService;
    readonly MapPrinter _mapPrinter;

    public ConsoleUi(GameService gameService, MapPrinter mapPrinter)
    {
        _gameService = gameService;
        _mapPrinter = mapPrinter;
    }

    internal void Run()
    {
        _mapPrinter.PrintMap();
        do
        {
            Console.WriteLine("Enter your guess");
            var nextGuess = Console.ReadLine();
            if (nextGuess == null || !ValidateGuess(nextGuess))
            {
                Console.WriteLine("Invalid guess format");
            }
            else
            {
                var firePoint = ParseGuess(nextGuess);
                var shootResult = _gameService.Shoot(firePoint);
                Console.WriteLine(shootResult.ToString());
                _mapPrinter.PrintMap();
            }
        } while (!_gameService.IsGameOver());

        Console.WriteLine("All battleships are sunk. You win!");
    }

    bool ValidateGuess(string? s)
        => !string.IsNullOrWhiteSpace(s)
           && s[0] is >= 'A' and <= 'J'
           && (s.Length == 2 && s[1] is >= '1' and <= '9'
               || s.Length == 3 && s.Substring(1, 2) == "10");

    Point ParseGuess(string guess)
        => new(guess[0] - 'A', int.Parse(guess.Substring(1)) - 1);
}