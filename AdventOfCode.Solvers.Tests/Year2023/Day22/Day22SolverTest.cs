using AdventOfCode.Solvers.Year2023.Day22;

namespace AdventOfCode.Solvers.Tests.Year2023.Day22;

public class Day22SolverTest
{
    private readonly ITestOutputHelper _output;

    private string example = """
    1,0,1~1,2,1
    0,0,2~2,0,2
    0,2,3~2,2,3
    0,0,4~0,2,4
    2,0,5~2,2,5
    0,1,6~2,1,6
    1,1,8~1,1,9
    """;

    public Day22SolverTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact(DisplayName ="2023 Day 22 Solver Has Correct Solution For Part 1 sample input")]
    public async Task Day22SolverHasCorrectSolutionForPart1SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day22Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer1.LastAsync();

        answer.Should().Be("5");
    }

    [Fact(DisplayName ="2023 Day 22 Solver Has Correct Solution For Part 2 sample input")]
    public async Task Day22SolverHasCorrectSolutionForPart2SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        int i = int.MaxValue;
        i = i + 1;

        var solver = new Day22Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer2.LastAsync();

        answer.Should().Be("7");
    }
}
