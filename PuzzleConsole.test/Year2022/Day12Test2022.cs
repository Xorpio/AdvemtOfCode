﻿using PuzzleConsole.Year2022.Day12;

namespace PuzzleConsole.test.Year2022;

[Trait("Year", "2022")]
[Trait("Day", "12")]
public partial class Day12Test2022
{
    [Scenario]
    public void SolveTest(ScenarioContext scenario)
    {
        var sut = new Day12();

        var sampleInput = """
        Sabqponm
        abcryxxl
        accszExk
        acctuvwj
        abdefghi
        """;

        var lines = sampleInput.Split("\n").Select(s => s.Trim()).ToArray();

        scenario.Fact("Sut should not be null", () =>
        {
            sut.Should().NotBeNull();
        });

        scenario.Fact("Small sample test", () =>
        {
            int.Parse(sut.Solve(lines)[0]).Should().Be(31);
        });

        scenario.Fact("Small test", () =>
        {
            var a = new Coords(1, 2);
            var b = new Coords(0, 1);
            var e = new Coords(10, 1);

            (a + e).Should().Be(9);
            (b + e).Should().Be(10);
        });

    }
}