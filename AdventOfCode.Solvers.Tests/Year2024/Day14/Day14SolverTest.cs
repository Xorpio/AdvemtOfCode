using AdventOfCode.Solvers.Year2024.Day14;

namespace AdventOfCode.Solvers.Tests.Year2024.Day14;

public class Day14SolverTest
{
    private readonly ITestOutputHelper _output;

    private string example = """
    p=0,4 v=3,-3
    p=6,3 v=-1,-3
    p=10,3 v=-1,2
    p=2,0 v=2,-1
    p=0,0 v=1,3
    p=3,0 v=-2,-2
    p=7,6 v=-1,-3
    p=3,0 v=-1,-2
    p=9,3 v=2,3
    p=7,3 v=-1,2
    p=2,4 v=2,-3
    p=9,5 v=-3,-3
    """;

    public Day14SolverTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact(DisplayName ="2024 Day 14 Solver Has Correct Solution For Part 1 sample input")]
    public async Task Day14SolverHasCorrectSolutionForPart1SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day14Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer1.LastAsync();

        answer.Should().Be("12");
    }
}
