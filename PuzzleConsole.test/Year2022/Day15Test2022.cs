using PuzzleConsole.Year2022.Day15;

namespace PuzzleConsole.test.Year2022;

[Trait("Year", "2022")]
[Trait("Day", "15")]
public partial class Day15Test2022 {
    [Scenario]
    public void SolveTest(ScenarioContext scenario)
    {
        var sut = new Day15();

        var sampleInput = """
        Sensor at x=2, y=18: closest beacon is at x=-2, y=15
        Sensor at x=9, y=16: closest beacon is at x=10, y=16
        Sensor at x=13, y=2: closest beacon is at x=15, y=3
        Sensor at x=12, y=14: closest beacon is at x=10, y=16
        Sensor at x=10, y=20: closest beacon is at x=10, y=16
        Sensor at x=14, y=17: closest beacon is at x=10, y=16
        Sensor at x=8, y=7: closest beacon is at x=2, y=10
        Sensor at x=2, y=0: closest beacon is at x=2, y=10
        Sensor at x=0, y=11: closest beacon is at x=2, y=10
        Sensor at x=20, y=14: closest beacon is at x=25, y=17
        Sensor at x=17, y=20: closest beacon is at x=21, y=22
        Sensor at x=16, y=7: closest beacon is at x=15, y=3
        Sensor at x=14, y=3: closest beacon is at x=15, y=3
        Sensor at x=20, y=1: closest beacon is at x=15, y=3
        """;
        var lines = sampleInput.Split("\n").Select(s => s.Trim()).ToArray();

        scenario.Fact("Sut should not be null", () =>
        {
            sut.Should().NotBeNull();
        });

        scenario.Fact("Small sample test part 1", () =>
        {
            int.Parse(sut.Solve(lines)[0]).Should().Be(26);
        });

        scenario.Fact("Small sample test part 2", () =>
        {
            sut.Solve(lines)[1].Should().Be("56000011");
        });

        var tests = new (Coord a, Coord b, int distance)[]
        {
            (new(0, 0), new(0, 3), 3),
            (new(0, 0), new(3, 0), 3),
            (new(0, 0), new(0, -3), 3),
            (new(0, 0), new(-3, 0), 3),
            (new(0, 0), new(1, 2), 3),
            (new(0, 0), new(2,1), 3),
            (new(0, 0), new(-1, -2), 3),
            (new(0, 0), new(-2,-1), 3),
        };

        foreach (var t in tests)
        {
            scenario.Theory("Coords test", t, () =>
            {
                (t.a + t.b).Should().Be(t.distance);
            });
        }
    }
}