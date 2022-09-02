using Battleships.Domain;
using Battleships.Domain.Models;
using FluentAssertions;
using Xunit;

namespace Battleship.Tests;

public class NeighboursServiceTests
{
    private readonly NeighboursService _subject = new();

    [Fact]
    public void GetNeighbours_Case1_ThenReturnsCorrectNeighbours()
    {
        // Act
        var neighbours = _subject.GetNeighbours(new Point(0, 0));

        // Assert
        neighbours.Should().BeEquivalentTo(
            new Point[] { new(0, 1), new(1, 0), new(1, 1) });
    }

    [Fact]
    public void GetNeighbours_Case2_ThenReturnsCorrectNeighbours()
    {
        // Act
        var neighbours = _subject.GetNeighbours(new Point(0, 5));

        // Assert
        neighbours.Should().BeEquivalentTo(
            new Point[] { new(0, 4), new(1, 4), new(1, 5), new(1, 6), new(0, 6) });
    }

    [Fact]
    public void GetNeighbours_Case3_ThenReturnsCorrectNeighbours()
    {
        // Act
        var neighbours = _subject.GetNeighbours(new Point(9, 9));

        // Assert
        neighbours.Should().BeEquivalentTo(
            new Point[] { new(8, 9), new(8, 8), new(9, 8) });
    }

    [Fact]
    public void GetNeighbours_Case4_ThenReturnsCorrectNeighbours()
    {
        // Act
        var neighbours = _subject.GetNeighbours(new Point(5, 5));

        // Assert
        neighbours.Should().BeEquivalentTo(
            new Point[]
            {
                new(4, 4), new(4, 5), new(4, 6),
                new(5, 4), new(5, 6),
                new(6, 4), new(6, 5), new(6, 6)
            }
        );
    }

    [Fact]
    public void GetNeighboursAndItself_Case1_ThenReturnsCorrectNeighbours()
    {
        // Act
        var neighbours = _subject.GetNeighboursAndItself(new Point(0, 0));

        // Assert
        neighbours.Should().BeEquivalentTo(
            new Point[] { new(0, 0), new(0, 1), new(1, 0), new(1, 1) });
    }

    [Fact]
    public void GetNeighboursAndItself_Case2_ThenReturnsCorrectNeighbours()
    {
        // Act
        var neighbours = _subject.GetNeighboursAndItself(new Point(0, 5));

        // Assert
        neighbours.Should().BeEquivalentTo(
            new Point[]
            {
                new(0, 4), new(1, 4),
                new(0, 5), new(1, 5),
                new(1, 6), new(0, 6)
            });
    }

    [Fact]
    public void GetNeighboursAndItself_Case3_ThenReturnsCorrectNeighbours()
    {
        // Act
        var neighbours = _subject.GetNeighboursAndItself(new Point(9, 9));

        // Assert
        neighbours.Should().BeEquivalentTo(
            new Point[] { new(8, 9), new(8, 8), new(9, 8), new(9, 9) });
    }

    [Fact]
    public void GetNeighboursAndItself_Case4_ThenReturnsCorrectNeighbours()
    {
        // Act
        var neighbours = _subject.GetNeighboursAndItself(new Point(5, 5));

        // Assert
        neighbours.Should().BeEquivalentTo(
            new Point[]
            {
                new(4, 4), new(4, 5), new(4, 6),
                new(5, 4), new(5, 5), new(5, 6),
                new(6, 4), new(6, 5), new(6, 6)
            }
        );
    }
}