using System.Collections.Generic;
using Battleships.Domain;
using Battleships.Domain.Models;
using FluentAssertions;
using Xunit;

namespace Battleship.Tests;

public class HitMapTests
{
    private readonly HitMap _subject = new(new NeighboursService());

    [Fact]
    public void AddHit_WhenAnotherCellWasFiredTo_ThenCellHasNoneStatus()
    {
        // Act
        _subject.AddHit(new Point(1, 1), GameService.FireResult.Misses, Ship.None);

        // Assert
        _subject.Map[5, 5].Should().Be(HitMap.HitResult.None);
    }

    [Fact]
    public void AddHit_WhenMisses_ThenAddsMissToMap()
    {
        // Act
        _subject.AddHit(new Point(5, 5), GameService.FireResult.Misses, Ship.None);

        // Assert
        _subject.Map[5, 5].Should().Be(HitMap.HitResult.Miss);
    }

    [Fact]
    public void AddHit_WhenHitsAndShipIsNotSunk_ThenAddsHitsToMap()
    {
        // Act
        _subject.AddHit(new Point(5, 5), GameService.FireResult.Hits, Ship.None);

        // Assert
        _subject.Map[5, 5].Should().Be(HitMap.HitResult.Hit);
    }

    [Fact]
    public void AddHit_WhenHitsAndShipSunks_ThenAddsSunkToMapAndAddMissesAroundTheShip()
    {
        // Act

        var ship = new Ship(new List<ShipSection>
            {
                new(2, 0), new(2, 1), new(2, 2), new(2, 3)
            }
        );

        _subject.AddHit(ship.Sections[0].Point, GameService.FireResult.Hits, ship);
        _subject.AddHit(ship.Sections[1].Point, GameService.FireResult.Hits, ship);
        _subject.AddHit(ship.Sections[2].Point, GameService.FireResult.Hits, ship);
        _subject.AddHit(ship.Sections[3].Point, GameService.FireResult.Sinks, ship);

        // Assert
        _subject.Map[2, 0].Should().Be(HitMap.HitResult.Hit);
        _subject.Map[2, 1].Should().Be(HitMap.HitResult.Hit);
        _subject.Map[2, 2].Should().Be(HitMap.HitResult.Hit);
        _subject.Map[2, 3].Should().Be(HitMap.HitResult.Hit);

        _subject.Map[1, 0].Should().Be(HitMap.HitResult.Miss);
        _subject.Map[1, 1].Should().Be(HitMap.HitResult.Miss);
        _subject.Map[1, 2].Should().Be(HitMap.HitResult.Miss);
        _subject.Map[1, 3].Should().Be(HitMap.HitResult.Miss);
        _subject.Map[1, 4].Should().Be(HitMap.HitResult.Miss);

        _subject.Map[2, 4].Should().Be(HitMap.HitResult.Miss);

        _subject.Map[3, 0].Should().Be(HitMap.HitResult.Miss);
        _subject.Map[3, 1].Should().Be(HitMap.HitResult.Miss);
        _subject.Map[3, 2].Should().Be(HitMap.HitResult.Miss);
        _subject.Map[3, 3].Should().Be(HitMap.HitResult.Miss);
        _subject.Map[3, 4].Should().Be(HitMap.HitResult.Miss);
    }
}