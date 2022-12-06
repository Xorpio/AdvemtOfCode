using PuzzleConsole.Year2015.Day4;

namespace PuzzleConsole.test.Year2015;

[Trait("Year", "2015")]
[Trait("Day", "4")]
public partial class Day4Test
{
    [Scenario]
    public void SolveTest(ScenarioContext scenario)
    {
        var sut = new Day4();

        scenario.Fact("Sut should not be null", () =>
        {
            sut.Should().NotBeNull();
        });
    }
}