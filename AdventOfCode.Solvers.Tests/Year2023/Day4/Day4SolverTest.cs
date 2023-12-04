using System.Reactive.Linq;
using AdventOfCode.Solvers.Year2023.Day4;
using FluentAssertions;
using Xunit.Abstractions;

namespace AdventOfCode.Solvers.Tests.Year2023.Day4;

public class Day4SolverTest
{
    private readonly ITestOutputHelper _output;

    private string example = """

    """;

    public Day4SolverTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact(DisplayName ="2023 Day 4 Solver Has Correct Solution For Part 1 sample input")]
    public async Task Day3SolverHasCorrectSolutionForPart1SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day4Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer1.LastAsync();

        answer.Should().Be("");
    }

    [Fact(DisplayName ="2023 Day 4 Solver Has Correct Solution For Part 2 sample input")]
    public async Task Day3SolverHasCorrectSolutionForPart2SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day4Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer2.LastAsync();

        answer.Should().Be("");
    }
}
