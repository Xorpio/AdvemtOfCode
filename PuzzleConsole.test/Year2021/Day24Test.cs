using PuzzleConsole.Year2021.Day24;

namespace PuzzleConsole.test.Year2021;

[Trait("Year", "2021")]
[Trait("Day", "24")]
public partial class Day24Test {
    [Scenario]
    public void SolveTest(ScenarioContext scenario)
    {
        var sut = new Day24();

        scenario.Fact("Sut should not be null", () =>
        {
            sut.Should().NotBeNull();
        });
    }
}
