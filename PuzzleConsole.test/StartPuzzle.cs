using PuzzleConsole.Year2015;
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

    [Fact]
    public void Test2()
    {
        const string objectToInstantiate = "PuzzleConsole.Year2015.Day1, PuzzleConsole";

        var objectType = Type.GetType(objectToInstantiate);

        ISolver instantiatedObject = Activator.CreateInstance(objectType) as ISolver;

        Assert.IsType<Day1>(instantiatedObject);

        var answer = instantiatedObject.Solve(new string[] { "a", "b" });
        Assert.Equal("a", answer[0]);
        Assert.Single(answer);
    }
}
