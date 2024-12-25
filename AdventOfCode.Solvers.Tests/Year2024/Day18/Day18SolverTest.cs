using AdventOfCode.Solvers.Year2024.Day18;

namespace AdventOfCode.Solvers.Tests.Year2024.Day18;

public class Day18SolverTest
{
    private readonly ITestOutputHelper _output;

    private string example = """
    5,4
    4,2
    4,5
    3,0
    2,1
    6,3
    2,4
    1,5
    0,6
    3,3
    2,6
    5,1
    1,2
    5,5
    2,5
    6,5
    1,4
    0,4
    6,4
    1,1
    6,1
    1,0
    0,5
    1,6
    2,0
    """;

    public Day18SolverTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact(DisplayName = "2024 Day 18 Solver Has Correct Solution For Part 1 sample input")]
    public async Task Day18SolverHasCorrectSolutionForPart1SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day18Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer1.LastAsync();

        answer.Should().Be("22");
    }

    [Fact(DisplayName = "2024 Day 18 Solver Has Correct Solution For Part 2 sample input")]
    public async Task Day18SolverHasCorrectSolutionForPart2SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day18Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer2.LastAsync();

        answer.Should().Be("6,1");
    }
}
