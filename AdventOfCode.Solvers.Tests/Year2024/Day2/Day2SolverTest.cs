using AdventOfCode.Solvers.Year2024.Day2;

namespace AdventOfCode.Solvers.Tests.Year2024.Day2;

public class Day2SolverTest
{
    private readonly ITestOutputHelper _output;

    private string example = """
    7 6 4 2 1
    1 2 7 8 9
    9 7 6 2 1
    1 3 2 4 5
    8 6 4 4 1
    1 3 6 7 9
    """;

    public Day2SolverTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact(DisplayName = "2024 Day 2 Solver Has Correct Solution For Part 1 sample input")]
    public async Task Day2SolverHasCorrectSolutionForPart1SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day2Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer1.LastAsync();

        answer.Should().Be("2");
    }

    [Fact(DisplayName = "2024 Day 2 Solver Has Correct Solution For Part 2 sample input")]
    public async Task Day2SolverHasCorrectSolutionForPart2SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day2Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer2.LastAsync();

        answer.Should().Be("4");
    }
}
