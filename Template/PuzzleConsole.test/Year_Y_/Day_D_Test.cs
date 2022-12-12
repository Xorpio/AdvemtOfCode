using PuzzleConsole.Year_Y_.Day_D_;

namespace PuzzleConsole.test.Year_Y_;

[Trait("Year", "_Y_")]
[Trait("Day", "_D_")]
public partial class Day_D_Test {
    [Scenario]
    public void SolveTest(ScenarioContext scenario)
    {
        var sut = new Day_D_();

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