using AdventOfCode.Solvers.Year2020.Day2;

namespace AdventOfCode.Solvers.Tests.Year2020.Day2;

public class Day2SolverTest
{
    private readonly ITestOutputHelper _output;

    private string example = """
    1-3 a: abcde
    1-3 b: cdefg
    2-9 c: ccccccccc
    """;

    public Day2SolverTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact(DisplayName ="2023 Day 2 Solver Has Correct Solution For Part 1 sample input")]
    public async Task Day3SolverHasCorrectSolutionForPart1SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day2Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer1.LastAsync();

        answer.Should().Be("2");
    }

    [Fact(DisplayName ="2023 Day 2 Solver Has Correct Solution For Part 2 sample input")]
    public async Task Day3SolverHasCorrectSolutionForPart2SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day2Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer2.LastAsync();

        answer.Should().Be("1");
    }}
