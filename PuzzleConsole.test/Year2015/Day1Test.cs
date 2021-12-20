using PuzzleConsole.Year2015.Day1;
using Xunit;

namespace PuzzleConsole.test.Year2015;

public class Day1Test
{
    [Fact]
    public void TestCanSolve()
    {
        var mocker = new AutoMocker(Moq.MockBehavior.Strict);

        var sut = mocker.CreateInstance<Day1>();

        var puzzle = new string[]
        {
            "(())",
        };

        var solution = sut.Solve(puzzle);

        solution.First().Should().Be("0");
    }
    [Fact]
    public void TestCanSolvePart2()
    {
        var mocker = new AutoMocker(Moq.MockBehavior.Strict);

        var sut = mocker.CreateInstance<Day1>();

        var puzzle = new string[]
        {
            "()())",
        };

        var solution = sut.Solve(puzzle);

        solution.Should().HaveCount(2);
        solution.Last().Should().Be("5");
    }
}
