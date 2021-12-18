using PuzzleConsole.Year2021.Day7;
using Xunit;

namespace PuzzleConsole.test.Year2021;

public class Day7Test
{
    [Fact]
    public void SolveTest()
    {
        var sut = new Day7();

        var puzzle = new string[] { "16,1,2,0,4,2,7,1,2,14" };

        var solution = sut.Solve(puzzle);

        solution.First().Should().Be("37");
        solution.Last().Should().Be("168");
    }
}
