using PuzzleConsole.Year2015.Day5;

namespace PuzzleConsole.test.Year2015;

[Trait("Year", "2015")]
[Trait("Day", "5")]
public partial class Day5Test2015 {
    [Scenario]
    public void SolveTest(ScenarioContext scenario)
    {
        var sut = new Day5();

        scenario.Fact("Sut should not be null", () =>
        {
            sut.Should().NotBeNull();
        });

        var tests = new (string inp, bool good)[]
        {
            ("ugknbfddgicrmopn", true),
            ("aaa", true),
            ("jchzalrnumimnmhp", false),
            ("haegwjzuvuyypxyu", false),
            ("dvszwmarrgswjxmb", false),
        };

        foreach (var t in tests)
        {
            scenario.Theory("Test moet slagen", t, () =>
            {
                sut.SolveLine(t.inp).Should().Be(t.good);
            });
        }

        var puzzle = new string[]
        {
            "ugknbfddgicrmopn",
            "aaa",
            "jchzalrnumimnmhp",
            "haegwjzuvuyypxyu",
            "dvszwmarrgswjxmb",
        };

        scenario.Fact("Totale test", () =>
        {
            sut.Solve(puzzle)[0].Should().Be("2");
        });

        var tests2 = new (string inp, bool good)[]
        {
            ("qjhvhtzxzqqjkmpb", true),
            ("xxyxx", true),
            ("uurcxstgmygtbstg", false),
            ("ieodomkazucvgmuy", false),
            ("aaabtb", false),
            ("aaabtbt", true),
            ("btbtaaa", true),
            ("abaoooo", true),
            ("ooooaba", true),
            ("abab", true),
        };

        foreach (var t in tests2)
        {
            scenario.Theory("Test moet slagen part 2", t, () =>
            {
                sut.SolveLinePart2(t.inp).Should().Be(t.good);
            });
        }

    }
}