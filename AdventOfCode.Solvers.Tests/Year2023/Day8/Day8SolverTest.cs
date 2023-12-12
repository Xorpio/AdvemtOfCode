using AdventOfCode.Solvers.Year2023.Day8;

namespace AdventOfCode.Solvers.Tests.Year2023.Day8;

public class Day8SolverTest
{
    private readonly ITestOutputHelper _output;

    private string example = """
    RL

    AAA = (BBB, CCC)
    BBB = (DDD, EEE)
    CCC = (ZZZ, GGG)
    DDD = (DDD, DDD)
    EEE = (EEE, EEE)
    GGG = (GGG, GGG)
    ZZZ = (ZZZ, ZZZ)
    """;

    public Day8SolverTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact(DisplayName ="2023 Day 8 Solver Has Correct Solution For Part 1 sample input")]
    public async Task Day8SolverHasCorrectSolutionForPart1SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day8Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer1.LastAsync();

        answer.Should().Be("2");
    }

    [Fact(DisplayName ="2023 Day 8 Solver Has Correct Solution For Part 2 sample input")]
    public async Task Day8SolverHasCorrectSolutionForPart2SampleInputAsync()
    {
        var e = """
        LR

        11A = (11B, XXX)
        11B = (XXX, 11Z)
        11Z = (11B, XXX)
        22A = (22B, XXX)
        22B = (22C, 22C)
        22C = (22Z, 22Z)
        22Z = (22B, 22B)
        XXX = (XXX, XXX)
        """;
        var lines = e.Split(Environment.NewLine);

        var solver = new Day8Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer2.LastAsync();

        answer.Should().Be("6");
    }}
