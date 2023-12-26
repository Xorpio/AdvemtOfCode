using AdventOfCode.Solvers.Year2023.Day17;

namespace AdventOfCode.Solvers.Tests.Year2023.Day17;

public class Day17SolverTest
{
    private readonly ITestOutputHelper _output;

    private string example = """
    2413432311323
    3215453535623
    3255245654254
    3446585845452
    4546657867536
    1438598798454
    4457876987766
    3637877979653
    4654967986887
    4564679986453
    1224686865563
    2546548887735
    4322674655533
    """;

    public Day17SolverTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact(DisplayName ="2023 Day 17 Solver Has Correct Solution For Part 1 sample input")]
    public async Task Day17SolverHasCorrectSolutionForPart1SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day17Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer1.LastAsync();

        answer.Should().Be("102");
    }

    [Fact(DisplayName ="2023 Day 17 Solver Has Correct Solution For Part 2 sample input")]
    public async Task Day17SolverHasCorrectSolutionForPart2SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day17Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer2.LastAsync();

        answer.Should().Be("94");
    }
}
