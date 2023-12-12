using AdventOfCode.Solvers.Year2023.Day11;

namespace AdventOfCode.Solvers.Tests.Year2023.Day11;

public class Day11SolverTest
{
    private readonly ITestOutputHelper _output;

    private string example = """
    ...#......
    .......#..
    #.........
    ..........
    ......#...
    .#........
    .........#
    ..........
    .......#..
    #...#.....
    """;

    public Day11SolverTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact(DisplayName ="2023 Day 11 Solver Has Correct Solution For Part 1 sample input")]
    public async Task Day11SolverHasCorrectSolutionForPart1SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day11Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer1.LastAsync();

        answer.Should().Be("374");
    }

    [Fact(DisplayName ="2023 Day 11 Solver Has Correct Solution For Part 2 sample input")]
    public async Task Day11SolverHasCorrectSolutionForPart2SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day11Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer2.LastAsync();

        answer.Should().Be("82000210");
    }

    [Fact]
    public void testPointAdd()
    {
        var p1 = new Point(0,4);
        var p2 = new Point(2,0);

        var p3 = p1.AddPoint(p2);

        p3.Should().Be(6);
    }
}
