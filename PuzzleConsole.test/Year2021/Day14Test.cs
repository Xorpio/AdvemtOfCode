using PuzzleConsole.Year2021;
using Xunit;
using ScenarioTests;

namespace PuzzleConsole.test.Year2021;

public partial class Day14Test
{
    [Scenario]
    public void SolveTest(ScenarioContext scenario)
    {
        var sut = new Day14();

        var puzzle = new string[] {
            "NNCB",
            "",
            "CH -> B",
            "HH -> N",
            "CB -> H",
            "NH -> C",
            "HB -> C",
            "HC -> B",
            "HN -> C",
            "NN -> C",
            "BH -> H",
            "NC -> B",
            "NB -> B",
            "BN -> B",
            "BB -> N",
            "BC -> B",
            "CC -> N",
            "CN -> C",
        };

        scenario.Fact("FinalSoltion", () =>
        {
            var sol = sut.Solve(puzzle);

            sol.First().Should().Be("1588");
            sol.Last().Should().Be("2188189693529");
        });
    }
}
