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
            var games = sut.PuzzleToGame(puzzle);
            var game = games[0];

            var pods = new List<Amphipod>()
            {
                new Amphipod(false, false, new Location(2,1), PodType.Bronze),
                new Amphipod(false, false, new Location(4,1), PodType.Copper),
                new Amphipod(false, false, new Location(6,1), PodType.Bronze),
                new Amphipod(false, false, new Location(8,1), PodType.Dessert),

                new Amphipod(false, false, new Location(2,2), PodType.Amber),
                new Amphipod(false, false, new Location(4,2), PodType.Dessert),
                new Amphipod(false, false, new Location(6,2), PodType.Copper),
                new Amphipod(false, false, new Location(8,2), PodType.Amber),
            };

            var podsp2 = new List<Amphipod>()
            {
                new Amphipod(false, false, new Location(2,1), PodType.Bronze),
                new Amphipod(false, false, new Location(4,1), PodType.Copper),
                new Amphipod(false, false, new Location(6,1), PodType.Bronze),
                new Amphipod(false, false, new Location(8,1), PodType.Dessert),

                new Amphipod(false, false, new Location(2,2), PodType.Dessert),
                new Amphipod(false, false, new Location(4,2), PodType.Copper),
                new Amphipod(false, false, new Location(6,2), PodType.Bronze),
                new Amphipod(false, false, new Location(8,2), PodType.Amber),

                new Amphipod(false, false, new Location(2,3), PodType.Dessert),
                new Amphipod(false, false, new Location(4,3), PodType.Bronze),
                new Amphipod(false, false, new Location(6,3), PodType.Amber),
                new Amphipod(false, false, new Location(8,3), PodType.Copper),

                new Amphipod(false, false, new Location(2,4), PodType.Amber),
                new Amphipod(false, false, new Location(4,4), PodType.Dessert),
                new Amphipod(false, false, new Location(6,4), PodType.Copper),
                new Amphipod(false, false, new Location(8,4), PodType.Amber),
            };

            games[0].Amphipods.Should().BeEquivalentTo(pods);
            games[1].Amphipods.Should().BeEquivalentTo(podsp2);
        });

        scenario.Fact("Solve should give solution", () =>
        {
            var games = sut.PuzzleToGame(puzzle);

            var solution = sut.SolveGame(games[0]);

            solution.Should().Be(12521);
        });

        scenario.Fact("Solve part 2 should give solution", () =>
        {
            var games = sut.PuzzleToGame(puzzle);

            var solution = sut.SolveGame(games[1]);

            solution.Should().Be(44169);
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
            var start = new Location(3, 0);
            var pod = new Amphipod(true, false, start, PodType.Dessert);

            var game = new Game(new List<Amphipod>()
            {
                new Amphipod(false, false, new Location(1, 0), PodType.Amber),
                pod
            }, 0);

            var expexted = new List<Move>()
            {
                new Move(start, new Location(8,1), PodType.Dessert),
            };

            var moves = pod.FindMoves(game);
            moves.Should().BeEquivalentTo(expexted);
        });

        scenario.Fact("In hall and to cave is free to left", () =>
        {
            var start = new Location(9, 0);
            var pod = new Amphipod(true, false, start, PodType.Amber);

            var game = new Game(new List<Amphipod>()
            {
                pod
            }, 0);

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
                var start = new Location(3, 0);
                var pod = new Amphipod(true, false, start, PodType.Dessert);

                var game = new Game(new List<Amphipod>()
                {
                    new Amphipod(false, false, new Location(1, 0), PodType.Amber),
                    new Amphipod(false, false, new Location(8, i), PodType.Amber),
                    pod
                }, 0);

                var expexted = new List<Move>()
                {
                };

                var moves = pod.FindMoves(game);
                moves.Should().BeEquivalentTo(expexted);
            });
        }

        scenario.Fact("In cave but other pod is not correct", () =>
        {
            var start = new Location(8, 1);
            var pod = new Amphipod(false, false, start, PodType.Dessert);

            var game = new Game(new List<Amphipod>()
            {
                new Amphipod(false, false, new Location(7, 0), PodType.Amber),
                new Amphipod(false, false, new Location(10, 0), PodType.Amber),
                new Amphipod(false, false, new Location(8, 2), PodType.Amber),
                pod
            }, 0);

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


            var solution = sut.SolveGame(game);
            solution.Should().Be(9);
        });
        scenario.Fact("two pod should be able to solve andwalk deepest" , () =>
        {
            var game = new Game(new List<Amphipod>()
            {
                new Amphipod(false, false, new Location(8, 2), PodType.Amber),
                new Amphipod(false, false, new Location(6, 2), PodType.Amber)
            }, 0);


            var solution = sut.SolveGame(game);
            solution.Should().Be(17);
        });

        scenario.Fact("In hall and to cave is free but cave is ocupied by same type", () =>
        {
            var start = new Location(3, 0);
            var pod = new Amphipod(true, false, start, PodType.Dessert);

            var game = new Game(new List<Amphipod>()
            {
                new Amphipod(false, false, new Location(1, 0), PodType.Amber),
                new Amphipod(false, false, new Location(8, 2), PodType.Dessert),
                pod
            }, 0);


            var expexted = new List<Move>()
            {
                new Move(start, new Location(8,1), PodType.Dessert),
            };

            var moves = pod.FindMoves(game);
            moves.Should().BeEquivalentTo(expexted);
        });
    }
}
