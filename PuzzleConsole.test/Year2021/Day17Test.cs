using PuzzleConsole.Year2021.Day17;

namespace PuzzleConsole.test.Year2021;

public partial class Day17Test
{
    [Scenario]
    public void SolveTest(ScenarioContext scenario)
    {
        var sut = new Day17();

        scenario.Fact("Sut should be puzzle", () =>
        {
            sut.Should().NotBeNull();
        });
    }
}
