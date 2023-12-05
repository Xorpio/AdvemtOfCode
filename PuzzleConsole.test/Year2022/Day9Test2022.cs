using PuzzleConsole.Year2022.Day9;

namespace PuzzleConsole.test.Year2022;

[Trait("Year", "2022")]
[Trait("Day", "9")]
public partial class Day9Test2022
{
    [Scenario]
    public void SolveTest(ScenarioContext scenario)
    {
        var sut = new Day9();

        scenario.Fact("Sut should not be null", () =>
        {
            sut.Should().NotBeNull();
        });

        var sampleInput = """
        R 4
        U 4
        L 3
        D 1
        R 4
        D 1
        L 5
        R 2
        """;
        var lines = sampleInput.Split("\n").Select(s => s.Trim()).ToArray();

        scenario.Fact("Solve moet part 1", () =>
        {
            int.Parse(sut.Solve(lines)[0]).Should().Be(13);
        });
        scenario.Fact("Solve moet part 2", () =>
        {
            int.Parse(sut.Solve(lines)[1]).Should().Be(1);
        });

        var sampleInputPart2 = """
        R 5
        U 8
        L 8
        D 3
        R 17
        D 10
        L 25
        U 20
        """;
        var lines2 = sampleInputPart2.Split("\n").Select(s => s.Trim()).ToArray();

        scenario.Fact("Solve moet part 2 larger example", () =>
        {
            int.Parse(sut.Solve(lines2)[1]).Should().Be(36);
        });

        var tests = new (Coord Head, Coord Tail, Coord Expected)[]
        {
            //rechts
            (new(0,0), new (0,0), new(0,0)),
            (new(0,1), new (0,0), new(0,0)),
            (new(0,2), new (0,0), new(0,1)),
            //links
            (new(0,0), new (0,0), new(0,0)),
            (new(0,-1), new (0,0), new(0,0)),
            (new(0,-2), new (0,0), new(0,-1)),
            //boven
            (new(0,0), new (0,0), new(0,0)),
            (new(1,0), new (0,0), new(0,0)),
            (new(2,0), new (0,0), new(1,0)),
            //beneden
            (new(0,0), new (0,0), new(0,0)),
            (new(-1,0), new (0,0), new(0,0)),
            (new(-2,0), new (0,0), new(-1,0)),
            //rechytsboven
            (new(0,0), new (0,0), new(0,0)),
            (new(1,1), new (0,0), new(0,0)),
            (new(2,2), new (0,0), new(1,1)),
            //rechtsonder
            (new(0,0), new (0,0), new(0,0)),
            (new(1,-1), new (0,0), new(0,0)),
            (new(2,-2), new (0,0), new(1,-1)),
            //rechytsboven
            (new(0,0), new (0,0), new(0,0)),
            (new(-1,1), new (0,0), new(0,0)),
            (new(-2,2), new (0,0), new(-1,1)),
            //rechytsboven
            (new(0,0), new (0,0), new(0,0)),
            (new(-1,-1), new (0,0), new(0,0)),
            (new(-2,-2), new (0,0), new(-1,-1)),
        };

        foreach (var t in tests)
        {
            scenario.Theory("Test coord", t, () =>
            {
                sut.FollowHead(t.Head, t.Tail).Should().Be(t.Expected);
            });
        }
    }
}