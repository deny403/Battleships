using Battleships.Domain;
using Battleships.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Battleships;

internal static class Di
{
    internal static ServiceProvider RegisterDi()
        => new ServiceCollection()
            .AddScoped<IBoardGeneratorService, BoardGeneratorService>()
            .AddScoped<ShipsValidatorService>()
            .AddScoped<GameService>()
            .AddScoped<NeighboursService>()
            .AddScoped<HitMap>()
            .AddScoped<MapPrinter>()
            .AddScoped<ConsoleUi>()
            .BuildServiceProvider();
}