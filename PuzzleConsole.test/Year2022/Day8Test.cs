using PuzzleConsole.Year2022.Day8;

namespace PuzzleConsole.test.Year2022;

[Trait("Year", "2022")]
[Trait("Day", "8")]
public partial class Day8Test
{
    [Scenario]
    public void SolveTest(ScenarioContext scenario)
    {
        var sut = new Day8();

        scenario.Fact("Sut should not be null", () =>
        {
            sut.Should().NotBeNull();
        });
    }
}