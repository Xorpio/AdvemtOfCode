using PuzzleConsole.Year2015.Day2;

namespace PuzzleConsole.test.Year2015;

public class Day2Test
{
    [Fact]
    public void SolveTest()
    {
        var sut = new Day2();

        var puzzle = new string[] { "2x3x4", "1x1x10" };

        var solution = sut.Solve(puzzle);
        

        solution.First().Should().Be("101");
        solution.Last().Should().Be("48");
    }
    
    [Fact]
    public void SolveLine1()
    {
        var sut = new Day2();

        var puzzle = "2x3x4";

        var solution = sut.SolveLine(puzzle);

        solution.Should().Be(58);
    }
    
    [Fact]
    public void SolveLine2()
    {
        var sut = new Day2();

        var puzzle = "1x1x10";

        var solution = sut.SolveLine(puzzle);

        solution.Should().Be(43);
    }
    
    [Fact]
    public void SolveRiibon1()
    {
        var sut = new Day2();

        var puzzle = "2x3x4";

        var solution = sut.SolveRibbon(puzzle);
        var solution2 = sut.SolveRibbon("4x2x3");

        solution.Should().Be(34);
        solution2.Should().Be(34);
    }
    
    [Fact]
    public void SolveRibon2()
    {
        var sut = new Day2();

        var puzzle = "1x1x10";

        var solution = sut.SolveRibbon(puzzle);

        solution.Should().Be(14);
    }
}