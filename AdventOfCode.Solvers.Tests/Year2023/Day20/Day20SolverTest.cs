using AdventOfCode.Solvers.Year2023.Day20;

namespace AdventOfCode.Solvers.Tests.Year2023.Day20;

public class Day20SolverTest
{
    private readonly ITestOutputHelper _output;

    private string example = """
    broadcaster -> a, b, c
    %a -> b
    %b -> c
    %c -> inv
    &inv -> a
    """;

    public Day20SolverTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact(DisplayName ="2023 Day 20 Solver Has Correct Solution For Part 1 sample input")]
    public async Task Day20SolverHasCorrectSolutionForPart1SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day20Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer1.LastAsync();

        answer.Should().Be("32000000");
    }

    [Fact(DisplayName ="2023 Day 20 Solver Has Correct Solution For Part 1 sample 2 input")]
    public async Task Day20SolverHasCorrectSolutionForPart1SampleInput2Async()
    {
        var example2 = """
        broadcaster -> a
        %a -> inv, con
        &inv -> b
        %b -> con
        &con -> output
        """;

        var lines = example2.Split(Environment.NewLine);

        var solver = new Day20Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer1.LastAsync();

        answer.Should().Be("11687500");
    }

    [Fact(DisplayName ="2023 Day 20 Solver Has Correct Solution For Part 2 sample input")]
    public async Task Day20SolverHasCorrectSolutionForPart2SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day20Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer2.LastAsync();

        answer.Should().Be("?");
    }}
