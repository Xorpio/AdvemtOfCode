using PuzzleConsole.Year2022.Day3;

namespace PuzzleConsole.test.Year2022;

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
    }
}