using PuzzleConsole.Year2021.Day10;
using Xunit;
using ScenarioTests;

namespace PuzzleConsole.test.Year2021;

[Trait("Year", "2021")]
[Trait("Day", "10")]
public partial class Day10Test
{
    [Scenario]
    public void SolveTest(ScenarioContext scenario)
    {
        var sut = new Day10();

        var puzzle = new string[] {
            "[({(<(())[]>[[{[]{<()<>>",
            "[(()[<>])]({[<{<<[]>>(",
            "{([(<{}[<>[]}>{[]{[(<()>",
            "(((({<>}<{<{<>}{[]{[]{}",
            "[[<[([]))<([[{}[[()]]]",
            "[{[{({}]{}}([{[{{{}}([]",
            "{<[[]]>}<{[{[{[]{()[[[]",
            "[<(<(<(<{}))><([]([]()",
            "<{([([[(<>()){}]>(<<{{",
            "<{([{{}}[<[[[<>{}]]]>[]]"
        };

        var solution = sut.Solve(puzzle);

        scenario.Fact("line should be detected as incorrupt", () => {
            var line = "[]";

            sut.isCorupt(line).res.Should().Be(false);
        });

         scenario.Fact("line should be detected as corrupt", () => {
            var line = "[}";

            sut.isCorupt(line).res.Should().Be(true);
        });

        scenario.Fact("First result should be 26397", () => {
            solution.First().Should().Be("26397");
        });

        scenario.Fact("second result should be 288957", () => {
            solution[1].Should().Be("288957");
        });
    }
}
