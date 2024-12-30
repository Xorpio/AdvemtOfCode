using AdventOfCode.Solvers.Year2024.Day22;

namespace AdventOfCode.Solvers.Tests.Year2024.Day22;

public class Day22SolverTest
{
    private readonly ITestOutputHelper _output;


    public Day22SolverTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact(DisplayName = "2024 Day 22 Solver Has Correct Solution For Part 1 sample input")]
    public async Task Day22SolverHasCorrectSolutionForPart1SampleInputAsync()
    {
        string example1 = """
        1
        10
        100
        2024
        """;
        var lines = example1.Split(Environment.NewLine);

        var solver = new Day22Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer1.LastAsync();

        answer.Should().Be("37327623");
    }

    [Theory(DisplayName = "calc works")]
    [InlineData(15887950, 1)]
    [InlineData(16495136, 2)]
    [InlineData(527345, 3)]
    [InlineData(704524, 4)]
    [InlineData(1553684, 5)]
    [InlineData(12683156, 6)]
    [InlineData(11100544, 7)]
    [InlineData(12249484, 8)]
    [InlineData(7753432, 9)]
    [InlineData(5908254, 10)]
    public void calcSecretWorks(decimal expected, int iterations)
    {
        var solver = new Day22Solver();
        var result = solver.calcSecret(123, iterations);
        result.Last().Should().Be(expected);
    }

    [Fact(DisplayName = "2024 Day 22 Solver Has Correct Solution For Part 2 sample input")]
    public async Task Day22SolverHasCorrectSolutionForPart2SampleInputAsync()
    {
        string example2 = """
        1
        2
        3
        2024
        """;
        var lines = example2.Split(Environment.NewLine);

        var solver = new Day22Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer2.LastAsync();

        answer.Should().Be("23");
    }
}
