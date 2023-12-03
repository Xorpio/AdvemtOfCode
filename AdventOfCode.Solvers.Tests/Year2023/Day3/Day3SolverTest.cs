using System.Reactive.Linq;
using AdventOfCode.Solvers.Year2023.Day3;
using FluentAssertions;
using Xunit.Abstractions;

namespace AdventOfCode.Solvers.Tests.Year2023.Day3;

public class Day3SolverTest
{
    private readonly ITestOutputHelper _output;

    public Day3SolverTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact(DisplayName ="2023 Day 3 Solver Has Correct Solution For Part 1 sample input")]
    public async Task Day3SolverHasCorrectSolutionForPart1SampleInputAsync()
    {
        var puzzle = """
        467..114..
        ...*......
        ..35..633.
        ......#...
        617*......
        .....+.58.
        ..592.....
        ......755.
        ...$.*....
        .664.598..
        """;

        var lines = puzzle.Split(Environment.NewLine);

        var solver = new Day3Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer1.LastAsync();

        answer.Should().Be("4361");
    }
}
