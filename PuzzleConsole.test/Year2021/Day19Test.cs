using PuzzleConsole.Year2021.Day19;
using Xunit;

namespace PuzzleConsole.test.Year2021;

[Trait("Year", "2021")]
[Trait("Day", "19")]
public partial class Day19Test
{
    [Scenario]
    public void SolveTest(ScenarioContext scenario)
    {
        var sut = new Day19();

        scenario.Fact("Sut should not be null", () =>
        {

            var puzzle = new string[]
            {
                "--- scanner 0 ---",
                "-1,-1,1",
                "-2,-2,2",
                "-3,-3,3",
                "-2,-3,1",
                "5,6,-4",
                "8,0,7",

                "--- scanner 0 ---",
                "1,-1,1",
                "2,-2,2",
                "3,-3,3",
                "2,-1,3",
                "-5,4,-6",
                "-8,-7,0",

                "--- scanner 0 ---",
                "-1,-1,-1",
                "-2,-2,-2",
                "-3,-3,-3",
                "-1,-3,-2",
                "4,6,5",
                "-7,0,8",

                "--- scanner 0 ---",
                "1,1,-1",
                "2,2,-2",
                "3,3,-3",
                "1,3,-2",
                "-4,-6,5",
                "7,0,8",

                "--- scanner 0 ---",
                "1,1,1",
                "2,2,2",
                "3,3,3",
                "3,1,2",
                "-6,-4,-5",
                "0,7,-8",

            };
            sut.Should().NotBeNull();
        });
    }
}
