using System.Collections.Generic;
using Battleships.Domain;
using Battleships.Domain.Models;
using FluentAssertions;
using Xunit;

namespace Battleship.Tests;

public class BoardValidatorTests
{
    private readonly ShipsValidatorService _subject = new(new NeighboursService());

    [Fact]
    public void IsValid_WhenEmptyBoard_ThenReturnsTrue()
    {
        // Act
        var isValid = _subject.IsValid(new List<Ship>());

        // Assert
        isValid.Should().BeTrue();
    }

    [Fact]
    public void IsValid_WhenValidBoard_ThenReturnsTrue()
    {
        // Arrange
        var ships = new List<Ship>
        {
            new(new List<ShipSection> { new(0, 0), new(0, 1), new(0, 2), new(0, 3), new(0, 4) }),
            new(new List<ShipSection> { new(2, 0), new(2, 1), new(2, 2), new(2, 3) }),
            new(new List<ShipSection> { new(5, 0), new(5, 1), new(5, 2), new(5, 3) })
        };

        // Act
        var isValid = _subject.IsValid(ships);

        // Assert
        isValid.Should().BeTrue();
    }

    [Fact]
    public void IsValid_WhenShipsAreTooClose_Case1_ThenReturnsFalse()
    {
        // Arrange
        var ships = new List<Ship>
        {
            new(new List<ShipSection> { new(0, 0), new(0, 1), new(0, 2), new(0, 3), new(0, 4) }),
            new(new List<ShipSection> { new(1, 5), new(1, 6), new(1, 7), new(1, 8) }),
            new(new List<ShipSection> { new(5, 5), new(5, 6), new(5, 7), new(5, 8) })
        };

        // Act
        var isValid = _subject.IsValid(ships);

        // Assert
        isValid.Should().BeFalse();
    }

    [Fact]
    public void IsValid_WhenShipsAreTooClose_Case2_ThenReturnsFalse()
    {
        // Arrange
        var ships = new List<Ship>
        {
            new(new List<ShipSection> { new(9, 5), new(9, 6), new(9, 7), new(9, 8), new(9, 9) }),
            new(new List<ShipSection> { new(8, 2), new(8, 3), new(8, 4), new(8, 5) }),
            new(new List<ShipSection> { new(5, 5), new(5, 6), new(5, 7), new(5, 8) })
        };

        // Act
        var isValid = _subject.IsValid(ships);

        // Assert
        isValid.Should().BeFalse();
    }
}