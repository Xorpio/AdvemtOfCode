using PuzzleConsole.Year2021.Day6;
using Xunit;

namespace PuzzleConsole.test.Year2021;

public class Day6Test
{
    [Fact]
    public void SolveTest()
    {
        var sut = new Day6();

        var puzzle = new string[] { "3,4,3,1,2" };

        var solution = sut.Solve(puzzle);

        solution.Should().HaveCount(3);
        solution.First().Should().Be("Day 18: 26");
        solution[1].Should().Be("Day 80: 5934");
        solution[2].Should().Be("Day 256: 26984457539");
    }
}
