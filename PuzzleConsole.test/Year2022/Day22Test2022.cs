using PuzzleConsole.Year2022.Day22;

namespace PuzzleConsole.test.Year2022;

[Trait("Year", "2022")]
[Trait("Day", "22")]
public partial class Day22Test2022 {
    [Scenario]
    public void SolveTest(ScenarioContext scenario)
    {
        var sut = new Day22();

        var sampleInput = """
                ...#
                .#..
                #...
                ....
        ...#.......#
        ........#...
        ..#....#....
        ..........#.
                ...#....
                .....#..
                .#......
                ......#.

        10R5L5R10L4R5L5
        """;
        var lines = sampleInput.Split("\n").ToArray();

        scenario.Fact("Sut should not be null", () =>
        {
            sut.Should().NotBeNull();
        });

        scenario.Fact("Small sample test", () =>
        {
            sut.Solve(lines)[0].Should().Be("6032");
        });
    }
}