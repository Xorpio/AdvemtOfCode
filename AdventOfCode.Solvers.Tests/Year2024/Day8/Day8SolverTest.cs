using AdventOfCode.Solvers.Year2024.Day8;

namespace AdventOfCode.Solvers.Tests.Year2024.Day8;

public class Day8SolverTest
{
    private readonly ITestOutputHelper _output;

    private string example = """
    ............
    ........0...
    .....0......
    .......0....
    ....0.......
    ......A.....
    ............
    ............
    ........A...
    .........A..
    ............
    ............
    """;

    public Day8SolverTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact(DisplayName = "2024 Day 8 Solver Has Correct Solution For Part 1 sample input")]
    public async Task Day8SolverHasCorrectSolutionForPart1SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day8Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer1.LastAsync();

        answer.Should().Be("14");
    }

    [Fact(DisplayName = "2024 Day 8 Solver Has Correct Solution For Part 2 sample input")]
    public async Task Day8SolverHasCorrectSolutionForPart2SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day8Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer2.LastAsync();

        answer.Should().Be("?");
    }
}
