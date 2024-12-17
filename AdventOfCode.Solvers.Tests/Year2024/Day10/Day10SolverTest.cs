﻿using AdventOfCode.Solvers.Year2024.Day10;

namespace AdventOfCode.Solvers.Tests.Year2024.Day10;

public class Day10SolverTest
{
    private readonly ITestOutputHelper _output;

    private string example = """
    sampleinput
    """;

    public Day10SolverTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact(DisplayName = "2024 Day 10 Solver Has Correct Solution For Part 1 sample input")]
    public async Task Day10SolverHasCorrectSolutionForPart1SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day10Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer1.LastAsync();

        answer.Should().Be("?");
    }

    [Fact(DisplayName = "2024 Day 10 Solver Has Correct Solution For Part 2 sample input")]
    public async Task Day10SolverHasCorrectSolutionForPart2SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day10Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer2.LastAsync();

        answer.Should().Be("?");
    }
}