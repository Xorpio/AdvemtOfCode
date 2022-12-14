using PuzzleConsole.Year2022.Day14;

namespace PuzzleConsole.test.Year2022;

[Trait("Year", "2022")]
[Trait("Day", "14")]
public partial class Day14Test2022
{
    [Scenario]
    public void SolveTest(ScenarioContext scenario)
    {
        var sut = new Day14();

        var sampleInput = """
        498,4 -> 498,6 -> 496,6
        503,4 -> 502,4 -> 502,9 -> 494,9
        """;
        var lines = sampleInput.Split("\n").Select(s => s.Trim()).ToArray();

        scenario.Fact("Sut should not be null", () =>
        {
            sut.Should().NotBeNull();
        });

        scenario.Fact("Small sample test", () =>
        {
            int.Parse(sut.Solve(lines)[0]).Should().Be(24);
        });
    }
}