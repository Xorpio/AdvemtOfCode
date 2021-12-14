using PuzzleConsole.Year2021;
using Xunit;
using ScenarioTests;

namespace PuzzleConsole.test.Year2021;

public partial class Day9Test
{
    [Scenario]
    public void SolveTest(ScenarioContext scenario)
    {
        var sut = new Day9();

        var puzzle = new string[] {
            "2199943210",
            "3987894921",
            "9856789892",
            "8767896789",
            "9899965678"
        };

        var solution = sut.Solve(puzzle);

        scenario.Fact("First result should be 15", () => {
            solution.First().Should().Be("15");
        });

        scenario.Fact("First result should be 1134", () => {
            solution[1].Should().Be("1134");
        });
    }
}
