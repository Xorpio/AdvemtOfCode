using PuzzleConsole.Year2021.Day21;

namespace PuzzleConsole.test.Year2021;

[Trait("Year", "2021")]
[Trait("Day", "21")]
public partial class Day21Test {
    [Scenario]
    public void SolveTest(ScenarioContext scenario)
    {
        var sut = new Day21();

        scenario.Fact("Sut should not be null", () =>
        {
            sut.Should().NotBeNull();
        });

        var examples = new (int startSpot, int endSpot, int move, int startScore, int endScore)[]
        {
            new (1, 1, 10, 13, 14),
            new (10, 10, 10, 13, 23),
            new (10, 1, 1, 13, 14),
            new (1, 2, 1, 13, 15)
        };

        foreach(var example in examples)
        {
            scenario.Theory("MoveAndScore", example, () =>
            {
                sut.player1 = example.startSpot;
                sut.score[0] = example.startScore;
                sut.MoveAndScore(example.move, 0);

                sut.score[0].Should().Be(example.endScore);
                sut.player1.Should().Be(example.endSpot);
            });
        }
            

        scenario.Fact("Example game", () =>
        {
            var (ScorePlayer1, ScorePlayer2, rolls) = sut.PlayAGame(4, 8);

            ScorePlayer1.Should().Be(1000);
            ScorePlayer2.Should().Be(745);
            rolls.Should().Be(993);
        });

        scenario.Fact("100 rolls to 1", () => {
            sut.dice = 99;
            var r1 = sut.Roll();
            sut.dice = 100;
            var r2 = sut.Roll();

            r1.Should().Be(100);
            r2.Should().Be(1);
        });

        scenario.Fact("Solve puzzle", () => {
            var puzzle = new string[]
            {
                "Player 1 starting position: 4",
                "Player 2 starting position: 8"
            };

            var solution = sut.Solve(puzzle);

            solution.First().Should().Be("739785");

            solution.Last().Should().Be("444356092776315");
        });
    }
}
