using Moq;
using PuzzleConsole.Year2022.Day1;

namespace PuzzleConsole.test.Year2022;

public partial class Day1Test
{
    [Scenario]
    public void Test(ScenarioContext scenario)
    {
        var sampleInput = """
1000
2000
3000

4000

5000
6000

7000
8000
9000

10000
""";

        var mocker = new AutoMocker(MockBehavior.Strict);

        var solver = mocker.CreateInstance<Day1>();

        var lines = sampleInput.Split("\n");

        scenario.Fact("Lines moeten gesplit zijn", () =>
        {
            lines.Should().HaveCount(14);
        });

        scenario.Fact("Sovler moet puzzle oplossen", () =>
        {
            var solution = solver.Solve(lines);

            solution[0].Should().Be("24000");
        });

        scenario.Fact("Sovler moet puzzle  deel 2 oplossen", () =>
        {
            var solution = solver.Solve(lines);

            solution[1].Should().Be("45000");
        });
    }
}