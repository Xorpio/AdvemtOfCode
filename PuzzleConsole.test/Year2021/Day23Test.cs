using PuzzleConsole.Year2021.Day23;

namespace PuzzleConsole.test.Year2021;

[Trait("Year", "2021")]
[Trait("Day", "23")]
public partial class Day23Test {
    [Scenario]
    public void SolveTest(ScenarioContext scenario)
    {
        var sut = new Day23();

        scenario.Fact("Sut should not be null", () =>
        {
            sut.Should().NotBeNull();
        });

        var puzzle = new[]
        {
            "#############",
            "#...........#",
            "###B#C#B#D###",
            "  #A#D#C#A#",
            "  #########",
        };

        scenario.Fact("Puzzle should be created", () =>
        {
            var game = sut.PuzzleToGame(puzzle);

            game.Amphipods.Should().HaveCount(8);
            game.Amphipods[0].Type.Should().Be(PodType.Bronze);
            game.Amphipods[0].Location.Col.Should().Be(2);
            game.Amphipods[0].Location.Row.Should().Be(1);

            game.Amphipods[1].Type.Should().Be(PodType.Copper);
            game.Amphipods[1].Location.Col.Should().Be(4);
            game.Amphipods[1].Location.Row.Should().Be(1);

            game.Amphipods[2].Type.Should().Be(PodType.Bronze);
            game.Amphipods[2].Location.Col.Should().Be(6);
            game.Amphipods[2].Location.Row.Should().Be(1);

            game.Amphipods[3].Type.Should().Be(PodType.Dessert);
            game.Amphipods[3].Location.Col.Should().Be(8);
            game.Amphipods[3].Location.Row.Should().Be(1);

            game.Amphipods[4].Type.Should().Be(PodType.Amber);
            game.Amphipods[4].Location.Col.Should().Be(2);
            game.Amphipods[4].Location.Row.Should().Be(2);

            game.Amphipods[5].Type.Should().Be(PodType.Dessert);
            game.Amphipods[5].Location.Col.Should().Be(4);
            game.Amphipods[5].Location.Row.Should().Be(2);

            game.Amphipods[6].Type.Should().Be(PodType.Copper);
            game.Amphipods[6].Location.Col.Should().Be(6);
            game.Amphipods[6].Location.Row.Should().Be(2);

            game.Amphipods[7].Type.Should().Be(PodType.Amber);
            game.Amphipods[7].Location.Col.Should().Be(8);
            game.Amphipods[7].Location.Row.Should().Be(2);

        });

        scenario.Fact("Solve should give solution", () =>
        {
            var game = sut.PuzzleToGame(puzzle);

            var solution = sut.SolveGame(game, new List<Move>());

            solution.Should().Be(12521);
        });

        scenario.Fact("Moves should be calculated to the left", () =>
        {
            var game = new Game(new List<Amphipod>()
            {
                new Amphipod(false, false, new Location(3, 0), PodType.Amber)
            }, 0);

            var start = new Location(2, 1);
            var pod = new Amphipod(false, false, start, PodType.Dessert);

            var expexted = new List<Move>()
            {
                new Move(start, new Location(0,0), PodType.Dessert),
                new Move(start, new Location(1,0), PodType.Dessert)
            };

            var moves = pod.FindMoves(game);
            moves.Should().BeEquivalentTo(expexted);
        });

        scenario.Fact("Moves should be calculated to the right", () =>
        {
            var game = new Game(new List<Amphipod>()
            {
                new Amphipod(false, false, new Location(1, 0), PodType.Amber),
                new Amphipod(false, false, new Location(5, 0), PodType.Amber)
            }, 0);

            var start = new Location(2, 1);
            var pod = new Amphipod(false, false, start, PodType.Dessert);

            var expexted = new List<Move>()
            {
                new Move(start, new Location(3,0), PodType.Dessert),
            };

            var moves = pod.FindMoves(game);
            moves.Should().BeEquivalentTo(expexted);
        });

        scenario.Fact("in hall should not calculate to hall", () =>
        {
            var game = new Game(new List<Amphipod>()
            {
                new Amphipod(false, false, new Location(5, 0), PodType.Amber)
            }, 0);

            var start = new Location(3, 0);
            var pod = new Amphipod(true, false, start, PodType.Dessert);

            var expexted = new List<Move>()
            {
            };

            var moves = pod.FindMoves(game);
            moves.Should().BeEquivalentTo(expexted);
        });

        scenario.Fact("In hall and to cave is free", () =>
        {
            var game = new Game(new List<Amphipod>()
            {
                new Amphipod(false, false, new Location(1, 0), PodType.Amber)
            }, 0);

            var start = new Location(3, 0);
            var pod = new Amphipod(true, false, start, PodType.Dessert);

            var expexted = new List<Move>()
            {
                new Move(start, new Location(8,1), PodType.Dessert),
            };

            var moves = pod.FindMoves(game);
            moves.Should().BeEquivalentTo(expexted);
        });

        scenario.Fact("In hall and to cave is free to left", () =>
        {
            var game = new Game(new List<Amphipod>()
            {
            }, 0);

            var start = new Location(9, 0);
            var pod = new Amphipod(true, false, start, PodType.Amber);

            var expexted = new List<Move>()
            {
                new Move(start, new Location(2,1), PodType.Amber),
            };

            var moves = pod.FindMoves(game);
            moves.Should().BeEquivalentTo(expexted);
        });

        for (int i = 1; i < 3; i++)
        {
            scenario.Theory("In hall and to cave is free but cave is ocupied", i, () =>
            {
                var game = new Game(new List<Amphipod>()
                {
                    new Amphipod(false, false, new Location(1, 0), PodType.Amber),
                    new Amphipod(false, false, new Location(8, i), PodType.Amber)
                }, 0);

                var start = new Location(3, 0);
                var pod = new Amphipod(true, false, start, PodType.Dessert);

                var expexted = new List<Move>()
                {
                };

                var moves = pod.FindMoves(game);
                moves.Should().BeEquivalentTo(expexted);
            });
        }

        scenario.Fact("In cave but other pod is not correct", () =>
        {
            var game = new Game(new List<Amphipod>()
            {
                new Amphipod(false, false, new Location(7, 0), PodType.Amber),
                new Amphipod(false, false, new Location(10, 0), PodType.Amber),
                new Amphipod(false, false, new Location(8, 2), PodType.Amber)
            }, 0);

            var start = new Location(8, 1);
            var pod = new Amphipod(false, false, start, PodType.Dessert);

            var expexted = new List<Move>()
            {
                new Move(start, new Location(9,0), PodType.Dessert),
            };

            var moves = pod.FindMoves(game);
            moves.Should().BeEquivalentTo(expexted);
        });

        scenario.Fact("One pod should be able to solve", () =>
        {
            var game = new Game(new List<Amphipod>()
            {
                new Amphipod(false, false, new Location(8, 2), PodType.Amber)
            }, 0);


            var solution = sut.SolveGame(game, new List<Move>());
            solution.Should().Be(2);
        });

        scenario.Fact("In hall and to cave is free but cave is ocupied by same type", () =>
        {
            var game = new Game(new List<Amphipod>()
            {
                new Amphipod(false, false, new Location(1, 0), PodType.Amber),
                new Amphipod(false, false, new Location(8, 1), PodType.Dessert)
            }, 0);

            var start = new Location(3, 0);
            var pod = new Amphipod(true, false, start, PodType.Dessert);

            var expexted = new List<Move>()
            {
                new Move(start, new Location(8,1), PodType.Dessert),
            };

            var moves = pod.FindMoves(game);
            moves.Should().BeEquivalentTo(expexted);
        });
    }
}
