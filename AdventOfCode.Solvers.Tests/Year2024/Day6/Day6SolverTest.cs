using AdventOfCode.Solvers.Year2024.Day6;

namespace AdventOfCode.Solvers.Tests.Year2024.Day6;

public class Day6SolverTest
{
    private readonly ITestOutputHelper _output;

    private string example = """
    ....#.....
    .........#
    ..........
    ..#.......
    .......#..
    ..........
    .#..^.....
    ........#.
    #.........
    ......#...
    """;

    public Day6SolverTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact(DisplayName = "2024 Day 6 Solver Has Correct Solution For Part 1 sample input")]
    public async Task Day6SolverHasCorrectSolutionForPart1SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day6Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer1.LastAsync();

        answer.Should().Be("41");
    }

    [Fact(DisplayName = "2024 Day 6 Solver Has Correct Solution For Part 2 sample input")]
    public async Task Day6SolverHasCorrectSolutionForPart2SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day6Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer2.LastAsync();

        answer.Should().Be("6");
    }
}
