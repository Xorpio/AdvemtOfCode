using System.Linq;
using PuzzleConsole.Year2021;
using Xunit;

namespace PuzzleConsole.test.Year2021;

public class Day5Test
{
    [Fact]
    public void SolveTest()
    {
        var sut = new Day5();

        var puzzle = new string[]
        {
            "0,9 -> 5,9",
            "8,0 -> 0,8",
            "9,4 -> 3,4",
            "2,2 -> 2,1",
            "7,0 -> 7,4",
            "6,4 -> 2,0",
            "0,9 -> 2,9",
            "3,4 -> 1,4",
            "0,0 -> 8,8",
            "5,5 -> 8,2",
        };

        var solution = sut.Solve(puzzle);

        solution.Should().HaveCount(2);
        solution.First().Should().Be("5");
        solution.Last().Should().Be("12");
    }
}
