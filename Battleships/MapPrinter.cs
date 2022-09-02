using Battleships.Domain;

namespace Battleships;

internal class MapPrinter
{
    private readonly HitMap _hitMap;

    public MapPrinter(HitMap hitMap)
    {
        _hitMap = hitMap;
    }

    internal void PrintMap()
    {
        Console.WriteLine();
        Console.Write("   ABCDEFGHIJ");
        for (var y = 0; y < BoardGeneratorService.BoardSize; y++)
        {
            Console.WriteLine();
            Console.Write(y + 1);
            Console.Write(y < 9 ? "  " : " ");

            for (var x = 0; x < BoardGeneratorService.BoardSize; x++)
            {
                Console.Write(_hitMap.Map[x, y] switch
                {
                    HitMap.HitResult.Hit => "X",
                    HitMap.HitResult.Miss => "O",
                    HitMap.HitResult.None => " ",
                    _ => throw new ArgumentOutOfRangeException()
                });
            }
        }

        Console.WriteLine();
    }
}