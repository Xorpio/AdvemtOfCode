using PuzzleConsole.Year2022.Day6;

namespace PuzzleConsole.test.Year2022;

[Trait("Year", "2022")]
[Trait("Day", "6")]
public partial class Day6Tests
{
    [Scenario]
    public void SolveTests(ScenarioContext scenario)
    {
        var sut = new Day6();

        scenario.Fact("Sut should not be null", () =>
        {
            sut.Should().NotBeNull();
        });
    }
}