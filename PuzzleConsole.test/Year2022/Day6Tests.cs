using PuzzleConsole.Year2022.Day6;

namespace PuzzleConsole.test.Year2022;

[Trait("Year", "2022")]
[Trait("Day", "6")]
public partial class Day6Tests
{
    [Scenario]
    public void SolveTests(ScenarioContext scenario)
    {
        var sut = new Day6();

        scenario.Fact("Sut should not be null", () =>
        {
            sut.Should().NotBeNull();
        });

        var puzzles = new (string Puzzle, int AnswerPart1, int AnswerPart2)[]
        {
            ("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 7, 19),
            ("bvwbjplbgvbhsrlpgdmjqwftvncz", 5, 23),
            ("nppdvjthqldpwncqszvftbrmjlhg", 6,23 ),
            ("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 10, 29),
            ("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 11,26),
        };

        foreach (var p in puzzles)
        {
            scenario.Theory("Puzzle part 1 should be solved", p, () =>
            {
                sut.Solve(new[] { p.Puzzle })[0].Should().Be(p.AnswerPart1.ToString());
            });
            scenario.Theory("Puzzle part 2 should be solved", p, () =>
            {
                sut.Solve(new[] { p.Puzzle })[1].Should().Be(p.AnswerPart2.ToString());
            });
        }
    }
}