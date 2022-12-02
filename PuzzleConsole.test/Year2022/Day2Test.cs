using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Newtonsoft.Json.Linq;
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

        var inputs = new (string Line, int Result)[]
        {
            ("A Y", (1 + 3)),
            ("A X", (3 + 0)),
            ("A Z", (2 + 6)),
            ("B Y", (2 + 3)),
            ("B X", (1 + 0)),
            ("B Z", (3 + 6)),
            ("C Y", (3 + 3)),
            ("C X", (2 + 0)),
            ("C Z", (1 + 6))
        };

        foreach (var line in inputs)
        {
            scenario.Theory("Line moet goed zijn", line, () =>
            {
                sut.Solve(new[] { line.Line })[1].Should().Be($"{line.Result}");
            });
        }

    }
}