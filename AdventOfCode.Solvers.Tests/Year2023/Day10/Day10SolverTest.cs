using AdventOfCode.Solvers.Year2023.Day10;

namespace AdventOfCode.Solvers.Tests.Year2023.Day10;

public class Day10SolverTest
{
    private readonly ITestOutputHelper _output;

    private string example = """
    -L|F7
    7S-7|
    L|7||
    -L-J|
    L|-JF
    """;
    private string example2 = """
    ..F7.
    .FJ|.
    SJ.L7
    |F--J
    LJ...
    """;

    public Day10SolverTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact(DisplayName ="2023 Day 10 Solver Has Correct Solution For Part 1 sample input")]
    public async Task Day10SolverHasCorrectSolutionForPart1SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day10Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer1.LastAsync();

        answer.Should().Be("4");
    }

    [Fact(DisplayName ="2023 Day 10 Solver Has Correct Solution For Part 2 sample input")]
    public async Task Day10SolverHasCorrectSolutionForPart2SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day10Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer2.LastAsync();

        answer.Should().Be("1");
    }

    [Fact(DisplayName ="2023 Day 10 Solver Has Correct Solution For Part 1 sample 2 input")]
    public async Task Day10SolverHasCorrectSolutionForPart1SampleInput2Async()
    {
        var lines = example2.Split(Environment.NewLine);

        var solver = new Day10Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer1.LastAsync();

        answer.Should().Be("8");
    }

    [Fact(DisplayName ="2023 Day 10 Solver Has Correct Solution For Part 2 large  sample input")]
    public async Task Day10SolverHasCorrectSolutionForPart2largeSampleInputAsync()
    {
        var largeExample = """
        ...........
        .S-------7.
        .|F-----7|.
        .||.....||.
        .||.....||.
        .|L-7.F-J|.
        .|..|.|..|.
        .L--J.L--J.
        ...........
        """;
        var lines = largeExample.Split(Environment.NewLine);

        var solver = new Day10Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer2.LastAsync();

        answer.Should().Be("4");
    }

    [Fact(DisplayName ="2023 Day 10 Solver Has Correct Solution For Part 2 other  sample input")]
    public async Task Day10SolverHasCorrectSolutionForPart2largeSampleInputAsync2()
    {
        var largeExample = """
        FF7FSF7F7F7F7F7F---7
        L|LJ||||||||||||F--J
        FL-7LJLJ||||||LJL-77
        F--JF--7||LJLJ7F7FJ-
        L---JF-JLJ.||-FJLJJ7
        |F|F-JF---7F7-L7L|7|
        |FFJF7L7F-JF7|JL---7
        7-L-JL7||F7|L7F-7F7|
        L.L7LFJ|||||FJL7||LJ
        L7JLJL-JLJLJL--JLJ.L
        """;
        var lines = largeExample.Split(Environment.NewLine);

        var solver = new Day10Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer2.LastAsync();

        answer.Should().Be("10");
    }
}
