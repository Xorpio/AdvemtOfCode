using AdventOfCode.Solvers.Year2024.Day12;

namespace AdventOfCode.Solvers.Tests.Year2024.Day12;

public class Day12SolverTest
{
    private readonly ITestOutputHelper _output;

    private string example = """
    RRRRIICCFF
    RRRRIICCCF
    VVRRRCCFFF
    VVRCCCJFFF
    VVVVCJJCFE
    VVIVCCJJEE
    VVIIICJJEE
    MIIIIIJJEE
    MIIISIJEEE
    MMMISSJEEE
    """;


    //private string example = """
    //AAAAAA
    //AAABBA
    //AAABBA
    //ABBAAA
    //ABBAAA
    //AAAAAA
    //""";

    public Day12SolverTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact(DisplayName = "2024 Day 12 Solver Has Correct Solution For Part 1 sample input")]
    public async Task Day12SolverHasCorrectSolutionForPart1SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day12Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer1.LastAsync();

        answer.Should().Be("1930");
    }

    [Fact(DisplayName = "2024 Day 12 Solver Has Correct Solution For Part 2 sample input")]
    public async Task Day12SolverHasCorrectSolutionForPart2SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day12Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer2.LastAsync();

        answer.Should().Be("1206");
    }
}
