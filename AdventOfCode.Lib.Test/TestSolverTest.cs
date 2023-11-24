using System.Reactive.Linq;
using FluentAssertions;
using Moq;
using Moq.AutoMock;

namespace AdventOfCode.Lib.Tests;

public class TestSolverTest
{
    private AutoMocker _mocker;
    private TestSolver _sut;

    public TestSolverTest()
    {
        _mocker = new AutoMocker(MockBehavior.Strict);
        _sut = _mocker.CreateInstance<TestSolver>();
    }

    [Fact(DisplayName = "TestAnswer1")]
    public async Task Test1Async()
    {
        // arrange
        var puzzle = new string[] { "foo", "bar" };

        // act
        _sut.Solve(puzzle);

        // assert
        _sut.Answer1.Subscribe(answer =>
        {
            answer.Should().Be("answer1foo");
        });
    }

    [Fact(DisplayName = "TestAnswer2")]
    public async Task Test2Async()
    {
        // arrange
        var puzzle = new string[] { "foo", "bar" };

        // act
        _sut.Solve(puzzle);

        // assert
        _sut.Answer2.Subscribe(answer =>
        {
            answer.Should().Be("answer2bar");
        });
    }

    [Fact(DisplayName = "TestNewPuzzle")]
    public async Task TestNewPuzzle()
    {
        // arrange
        var puzzle = new string[] { "foo", "bar" };

        // act
        _sut.Solve(puzzle);

        // assert
        var answer1 = await _sut.Answer1.LastAsync();
        answer1.Should().Be("answer1foo");

        _sut.Reset();

        //give new puzzle
        puzzle = new string[] { "baz", "qux" };
        _sut.Solve(puzzle);
        answer1 = await _sut.Answer1.LastAsync();
        answer1.Should().Be("answer1baz");
    }
}
