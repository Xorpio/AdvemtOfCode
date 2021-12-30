using PuzzleConsole.Year2021.Day23;

namespace PuzzleConsole.test.Year2021;

[Trait("Year", "2021")]
[Trait("Day", "23")]
public partial class Day23Test {
    [Scenario]
    public void SolveTest(ScenarioContext scenario)
    {
        var sut = new Day23();

        scenario.Fact("Sut should not be null", () =>
        {
            sut.Should().NotBeNull();
        });
    }
}
