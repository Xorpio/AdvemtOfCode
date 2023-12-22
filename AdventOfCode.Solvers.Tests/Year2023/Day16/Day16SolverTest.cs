using System.Xml.Xsl;
using AdventOfCode.Solvers.Year2023.Day16;

namespace AdventOfCode.Solvers.Tests.Year2023.Day16;

public class Day16SolverTest
{
    private readonly ITestOutputHelper _output;

    private string example = """
    .|...\....
    |.-.\.....
    .....|-...
    ........|.
    ..........
    .........\
    ..../.\\..
    .-.-/..|..
    .|....-|.\
    ..//.|....
    """;

    public Day16SolverTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact(DisplayName ="2023 Day 16 Solver Has Correct Solution For Part 1 sample input")]
    public async Task Day16SolverHasCorrectSolutionForPart1SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day16Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer1.LastAsync();

        answer.Should().Be("46");
    }

    [Fact(DisplayName ="2023 Day 16 Solver Has Correct Solution For Part 2 sample input")]
    public async Task Day16SolverHasCorrectSolutionForPart2SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day16Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer2.LastAsync();

        answer.Should().Be("51");
    }
}
