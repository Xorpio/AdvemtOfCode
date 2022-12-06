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

        var puzzles = new (string Puzzle, int Answer)[]
        {
            ("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 7),
            ("bvwbjplbgvbhsrlpgdmjqwftvncz", 5),
            ("nppdvjthqldpwncqszvftbrmjlhg", 6),
            ("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 10),
            ("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 11),
        };

        foreach (var p in puzzles)
        {
            scenario.Theory("Puzzle should be solved", p, () =>
            {
                sut.Solve(new[] { p.Puzzle })[0].Should().Be(p.Answer.ToString());
            });
        }
    }
}