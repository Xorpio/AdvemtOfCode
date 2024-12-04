using AdventOfCode.Solvers.Year2024.Day4;

namespace AdventOfCode.Solvers.Tests.Year2024.Day4;

public class Day4SolverTest
{
    private readonly ITestOutputHelper _output;

    private string example = """
    MMMSXXMASM
    MSAMXMSMSA
    AMXSXMAAMM
    MSAMASMSMX
    XMASAMXAMM
    XXAMMXXAMA
    SMSMSASXSS
    SAXAMASAAA
    MAMMMXMMMM
    MXMXAXMASX
    """;

    public Day4SolverTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact(DisplayName = "2024 Day 4 Solver Has Correct Solution For Part 1 sample input")]
    public async Task Day4SolverHasCorrectSolutionForPart1SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day4Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer1.LastAsync();

        answer.Should().Be("18");
    }

    [Fact(DisplayName = "2024 Day 4 Solver Has Correct Solution For Part 2 sample input")]
    public async Task Day4SolverHasCorrectSolutionForPart2SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day4Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer2.LastAsync();

        answer.Should().Be("9");
    }
}
