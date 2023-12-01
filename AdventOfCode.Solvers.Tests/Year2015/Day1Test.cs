using System.Formats.Asn1;
using System.Reactive.Linq;
using AdventOfCode.Solvers.Year2015;
using FluentAssertions;

namespace AdventOfCode.Solvers.Tests.Year2015;
public class Day1Test
{
    [Theory(DisplayName = "TestAnswer1")]
    [InlineData(new string[] { "(())" }, "0")]
    [InlineData(new string[] { "()()" }, "0")]
    [InlineData(new string[] { "(()(()(" }, "3")]
    [InlineData(new string[] { "(((" }, "3")]
    [InlineData(new string[] { "())" }, "-1")]
    [InlineData(new string[] { "))(" }, "-1")]
    [InlineData(new string[] { ")))" }, "-3")]
    [InlineData(new string[] { ")())())" }, "-3")]
    public async void TestPart1(string[] puzzle, string expected)
    {
        var solver = new Day1();

        solver.Solve(puzzle);

        string answer = await solver.Answer1.LastAsync();

        answer.Should().Be(expected);
    }

    [Theory(DisplayName = "TestAnswer1")]
    [InlineData(new string[] { ")" }, "1")]
    [InlineData(new string[] { "()())" }, "5")]
    public async void TestPart2(string[] puzzle, string expected)
    {
        var solver = new Day1();

        solver.Solve(puzzle);

        string answer = await solver.Answer2.LastAsync();

        answer.Should().Be(expected);
    }
}
