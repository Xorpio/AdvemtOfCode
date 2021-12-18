using PuzzleConsole.Year2021.Day2;
using Xunit;

namespace PuzzleConsole.test.Year2021;

public class Day2Test
{
    [Fact]
    public void SolveTest()
    {
        var sut = new Day2();

        var puzzle = new string[]
        {
            "forward 5",
            "down 5",
            "forward 8",
            "up 3",
            "down 8",
            "forward 2"
        };

        var solution = sut.Solve(puzzle);

        solution.Should().HaveCount(2);
        solution.First().Should().Be("150", "900");
    }
}
