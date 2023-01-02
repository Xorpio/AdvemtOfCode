using PuzzleConsole.Year2022.Day25;

namespace PuzzleConsole.test.Year2022;

[Trait("Year", "2022")]
[Trait("Day", "25")]
public partial class Day25Test2022 {
    [Scenario]
    public void SolveTest(ScenarioContext scenario)
    {
        var sut = new Day25();

        var sampleInput = """
        SampleInput
        """;
        var lines = sampleInput.Split("\n").Select(s => s.Trim()).ToArray();

        scenario.Fact("Sut should not be null", () =>
        {
            sut.Should().NotBeNull();
        });

        scenario.Fact("Small sample test", () =>
        {
            sut.Solve(lines)[0].Should().Be(lines[0]);
        });
    }
}