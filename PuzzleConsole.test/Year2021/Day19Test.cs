using PuzzleConsole.Year2021.Day19;

namespace PuzzleConsole.test.Year2021;

public partial class Day19Test
{
    [Scenario]
    public void SolveTest(ScenarioContext scenario)
    {
        var sut = new Day19();

        scenario.Fact("Sut should not be null", () =>
        {
            sut.Should().NotBeNull();
        });
    }
}
