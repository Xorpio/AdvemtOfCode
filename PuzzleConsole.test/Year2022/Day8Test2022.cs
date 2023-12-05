using PuzzleConsole.Year2022.Day8;

namespace PuzzleConsole.test.Year2022;

[Trait("Year", "2022")]
[Trait("Day", "8")]
public partial class Day8Test2022
{
    [Scenario]
    public void SolveTest(ScenarioContext scenario)
    {
        var sut = new Day8();

        var sampleInput = """
        30373
        25512
        65332
        33549
        35390
        """;
        var lines = sampleInput.Split("\n").Select(s => s.Trim()).ToArray();

        scenario.Fact("Sut should not be null", () =>
        {
            sut.Should().NotBeNull();
        });

        scenario.Fact("Solve moet part 1", () =>
        {
            int.Parse(sut.Solve(lines)[0]).Should().Be(21);
        });

        scenario.Fact("Solve moet part 2", () =>
        {
            int.Parse(sut.Solve(lines)[1]).Should().Be(8);
        });

        var sampleInputPart2 = new (string p, int a)[]
        {
            (
                """
            999
            919
            999
            """, 1),
            (
                """
            111
            191
            111
            """, 1),
        };

        foreach (var p in sampleInputPart2)
        {
            scenario.Theory("Solve moet part 2 - samples", p, () =>
            {
                var l = p.p.Split("\n").Select(s => s.Trim()).ToArray();
                int.Parse(sut.Solve(l)[1]).Should().Be(p.a);
            });
        }
    }
}