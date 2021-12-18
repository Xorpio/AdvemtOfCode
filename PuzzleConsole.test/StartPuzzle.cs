using PuzzleConsole.Year2021.Day1;
using Xunit;

namespace PuzzleConsole.test;

public class StartPuzzle
{
    [Fact]
    public void Test1()
    {
        var solver = new Day1();
        Assert.IsType<Day1>(solver);
    }
}
