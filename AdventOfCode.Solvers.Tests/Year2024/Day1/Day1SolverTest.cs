using AdventOfCode.Solvers.Year2024.Day1;

namespace AdventOfCode.Solvers.Tests.Year2024.Day1;
public class Day1SolverTest
{
    [Fact(DisplayName = "TestAnswer1")]
    public async Task TestPart1Async()
    {
        var solver = new Day1Solver();

        var puzzle = new[]
        {
            "3   4",
            "4   3",
            "2   5",
            "1   3",
            "3   9",
            "3   3",
        };

        solver.Solve(puzzle);

        string answer = await solver.Answer1.LastAsync();

        answer.Should().Be("11");
    }

    [Fact(DisplayName = "TestAnswer2")]
    public async Task TestPart2Async()
    {
        var solver = new Day1Solver();

        var puzzle = new[]
        {
            "3   4",
            "4   3",
            "2   5",
            "1   3",
            "3   9",
            "3   3",
        };

        solver.Solve(puzzle);

        string answer = await solver.Answer2.LastAsync();

        answer.Should().Be("31");
    }
}
