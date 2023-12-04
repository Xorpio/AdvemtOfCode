using AdventOfCode.Solvers.Year2020.Day3;

namespace AdventOfCode.Solvers.Tests.Year2020.Day3;

public class Day3SolverTest
{
    private readonly ITestOutputHelper _output;

    private string example = """
    ..##.......
    #...#...#..
    .#....#..#.
    ..#.#...#.#
    .#...##..#.
    ..#.##.....
    .#.#.#....#
    .#........#
    #.##...#...
    #...##....#
    .#..#...#.#
    """;

    public Day3SolverTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact(DisplayName ="2020 Day 3 Solver Has Correct Solution For Part 1 sample input")]
    public async Task Day3SolverHasCorrectSolutionForPart1SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day3Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer1.LastAsync();

        answer.Should().Be("7");
    }

    [Fact(DisplayName ="2020 Day 3 Solver Has Correct Solution For Part 2 sample input")]
    public async Task Day3SolverHasCorrectSolutionForPart2SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day3Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer2.LastAsync();

        answer.Should().Be("336");
    }}
