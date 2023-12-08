using AdventOfCode.Solvers.Year2021.Day25;

namespace AdventOfCode.Solvers.Tests.Year2021.Day25;

public class Day25SolverTest
{
    private readonly ITestOutputHelper _output;

    private string example = """
    v...>>.vv>
    .vv>>.vv..
    >>.>v>...v
    >>v>>.>.v.
    v>v.vv.v..
    >.>>..v...
    .vv..>.>v.
    v.v..>>v.v
    ....v..v.>
    """;

    public Day25SolverTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact(DisplayName ="2021 Day 25 Solver Has Correct Solution For Part 1 sample input")]
    public async Task Day25SolverHasCorrectSolutionForPart1SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day25Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer1.LastAsync();

        answer.Should().Be("58");
    }

    [Fact(DisplayName ="2021 Day 25 Solver Has Correct Solution For Part 2 sample input")]
    public async Task Day25SolverHasCorrectSolutionForPart2SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day25Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer2.LastAsync();

        answer.Should().Be("?");
    }}
