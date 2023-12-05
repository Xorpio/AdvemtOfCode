using PuzzleConsole.Year2015.Day3;

namespace PuzzleConsole.test.Year2015;

public partial class Day3Test
{
    [Scenario]
    public void SolveTest(ScenarioContext scenario)
    {
        var sut = new Day3();

        scenario.Fact("Sut should not be null", () =>
        {
            sut.Should().NotBeNull();
        });

        var puzles = new (string Puzzle, string res)[]
        {
            (">", "2"),
            ("^>v<", "4"),
            ("^v^v^v^v^v", "2"),
        };

        foreach (var p in puzles)
        {
            scenario.Theory("Puzzle moet kloppen", p, () =>
            {
                sut.Solve(new[] { p.Puzzle })[0].Should().Be(p.res);
            });
        }

        var puzles2 = new (string Puzzle, string res)[]
        {
            ("^v", "3"),
            ("^>v<", "3"),
            ("^v^v^v^v^v", "11"),
        };

        foreach (var p in puzles2)
        {
            scenario.Theory("Puzzle 2 moet kloppen", p, () =>
            {
                sut.Solve(new[] { p.Puzzle })[1].Should().Be(p.res);
            });
        }
    }
}