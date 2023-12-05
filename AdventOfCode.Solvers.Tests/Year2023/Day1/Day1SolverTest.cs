using AdventOfCode.Solvers.Year2023.Day1;

namespace AdventOfCode.Solvers.Tests.Year2023.Day1;
public class Day1SolverTest
{
    [Theory(DisplayName = "TestAnswer1")]
    [InlineData("1abc2", 12)]
    [InlineData("pqr3stu8vwx", 38)]
    [InlineData("a1b2c3d4e5f", 15)]
    [InlineData("treb7uchet", 77)]
    public void TestPart1Line(string puzzle, int expected)
    {
        var solver = new Day1Solver();

        var answer = solver.SolveLine(puzzle);

        answer.Should().Be(expected);
    }

    [Fact(DisplayName = "TestAnswer1")]
    public async Task TestPart1Async()
    {
        var solver = new Day1Solver();

        var puzzle = new[]
        {
            "1abc2",
            "pqr3stu8vwx",
            "a1b2c3d4e5f",
            "treb7uchet"
        };

        solver.Solve(puzzle);

        string answer = await solver.Answer1.LastAsync();

        answer.Should().Be("142");
    }

    [Theory(DisplayName = "TestAnswer2")]
    [InlineData("two1nine", 29)]
    [InlineData("eightwothree", 83)]
    [InlineData("abcone2threexyz", 13)]
    [InlineData("xtwone3four", 24)]
    [InlineData("4nineeightseven2", 42)]
    [InlineData("zoneight234", 14)]
    [InlineData("7pqrstsixteen", 76)]
    [InlineData("9g", 99)]
    public void TestAnswer2LineFix(string line, int answer)
    {
        var solver = new Day1Solver();

        var result = solver.SolveLinePart2(line);

        result.Should().Be(answer);
    }

    [Fact(DisplayName = "TestAnswer2")]
    public async Task TestPart2Async()
    {
        var solver = new Day1Solver();

        var puzzle = new[]
        {
            "two1nine",
            "eightwothree",
            "abcone2threexyz",
            "xtwone3four",
            "4nineeightseven2",
            "zoneight234",
            "7pqrstsixteen"
        };

        solver.Solve(puzzle);

        string answer = await solver.Answer2.LastAsync();

        answer.Should().Be("281");
    }
}
