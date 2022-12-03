using PuzzleConsole.Year2022.Day3;

namespace PuzzleConsole.test.Year2022;

public partial class Day3Tests
{
    [Scenario]
    public void SolveTests(ScenarioContext scenario)
    {
        var sut = new Day3();

        scenario.Fact("Sut should not be null", () =>
        {
            sut.Should().NotBeNull();
        });

        var puzzles = new (string p, int s1, int s2)[]
        {
            ("aa", 1, 0),
            ("zz", 26, 0),
            ("AA", 27, 0),
            ("ZZ", 52, 0),
            ("vJrwpWtwJgWrhcsFMMfFFhFp", 16, 0),
            ("jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL", 38, 0),
            ("PmmdzqPrVvPwwTWBwg", 42, 0),
            ("wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn", 22, 0),
            ("ttgJtRGJQctTZtZT", 20, 0),
            ("CrZsJsPPZsGzwwsLwLmpwMDw", 19, 0),
            ("""
            vJrwpWtwJgWrhcsFMMfFFhFp
            jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
            PmmdzqPrVvPwwTWBwg
            wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
            ttgJtRGJQctTZtZT
            CrZsJsPPZsGzwwsLwLmpwMDw
            """
                , 157, 70),
        };

        foreach (var p in puzzles)
        {
            scenario.Theory("Puzzle solve", p, () =>
            {
                var puzzleLines = p.p.Split("\r\n");

                sut.Solve(puzzleLines)[0].Should().Be(p.s1.ToString());
                sut.Solve(puzzleLines)[1].Should().Be(p.s2.ToString());
            });
        }
    }
}