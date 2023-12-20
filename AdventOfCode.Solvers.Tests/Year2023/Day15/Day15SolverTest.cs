using AdventOfCode.Solvers.Year2023.Day15;

namespace AdventOfCode.Solvers.Tests.Year2023.Day15;

public class Day15SolverTest
{
    private readonly ITestOutputHelper _output;

    private string example = """
    rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7
    """;

    public Day15SolverTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact(DisplayName ="2023 Day 15 Solver Has Correct Solution For Part 1 sample input")]
    public async Task Day15SolverHasCorrectSolutionForPart1SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day15Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer1.LastAsync();

        answer.Should().Be("1320");
    }

    [Fact(DisplayName ="2023 Day 15 Solver Has Correct Solution For Part 2 sample input")]
    public async Task Day15SolverHasCorrectSolutionForPart2SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day15Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer2.LastAsync();

        answer.Should().Be("145");
    }}
