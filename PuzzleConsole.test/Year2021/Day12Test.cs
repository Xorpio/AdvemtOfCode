using PuzzleConsole.Year2021;
using Xunit;
using ScenarioTests;

namespace PuzzleConsole.test.Year2021;

public partial class Day12Test
{
    [Scenario]
    public void SolveTest(ScenarioContext scenario)
    {
        var sut = new Day12();
        sut.Debug = true;

        scenario.Fact("Simplest Puzle", () =>
        {
            var puzzle = new string[] {
                "start-end",
            };
            var expectation = new string[] { "start,end" };

            var sol = sut.Solve(puzzle);

            sol.Should().BeEquivalentTo(expectation);
        });

        scenario.Fact("Simplest 2 Puzle", () =>
        {
            var puzzle = new string[] {
                "start-A",
                "A-end"
            };
            var expectation = new string[] { "start,A,end" };

            var sol = sut.Solve(puzzle);

            sol.Should().BeEquivalentTo(expectation);
        });

        scenario.Fact("Simplest 3 Puzle", () =>
        {
            var puzzle = new string[] {
                "start-a",
                "start-b",
                "b-end",
                "a-end"
            };
            var expectation = new string[] { "start,a,end", "start,b,end" };

            var sol = sut.Solve(puzzle);

            sol.Should().BeEquivalentTo(expectation);
        });

        scenario.Fact("SimplePuzle", () =>
        {
            var puzzle = new string[] {
                "start-A",
                "start-b",
                "A-c",
                "A-b",
                "b-d",
                "A-end",
                "b-end"
            };

            var sol = sut.Solve(puzzle);

            var expectation = new string[] {
                "start,A,b,A,c,A,end",
                "start,A,b,A,end",
                "start,A,b,end",
                "start,A,c,A,b,A,end",
                "start,A,c,A,b,end",
                "start,A,c,A,end",
                "start,A,end",
                "start,b,A,c,A,end",
                "start,b,A,end",
                "start,b,end",
            };
            sol.Should().BeEquivalentTo(expectation);
        });

        scenario.Fact("larger", () =>
        {
            var puzzle = new string[] {
                "dc-end",
"HN-start",
"start-kj",
"dc-start",
"dc-HN",
"LN-dc",
"HN-end",
"kj-sa",
"kj-HN",
"kj-dc",

            };

            var sol = sut.Solve(puzzle);

            var expectation = new string[] {
                "start,HN,dc,HN,end",
"start,HN,dc,HN,kj,HN,end",
"start,HN,dc,end",
"start,HN,dc,kj,HN,end",
"start,HN,end",
"start,HN,kj,HN,dc,HN,end",
"start,HN,kj,HN,dc,end",
"start,HN,kj,HN,end",
"start,HN,kj,dc,HN,end",
"start,HN,kj,dc,end",
"start,dc,HN,end",
"start,dc,HN,kj,HN,end",
"start,dc,end",
"start,dc,kj,HN,end",
"start,kj,HN,dc,HN,end",
"start,kj,HN,dc,end",
"start,kj,HN,end",
"start,kj,dc,HN,end",
"start,kj,dc,end",

            };
            sol.Should().BeEquivalentTo(expectation);
        });

        scenario.Fact("biggest", () =>
        {
            sut.Debug = false;
            var puzzle = new string[] {
                "fs-end",
                "he-DX",
                "fs-he",
                "start-DX",
                "pj-DX",
                "end-zg",
                "zg-sl",
                "zg-pj",
                "pj-he",
                "RW-he",
                "fs-DX",
                "pj-RW",
                "zg-RW",
                "start-pj",
                "he-WI",
                "zg-he",
                "pj-fs",
                "start-RW",

            };

            var sol = sut.Solve(puzzle);

            sol.First().Should().Be("226");
        });
    }
}
