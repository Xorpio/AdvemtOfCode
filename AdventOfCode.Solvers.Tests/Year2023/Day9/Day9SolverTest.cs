using System.Diagnostics;
using AdventOfCode.Solvers.Year2023.Day9;

namespace AdventOfCode.Solvers.Tests.Year2023.Day9;

public class Day9SolverTest
{
    private readonly ITestOutputHelper _output;

    private string example = """
    0 3 6 9 12 15
    1 3 6 10 15 21
    10 13 16 21 30 45
    """;

    public Day9SolverTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact(DisplayName ="2023 Day 9 Solver Has Correct Solution For Part 1 sample input")]
    public async Task Day9SolverHasCorrectSolutionForPart1SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day9Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer1.LastAsync();

        answer.Should().Be("114");
    }

    [Fact(DisplayName ="2023 Day 9 Solver Has Correct Solution For Part 2 sample input")]
    public async Task Day9SolverHasCorrectSolutionForPart2SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day9Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer2.LastAsync();

        answer.Should().Be("2");
    }

    [Theory]
    [InlineData("13 23 46 97 206 436 919 1924 3971 8016 15765 30243 56875 105604 195153 361805 677725 1289089 2494076 4898137 9713336", 9602984)]
    [InlineData("0 3 6 9 12 15", 18)]
    public void TestVersion1(string input, double answer)
    {
        var numbers = input.Split(' ').Select(double.Parse).ToArray();

        var ans = version1(numbers);
        ans.Should().Be(answer);
    }

    private double version1(double[] numbers)
    {
        var timer = new Stopwatch();
        timer.Start();

        var all0 = true;
        double[] intervals = new double[numbers.Length - 1];
        for(var i = 0; i < numbers.Length - 1; i++)
        {
            intervals[i] = numbers[i + 1] - numbers[i];
            if (intervals[i] != 0)
            {
                all0 = false;
            }
        }

        timer.Stop();
        _output.WriteLine($"Time: {timer.Elapsed}");

        if (all0)
        {
            return 0;
        }

        return intervals.Last() + version1(intervals);
    }

    [Theory]
    [InlineData("13 23 46 97 206 436 919 1924 3971 8016 15765 30243 56875 105604 195153 361805 677725 1289089 2494076 4898137 9713336", 9602984)]
    [InlineData("0 3 6 9 12 15", 18)]
    public void Testversion2(string input, double answer)
    {
        var numbers = input.Split(' ').Select(double.Parse).ToArray();

        var ans = version2(numbers);
        ans.Should().Be(answer);
    }

    private double version2(IEnumerable<double> numbers)
    {
        var timer = new Stopwatch();
        timer.Start();

        var intervals = numbers.Select((n, i) => numbers.Skip(i).Take(2))
                                                        .Where(n => n.Count() == 2)
                                                        .Select(n => n.Last() - n.First());

        Console.WriteLine($"Time: {timer.Elapsed}");

        if (intervals.Any(n => n != 0))
        {
            timer.Stop();
            _output.WriteLine($"Time: {timer.Elapsed}");
            return intervals.Last() + version2(intervals);
        }

        timer.Stop();
        _output.WriteLine($"Time: {timer.Elapsed}");

        return 0;
    }
}
