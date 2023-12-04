using AdventOfCode.Solvers.Year2020.Day1;

namespace AdventOfCode.Solvers.Tests.Year2020.Day1;

public class Day1SolverTest
{
    
    private readonly ITestOutputHelper _output;

    private string example = """
    1456
    979
    1721
    366
    299
    675
    """;

    public Day1SolverTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact(DisplayName ="2023 Day 1 Solver Has Correct Solution For Part 1 sample input")]
    public async Task Day3SolverHasCorrectSolutionForPart1SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day1Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer1.LastAsync();

        answer.Should().Be("514579");
    }

    [Fact(DisplayName ="2023 Day 1 Solver Has Correct Solution For Part 2 sample input")]
    public async Task Day3SolverHasCorrectSolutionForPart2SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day1Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer2.LastAsync();

        answer.Should().Be("241861950");
    }}
