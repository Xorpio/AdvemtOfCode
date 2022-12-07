using PuzzleConsole.Year2015.Day6;

namespace PuzzleConsole.test.Year2015;

[Trait("Year", "2015")]
[Trait("Day", "6")]
public partial class Day6Test2015
{
    [Scenario]
    public void SolveTest(ScenarioContext scenario)
    {
        var sut = new Day6();

        scenario.Fact("Sut should not be null", () =>
        {
            sut.Should().NotBeNull();
        });

        var puzzles = new (string P, int A)[]
        {
            ("turn on 0,0 through 999,999", 1_000_000),
            ("toggle 0,0 through 999,0", 1_000),
            ("turn on 499,499 through 500,500", 4)
        };

        foreach (var p in puzzles)
        {
            scenario.Theory("Solve", p, () =>
            {
                sut.Solve(new []{ p.P })[0].Should().Be(p.A.ToString());
            });
        }

        var puzzlesPart2 = new (string P, int A)[]
        {
            ("turn on 0,0 through 999,999", 1_000_000),
            ("toggle 0,0 through 999,0", 2_000),
            ("turn on 499,499 through 500,500", 4)
        };

        foreach (var p in puzzlesPart2)
        {
            scenario.Theory("Solve part 2", p, () =>
            {
                sut.Solve(new []{ p.P })[1].Should().Be(p.A.ToString());
            });
        }
    }
}