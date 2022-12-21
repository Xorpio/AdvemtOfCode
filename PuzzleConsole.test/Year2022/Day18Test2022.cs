using PuzzleConsole.Year2022.Day18;

namespace PuzzleConsole.test.Year2022;

[Trait("Year", "2022")]
[Trait("Day", "18")]
public partial class Day18Test2022
{
    [Scenario]
    public void SolveTest(ScenarioContext scenario)
    {
        var sut = new Day18();

        var sampleInput = """
        2,2,2
        1,2,2
        3,2,2
        2,1,2
        2,3,2
        2,2,1
        2,2,3
        2,2,4
        2,2,6
        1,2,5
        3,2,5
        2,1,5
        2,3,5
        """;
        var lines = sampleInput.Split("\n").Select(s => s.Trim()).ToArray();

        scenario.Fact("Sut should not be null", () =>
        {
            sut.Should().NotBeNull();
        });

        scenario.Fact("Small sample test", () =>
        {
            sut.Solve(lines)[0].Should().Be("64");
        });

        scenario.Fact("Small sample test part 2", () =>
        {
            sut.Solve(lines)[1].Should().Be("58");
        });
    }
}