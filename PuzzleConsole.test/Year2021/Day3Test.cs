using PuzzleConsole.Year2021.Day3;
using Xunit;

namespace PuzzleConsole.test.Year2021;

public class Day3Test
{
    [Fact]
    public void SolveTest()
    {
        var sut = new Day3();

        var puzzle = new string[]
        {
            "00100",
            "11110",
            "10110",
            "10111",
            "10101",
            "01111",
            "00111",
            "11100",
            "10000",
            "11001",
            "00010",
            "01010"
        };

        var solution = sut.Solve(puzzle);

        solution.Should().HaveCount(2);
        solution.First().Should().Be("198");
        solution.Last().Should().Be("230");
    }
}
