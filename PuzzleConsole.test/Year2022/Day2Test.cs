using PuzzleConsole.Year2022.Day2;

namespace PuzzleConsole.test.Year2022;

public partial class Day2Test
{
    [Scenario]
    public void SolveTest(ScenarioContext scenario)
    {
        var sut = new Day2();

        scenario.Fact("Sut should not be null", () =>
        {
            sut.Should().NotBeNull();
        });

        var puzzle = """
A Y
B X
C Z
""";

        var puzzleLines = puzzle.Split("\r\n");

        scenario.Fact("PuzzleSolve test", () =>
        {
            sut.Solve(puzzleLines)[0].Should().Be("15");
            sut.Solve(puzzleLines)[1].Should().Be("12");
        });
    }
}