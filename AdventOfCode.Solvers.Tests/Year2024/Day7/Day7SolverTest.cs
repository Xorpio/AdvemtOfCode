using AdventOfCode.Solvers.Year2024.Day7;

namespace AdventOfCode.Solvers.Tests.Year2024.Day7;

public class Day7SolverTest
{
    private readonly ITestOutputHelper _output;

    private string example = """
    190: 10 19
    3267: 81 40 27
    83: 17 5
    156: 15 6
    7290: 6 8 6 15
    161011: 16 10 13
    192: 17 8 14
    21037: 9 7 18 13
    292: 11 6 16 20
    """;

    public Day7SolverTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact(DisplayName = "2024 Day 7 Solver Has Correct Solution For Part 1 sample input")]
    public async Task Day7SolverHasCorrectSolutionForPart1SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day7Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer1.LastAsync();

        answer.Should().Be("3749");
    }

    [Fact(DisplayName = "2024 Day 7 Solver Has Correct Solution For Part 2 sample input")]
    public async Task Day7SolverHasCorrectSolutionForPart2SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day7Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer2.LastAsync();

        answer.Should().Be("11387");
    }

    [Fact]
    public async Task testje()
    {
        var ints = new int[] { 1, 2, 3, 4, 5 };
        var ints2 = ints[..3].Concat(ints[3..]).ToArray();

        ints.Should().BeEquivalentTo(ints2);
    }
}
