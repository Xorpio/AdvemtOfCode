using PuzzleConsole.Year2021.Day22;

namespace PuzzleConsole.Year2021.Day23;

public class Day23 : ISolver
{
    public static readonly int[] Caves = { 2,4,6,8 };

    public string[] Solve(string[] puzzle)
    {


        var games = PuzzleToGame(puzzle);

        return new string[] { SolveGame(games[0]).ToString(), SolveGame(games[1]).ToString() };
    }

    public Game[] PuzzleToGame(string[] puzzle)
    {
        var pods = new List<Amphipod>();
        var podsPart2 = new List<Amphipod>();

        Location loc;
        PodType type;

        //#############
        //#...........#
        //###B#C#B#D###
        //  #A#D#C#A#
        //  #########

        var line = puzzle[2];
        var col = 3;
        loc = new Location(col - 1, 1);
        type = PodTypeFromChar(line[col]);
        pods.Add(new Amphipod(false, false, loc, type));
        podsPart2.Add(new Amphipod(false, false, loc, type));

        col += 2;
        loc = new Location(col - 1, 1);
        type = PodTypeFromChar(line[col]);
        pods.Add(new Amphipod(false, false, loc, type));
        podsPart2.Add(new Amphipod(false, false, loc, type));

        col += 2;
        loc = new Location(col - 1, 1);
        type = PodTypeFromChar(line[col]);
        pods.Add(new Amphipod(false, false, loc, type));
        podsPart2.Add(new Amphipod(false, false, loc, type));

        col += 2;
        loc = new Location(col - 1, 1);
        type = PodTypeFromChar(line[col]);
        pods.Add(new Amphipod(false, false, loc, type));
        podsPart2.Add(new Amphipod(false, false, loc, type));

        line = puzzle[3];
        col = 3;
        loc = new Location(col - 1, 2);
        var loc2 = new Location(col - 1, 4);
        type = PodTypeFromChar(line[col]);
        pods.Add(new Amphipod(false, false, loc, type));
        podsPart2.Add(new Amphipod(false, false, loc2, type));

        col += 2;
        loc = new Location(col - 1, 2);
        loc2 = new Location(col - 1, 4);
        type = PodTypeFromChar(line[col]);
        pods.Add(new Amphipod(false, false, loc, type));
        podsPart2.Add(new Amphipod(false, false, loc2, type));

        col += 2;
        loc = new Location(col - 1, 2);
        loc2 = new Location(col - 1, 4);
        type = PodTypeFromChar(line[col]);
        pods.Add(new Amphipod(false, false, loc, type));
        podsPart2.Add(new Amphipod(false, false, loc2, type));

        col += 2;
        loc = new Location(col - 1, 2);
        loc2 = new Location(col - 1, 4);
        type = PodTypeFromChar(line[col]);
        pods.Add(new Amphipod(false, false, loc, type));
        podsPart2.Add(new Amphipod(false, false, loc2, type));

        //  #D#C#B#A#
        //  #D#B#A#C#
        podsPart2.Add(new Amphipod(false, false, new Location(2, 2), PodType.Dessert));
        podsPart2.Add(new Amphipod(false, false, new Location(2, 3), PodType.Dessert));

        podsPart2.Add(new Amphipod(false, false, new Location(4, 2), PodType.Copper));
        podsPart2.Add(new Amphipod(false, false, new Location(4, 3), PodType.Bronze));

        podsPart2.Add(new Amphipod(false, false, new Location(6, 2), PodType.Bronze));
        podsPart2.Add(new Amphipod(false, false, new Location(6, 3), PodType.Amber));

        podsPart2.Add(new Amphipod(false, false, new Location(8, 2), PodType.Amber));
        podsPart2.Add(new Amphipod(false, false, new Location(8, 3), PodType.Copper));

        return new Game[] { new Game(pods, 0), new Game(podsPart2, 0) };
    }

    public PodType PodTypeFromChar(char c) => c switch
    {
        'A' => PodType.Amber,
        'B' => PodType.Bronze,
        'C' => PodType.Copper,
        'D' => PodType.Dessert,
        _ => throw new NotImplementedException()
    };

    public int SolveGame(Game game)
    {
        var visited = new Dictionary<string, bool>();

        var games = new Dictionary<string, (Game game, string via)>();

        var queue = new PriorityQueue<Game, int>();

        queue.Enqueue(game, 0);

        var answer = int.MaxValue;

        do
        {
            game = queue.Dequeue();
            var moves = new List<Move>();
            foreach (var pod in game.Amphipods)
            {
                moves.AddRange(pod.FindMoves(game));
            }

            foreach (var move in moves)
            {
                var newGame = game.ApplyMove(move);

                if (newGame.IsSolved() && newGame.Score < answer)
                {
                    answer = newGame.Score;
                }

                games.TryAdd(newGame.ToString(), (newGame, game.ToString()));

                if (games[newGame.ToString()].game.Score > newGame.Score)
                {
                    games[game.ToString()] = (newGame, game.ToString());
                }

                if (!visited.ContainsKey(game.ToString()))
                {
                    queue.Enqueue(newGame, newGame.Score + move.Score);
                }
            }

            visited.TryAdd(game.ToString(), true);
        } while (queue.Count > 0);

        return answer;
    //    if (game.Amphipods.All(p => p.Location.Col == (int)p.Type))
    //    {
    //        return 0; //solved game
    //    }

    //    var scores = new Dictionary<Game, (int Score, bool Checked)>();
    //    scores.Add(game, (0, false));
    //    var movesToTry = new Stack<(Move, Game, int)>();
    //    while (scores.Any(kv => kv.Value.Checked == false))
    //    {
    //        game = scores.First(kv => kv.Value.Checked == false).Key;

    //        var score = scores[game].Score;

    //        var moves = new List<Move>();
    //        foreach (var pod in game.Amphipods)
    //        {
    //            moves.AddRange(pod.FindMoves(game));
    //        }

    //        foreach (var move in moves)
    //        {
    //            var newGame = game.ApplyMove(move);
    //            scores.TryAdd(newGame, (int.MaxValue, false));
    //            if (scores[newGame].Score > score + move.Score)
    //            {
    //                scores[newGame] = (score + move.Score, false);
    //            }
    //        }

    //        scores[game] = (scores[game].Score, true);
    //    }

    //    return scores.Where(g => g.Key.Amphipods.All(p => p.Location.Col == (int)p.Type))
    //        .MinBy(kv => kv.Value.Score).Value.Score;
    }
}

public record Game(List<Amphipod> Amphipods, int Score)
{
    public Game ApplyMove(Move move)
    {
        var pods = new List<Amphipod>(Amphipods);

        var p = pods.Find(p => p.Location == move.From);
        pods.Remove(p);
        pods.Add(p with
        {
            Location = move.To,
            MovedToHall = true,
            MovedToCave = move.To.Row > 0
        });

        var newGame = this with { Amphipods = pods, Score = Score + move.Score };
        return newGame;
    }

    public bool IsSolved() => Amphipods.All(p => p.Location.Col == (int)p.Type);

    public override string ToString()
    {
        var ret = "";
        foreach(var pod in Amphipods.OrderBy(p => p.Location.Row).ThenBy(p => p.Location.Col))
        {
            ret += $"({pod.Type},{pod.Location.Row},{pod.Location.Col})";
        }

        return ret;
    }
}

public record Amphipod(bool MovedToHall, bool MovedToCave, Location Location, PodType Type)
{
    public IEnumerable<Move> FindMoves(Game game)
    {
        var moves = new List<Move>();
        if (Location.Col != (int)Type || game.Amphipods.Any(p => p.Location.Col == (int)Type && p.Type != Type)) // and cave is empty from others
        {
            if (!MovedToHall && !game.Amphipods.Any(p => p.Location.Row < Location.Row && p.Location.Col == Location.Col))
            {
                //try moving left
                for (var i = Location.Col; i >= 0; i--)
                {
                    if (game.Amphipods.Any(p => p.Location.Col == i && p.Location.Row == 0)) // taken
                    {
                        i = -1;
                    }
                    else if (!Day23.Caves.Contains(i))
                    {
                        moves.Add(new Move(Location, new Location(i, 0), Type));
                    }
                }
                //try moving Right
                for (var i = Location.Col; i <= 10; i++)
                {
                    if (game.Amphipods.Any(p => p.Location.Col == i && p.Location.Row == 0)) // taken
                    {
                        i = 11;
                    }
                    else if (!Day23.Caves.Contains(i))
                    {
                        moves.Add(new Move(Location, new Location(i, 0), Type));
                    }
                }
            }
            else if (!MovedToCave && MovedToHall)
            {
                if (Location.Col > (int)Type &&  //move to left
                    !game.Amphipods.Any(p => p.Location.Row == 0 && p.Location.Col.Between((int)Type, Location.Col - 1)) && //and the way is free
                    !game.Amphipods.Any(p => p.Location.Col == (int)Type && p.Type != Type)) // and cave is empty from others
                {
                    var depth = game.Amphipods.Where(p => p.Type == Type && p.Location.Col != (int)p.Type).Count();
                    moves.Add(new Move(Location, new Location((int)Type, depth), Type));
                }
                if (Location.Col < (int)Type &&  //move to left
                    !game.Amphipods.Any(p => p.Location.Row == 0 && p.Location.Col.Between(Location.Col + 1, (int)Type)) && //and the way is free
                    !game.Amphipods.Any(p => p.Location.Col == (int)Type && p.Type != Type)) // and cave is empty from others
                {
                    //var depth = game.Amphipods.Any(p => p.Location.Col == (int)Type && p.Location.Row == 2) ? 1 : 2;
                    var depth = game.Amphipods.Where(p => p.Type == Type && p.Location.Col != (int)p.Type).Count();
                    moves.Add(new Move(Location, new Location((int)Type, depth), Type));
                }
            }
        }

        return moves;
    }
}

public record Location (int Col, int Row);

public record Move(Location From, Location To, PodType Type)
{
    public int Score
    {
        get
        {
            var score = Math.Abs(From.Col - To.Col);
            score += Math.Abs(From.Row - To.Row);
            return Type switch
            {
                PodType.Dessert => score * 1000,
                PodType.Amber => score,
                PodType.Copper => score * 100,
                PodType.Bronze => score * 10,
                _ => throw new NotSupportedException()
            };
        }
    }
};

public enum PodType
{
    Amber = 2,
    Bronze = 4,
    Copper = 6,
    Dessert = 8,
}