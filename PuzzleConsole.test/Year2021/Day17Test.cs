using PuzzleConsole.Year2021.Day17;

namespace PuzzleConsole.test.Year2021;

public partial class Day17Test
{
    [Scenario]
    public void SolveTest(ScenarioContext scenario)
    {
        var sut = new Day17();

        scenario.Fact("Sut should be puzzle", () =>
        {
            sut.Should().NotBeNull();
        });

        scenario.Fact("Resolve minmax", () =>
        {
            var input = "target area: x=20..30, y=-10..-5";

            (int XMin, int XMax, int YMin, int YMax) expected = new(20, 30, -5, -10);

            var solution = sut.GetMinMax(input);
            solution.Should().Be(expected);
        });

        var testCases = new (int x, int y, bool hit)[]
        {
            new (7, 2, true),
            new (6, 3, true),
            new (6, 9, true),
            new (9, 0, true),
            new (17, -4, false),
        };

        foreach (var testCase in testCases)
        {

            scenario.Theory("CalcFire", testCase, () =>
            {
                (int XMin, int XMax, int YMin, int YMax) target = new(20, 30, -5, -10);

                var solution = sut.Fire(testCase.x, testCase.y, target);
                solution.Should().Be(testCase.hit);
            });
        }

        scenario.Fact("Get max Y", () =>
        {
            (int XMin, int XMax, int YMin, int YMax) target = new(20, 30, -5, -10);
            
            var expected = 45;

            var solution = sut.FindMaxYVelocity(target);

            solution.maxY.Should().Be(expected);
        });

        var input = "target area: x=20..30, y=-10..-5";
        var solution = sut.Solve(new string [] {input });

        scenario.Fact("Sovle part 1", () =>
        {

            var expected = "45";

            solution.First().Should().Be(expected);
        });

        scenario.Fact("Sovle part 2", () =>
        {
            var expected = "112";

            solution.Last().Should().Be(expected);
        });
    }
}
