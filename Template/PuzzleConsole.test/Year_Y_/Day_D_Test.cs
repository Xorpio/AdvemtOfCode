using PuzzleConsole.Year_Y_.Day_D_;

namespace PuzzleConsole.test.Year_Y_;

public partial class Day_D_Test
{
    [Scenario]
    public void SolveTest(ScenarioContext scenario)
    {
        var sut = new Day_D_();

        scenario.Fact("Sut should not be null", () =>
        {
            sut.Should().NotBeNull();
        });
    }
}
