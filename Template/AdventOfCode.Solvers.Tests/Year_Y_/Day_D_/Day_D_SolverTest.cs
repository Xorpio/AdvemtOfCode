using AdventOfCode.Solvers.Year_Y_.Day_D_;

namespace AdventOfCode.Solvers.Tests.Year_Y_.Day_D_;

public class Day_D_SolverTest
{
    private readonly ITestOutputHelper _output;

    private string example = """
    sampleinput
    """;

    public Day_D_SolverTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact(DisplayName ="_Y_ Day _D_ Solver Has Correct Solution For Part 1 sample input")]
    public async Task Day_D_SolverHasCorrectSolutionForPart1SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day_D_Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer1.LastAsync();

        answer.Should().Be("?");
    }

    [Fact(DisplayName ="_Y_ Day _D_ Solver Has Correct Solution For Part 2 sample input")]
    public async Task Day_D_SolverHasCorrectSolutionForPart2SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day_D_Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer2.LastAsync();

        answer.Should().Be("?");
    }}
