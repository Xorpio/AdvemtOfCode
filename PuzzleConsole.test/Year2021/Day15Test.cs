﻿using PuzzleConsole.Year2021.Day15;

namespace PuzzleConsole.test.Year2021;

public partial class Day15Test
{
    [Scenario]
    public void SolveTest(ScenarioContext scenario)
    {
        var sut = new Day15();

        var puzzle = new string[]
        {
            "1163751742",
            "1381373672",
            "2136511328",
            "3694931569",
            "7463417111",
            "1319128137",
            "1359912421",
            "3125421639",
            "1293138521",
            "2311944581",

        };

        scenario.Fact("Test puzzle to be created", () =>
        {
            sut.Should().NotBeNull();
        });

    }
}