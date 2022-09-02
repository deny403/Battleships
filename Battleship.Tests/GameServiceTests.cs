using System.Collections.Generic;
using Battleships.Domain;
using Battleships.Domain.Interfaces;
using Battleships.Domain.Models;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Battleship.Tests;

public class GameServiceTests
{
    private readonly GameService _subject;

    public GameServiceTests()
    {
        var boardGeneratorService = Substitute.For<IBoardGeneratorService>();
        boardGeneratorService.GenerateShips().Returns(CreateDefaultShips());
        _subject = new GameService(boardGeneratorService, new HitMap(new NeighboursService()));
    }

    private static List<Ship> CreateDefaultShips()
        => new()
        {
            new(new List<ShipSection> { new(0, 0), new(0, 1), new(0, 2), new(0, 3), new(0, 4) }),
            new(new List<ShipSection> { new(2, 0), new(2, 1), new(2, 2), new(2, 3) }),
            new(new List<ShipSection> { new(9, 6), new(9, 7), new(9, 8), new(9, 9) })
        };

    [Theory]
    [InlineData(0, 0)]
    [InlineData(0, 1)]
    [InlineData(0, 2)]
    [InlineData(0, 3)]
    [InlineData(0, 4)]
    [InlineData(2, 0)]
    [InlineData(2, 1)]
    [InlineData(2, 2)]
    [InlineData(2, 3)]
    [InlineData(9, 6)]
    [InlineData(9, 7)]
    [InlineData(9, 8)]
    [InlineData(9, 9)]
    public void Shoot_WhenHitShip_ThenReturnsHit(int x, int y)
    {
        // Act, Assert
        _subject.Shoot(new Point(x, y)).Should().Be(GameService.FireResult.Hits);
    }

    [Theory]
    [InlineData(1, 0)]
    [InlineData(5, 5)]
    [InlineData(3, 3)]
    [InlineData(8, 9)]
    public void Shoot_WhenMissesShip_ThenReturnsMisses(int x, int y)
    {
        // Act, Assert
        _subject.Shoot(new Point(x, y)).Should().Be(GameService.FireResult.Misses);
    }

    [Fact]
    public void Shoot_WhenSinksShip_ThenReturnsSinks()
    {
        // Arrange
        _subject.Shoot(new Point(2, 0));
        _subject.Shoot(new Point(2, 1));
        _subject.Shoot(new Point(2, 2));

        // Act, Assert
        _subject.Shoot(new Point(2, 3)).Should().Be(GameService.FireResult.Sinks);
    }

    [Fact]
    public void IsGameOver_WhenNoShipsHit_ThenReturnsFalse()
    {
        // Act, Assert
        _subject.IsGameOver().Should().BeFalse();
    }

    [Fact]
    public void IsGameOver_WhenNotAllTheShipsSunk_ThenReturnsFalse()
    {
        // Arrange
        _subject.Shoot(new Point(0, 0));
        _subject.Shoot(new Point(0, 1));
        _subject.Shoot(new Point(0, 2));
        _subject.Shoot(new Point(0, 3));
        _subject.Shoot(new Point(0, 4));

        _subject.Shoot(new Point(9, 0));
        _subject.Shoot(new Point(9, 1));
        _subject.Shoot(new Point(9, 2));
        _subject.Shoot(new Point(9, 3));

        // Act, Assert
        _subject.IsGameOver().Should().BeFalse();
    }

    [Fact]
    public void IsGameOver_WhenAllTheShipsSunk_ThenReturnsTrue()
    {
        // Arrange
        _subject.Shoot(new Point(0, 0));
        _subject.Shoot(new Point(0, 1));
        _subject.Shoot(new Point(0, 2));
        _subject.Shoot(new Point(0, 3));
        _subject.Shoot(new Point(0, 4));

        _subject.Shoot(new Point(2, 0));
        _subject.Shoot(new Point(2, 1));
        _subject.Shoot(new Point(2, 2));
        _subject.Shoot(new Point(2, 3));

        _subject.Shoot(new Point(9, 6));
        _subject.Shoot(new Point(9, 7));
        _subject.Shoot(new Point(9, 8));
        _subject.Shoot(new Point(9, 9));

        // Act, Assert
        _subject.IsGameOver().Should().BeTrue();
    }
}