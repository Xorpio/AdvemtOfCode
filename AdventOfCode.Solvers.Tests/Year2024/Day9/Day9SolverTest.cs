using AdventOfCode.Solvers.Year2024.Day9;

namespace AdventOfCode.Solvers.Tests.Year2024.Day9;

public class Day9SolverTest
{
    private readonly ITestOutputHelper _output;

    private string example = """
    2333133121414131402
    """;

    public Day9SolverTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact(DisplayName = "2024 Day 9 Solver Has Correct Solution For Part 1 sample input")]
    public async Task Day9SolverHasCorrectSolutionForPart1SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day9Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer1.LastAsync();

        answer.Should().Be("1928");
    }

    [Fact(DisplayName = "2024 Day 9 Solver Has Correct Solution For Part 2 sample input")]
    public async Task Day9SolverHasCorrectSolutionForPart2SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day9Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer2.LastAsync();

        answer.Should().Be("2858");
    }
}
