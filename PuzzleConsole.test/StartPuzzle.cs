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

    [Fact]
    public void testRecord()
    {
        var a = new TestA(new List<TestB> { new TestB("Fred") });

        var c = a with { TestBs = new List<TestB>(a.TestBs)};

        a.TestBs.Add(new TestB("Mark"));

        a.Should().Be(c);
    }
}

public record TestA(List<TestB> TestBs);
public record TestB(string name);
