using PuzzleConsole.Year2021.Day1;
using Xunit;

namespace PuzzleConsole.test.Year2021;

public class Day1Test
{
    [Fact]
    public void TestCanSolve()
    {
        var mocker = new AutoMocker(Moq.MockBehavior.Strict);

        var sut = mocker.CreateInstance<Day1>();

        var puzzle = new string[]
        {
            "199",
            "200",
            "208",
            "210",
            "200",
            "207",
            "240",
            "269",
            "260",
            "263"
        };

        var solution = sut.Solve(puzzle);

        solution.Should().HaveCount(2);
        solution.First().Should().Be("7");
        solution.Last().Should().Be("5");
    }
}
