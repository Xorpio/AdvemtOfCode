using AdventOfCode.Solvers.Year2024.Day3;

namespace AdventOfCode.Solvers.Tests.Year2024.Day3;

public class Day3SolverTest
{
    private readonly ITestOutputHelper _output;

    private string example1 = """
    xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))
    """;
    private string example2 = """
    xmul(2,4)&mul[3,7]!^don't()_mul(5,5)don't()+mul(32,64](mul(11,8)undo()?mul(8,5))
    """;

    public Day3SolverTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact(DisplayName = "2024 Day 3 Solver Has Correct Solution For Part 1 sample input")]
    public async Task Day3SolverHasCorrectSolutionForPart1SampleInputAsync()
    {
        var lines = example1.Split(Environment.NewLine);

        var solver = new Day3Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer1.LastAsync();

        answer.Should().Be("161");
    }

    [Fact(DisplayName = "2024 Day 3 Solver Has Correct Solution For Part 2 sample input")]
    public async Task Day3SolverHasCorrectSolutionForPart2SampleInputAsync()
    {
        var lines = example2.Split(Environment.NewLine);

        var solver = new Day3Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer2.LastAsync();

        answer.Should().Be("48");
    }
}
