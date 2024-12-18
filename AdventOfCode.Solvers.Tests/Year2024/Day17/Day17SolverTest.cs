using AdventOfCode.Solvers.Year2024.Day17;

namespace AdventOfCode.Solvers.Tests.Year2024.Day17;

public class Day17SolverTest
{
    private readonly ITestOutputHelper _output;

    private string example = """
    Register A: 729
    Register B: 0
    Register C: 0

    Program: 0,1,5,4,3,0
    """;

    public Day17SolverTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact(DisplayName = "2024 Day 17 Solver Has Correct Solution For Part 1 sample input")]
    public async Task Day17SolverHasCorrectSolutionForPart1SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day17Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer1.LastAsync();

        answer.Should().Be("4,6,3,5,6,3,5,2,1,0");
    }

    [Fact(DisplayName = "2024 Day 17 Solver Has Correct Solution For Part 2 sample input")]
    public async Task Day17SolverHasCorrectSolutionForPart2SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day17Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer2.LastAsync();

        answer.Should().Be("?");
    }
}
