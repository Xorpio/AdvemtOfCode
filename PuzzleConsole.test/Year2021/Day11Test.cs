using PuzzleConsole.Year2021;
using Xunit;
using ScenarioTests;

namespace PuzzleConsole.test.Year2021;

public partial class Day11Test
{
    [Scenario]
    public void SolveTest(ScenarioContext scenario)
    {
        var sut = new Day11();

        sut.Part2 = false;

        var puzzle = new string[] {
            "5483143223",
            "2745854711",
            "5264556173",
            "6141336146",
            "6357385478",
            "4167524645",
            "2176841721",
            "6882881134",
            "4846848554",
            "5283751526"
        };

        var solution = sut.Solve(puzzle);
        scenario.Fact("First step test", () => {
            sut.Steps = 1;
            var p = new string[] {
                "5483143223",
                "2745854711",
                "5264556173",
                "6141336146",
                "6357385478",
                "4167524645",
                "2176841721",
                "6882881134",
                "4846848554",
                "5283751526"
            };
            var e = new string[] {
                "0",
                "6594254334",
"3856965822",
"6375667284",
"7252447257",
"7468496589",
"5278635756",
"3287952832",
"7993992245",
"5957959665",
"6394862637"
            };
            var sol = sut.Solve(p);
            sol.Should().Equal(e);
        });

        scenario.Fact("second step", () => {
            sut.Steps = 1;
            var e = new string[] {
                "35",
                "8807476555",
"5089087054",
"8597889608",
"8485769600",
"8700908800",
"6600088989",
"6800005943",
"0000007456",
"9000000876",
"8700006848",

            };
            var p = new string[] {
                "6594254334",
"3856965822",
"6375667284",
"7252447257",
"7468496589",
"5278635756",
"3287952832",
"7993992245",
"5957959665",
"6394862637"
            };
            var sol = sut.Solve(p);
            sol.Should().Equal(e);
        });
        scenario.Fact("first 2 steps", () => {
            var e = new string[] {
                "35",
                "8807476555",
"5089087054",
"8597889608",
"8485769600",
"8700908800",
"6600088989",
"6800005943",
"0000007456",
"9000000876",
"8700006848",

            };
            sut.Steps = 2;
            var sol = sut.Solve(puzzle);
            sol.Should().Equal(e);
        });
        scenario.Fact("first 10 steps", () => {
            var e = new string[] {
                "204",
                "0481112976",
"0031112009",
"0041112504",
"0081111406",
"0099111306",
"0093511233",
"0442361130",
"5532252350",
"0532250600",
"0032240000",


            };
            sut.Steps = 10;
            var sol = sut.Solve(puzzle);
            sol.Should().Equal(e);
        });

        scenario.Fact("After 100 steps(solution)", () =>
        {
            var e = new string[]
            {
                "1656",
"0397666866",
"0749766918",
"0053976933",
"0004297822",
"0004229892",
"0053222877",
"0532222966",
"9322228966",
"7922286866",
"6789998766",

            };
            solution.Should().Equal(e);

        });

        scenario.Fact("Part2", () =>
        {
            sut.Part2 = true;
            solution = sut.Solve(puzzle);
            var e = new string[]
            {
                "1656",
                "195"
            };
            solution.Should().Equal(e);

        });

    }
}
