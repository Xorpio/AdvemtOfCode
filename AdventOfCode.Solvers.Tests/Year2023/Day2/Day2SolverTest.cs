using System.Reactive.Linq;
using AdventOfCode.Solvers.Year2023.Day2;
using FluentAssertions;

namespace AdventOfCode.Solvers.Tests.Year2023.Day2;

public class Day2SolverTest
{
    [Theory(DisplayName = "TestGame")]
    [InlineData("Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green", 1,6,4,2)]
    [InlineData("Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue", 2,4,1,3)]
    [InlineData("Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red", 3,6,20,13)]
    [InlineData("Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red", 4,15,14,3)]
    [InlineData("Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green", 5,2,6,3)]
    public void TestGame(string input, int id, int blue, int red, int green)
    {
        var game = new Game(input);

        game.Id.Should().Be(id);
        game.Blue.Should().Be(blue);
        game.Red.Should().Be(red);
        game.Green.Should().Be(green);
    }

    [Fact(DisplayName = "TestSolvePart1")]
    public async Task TestSolvePart1Async()
    {
        var solver = new Day2Solver();
        var puzzle = new[]
        {
            "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green",
            "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue",
            "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red",
            "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red",
            "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green"
        };

        solver.Solve(puzzle);

        string answer = await solver.Answer1.LastAsync();

        answer.Should().Be("8");
    }

    [Theory(DisplayName = "TestGame")]
    [InlineData("Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green", 1,4, 2, 6, 48)]
    [InlineData("Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue", 2,1, 3, 4, 12)]
    [InlineData("Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red", 3,20, 13, 6, 1560)]
    [InlineData("Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red", 4,14,3,15,630)]
    [InlineData("Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green", 5,6,3,2,36)]
    public void TestGamePower(string input, int id, int red, int green, int blue, int power)
    {
        var game = new Game(input);

        game.Id.Should().Be(id);
        game.Blue.Should().Be(blue);
        game.Red.Should().Be(red);
        game.Green.Should().Be(green);
        game.Power.Should().Be(power);
    }

    [Fact(DisplayName = "TestSolvePart2")]
    public async Task TestSolvePart2Async()
    {
        var solver = new Day2Solver();
        var puzzle = new[]
        {
            "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green",
            "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue",
            "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red",
            "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red",
            "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green"
        };

        solver.Solve(puzzle);

        string answer = await solver.Answer2.LastAsync();

        answer.Should().Be("2286");
    }
}
