using AdventOfCode.Solvers.Year2023.Day12;

namespace AdventOfCode.Solvers.Tests.Year2023.Day12;

public class Day12SolverTest
{
    private readonly ITestOutputHelper _output;

    private string example = """
    ???.### 1,1,3
    .??..??...?##. 1,1,3
    ?#?#?#?#?#?#?#? 1,3,1,6
    ????.#...#... 4,1,1
    ????.######..#####. 1,6,5
    ?###???????? 3,2,1
    """;

    public Day12SolverTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact(DisplayName ="2023 Day 12 Solver Has Correct Solution For Part 1 sample input")]
    public async Task Day12SolverHasCorrectSolutionForPart1SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day12Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer1.LastAsync();

        answer.Should().Be("21");
    }

    [Fact(DisplayName ="2023 Day 12 Solver Has Correct Solution For Part 2 sample input")]
    public async Task Day12SolverHasCorrectSolutionForPart2SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day12Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer2.LastAsync();

        answer.Should().Be("525152");
    }

    [Fact]
    public void testLine()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day12Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        var ans = solver.solveRecursive("?###????????", null, new[] { 3, 2, 1 });

        ans.Should().Be(10);
    }
}
