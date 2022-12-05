using PuzzleConsole.Year2022.Day4;

namespace PuzzleConsole.test.Year2022;

[Trait("Year", "2022")]
[Trait("Day", "4")]
public partial class Day4Test {
    [Scenario]
    public void SolveTest(ScenarioContext scenario)
    {
        var sut = new Day4();

        scenario.Fact("Sut should not be null", () =>
        {
            sut.Should().NotBeNull();
        });

        var sampleInput = """
        2-4,6-8
        2-3,4-5
        5-7,7-9
        2-8,3-7
        6-6,4-6
        2-6,4-8
        """;
        var lines = sampleInput.Split("\n");

        scenario.Fact("Solve part1 should have result", () =>
        {
            sut.Solve(lines)[0].Should().Be("2");
        });

        scenario.Fact("Solve part 2should have result", () =>
        {
            sut.Solve(lines)[1].Should().Be("4");
        });
    }
}