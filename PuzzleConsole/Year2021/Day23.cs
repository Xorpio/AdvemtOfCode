using PuzzleConsole.Year2021.Day22;

namespace PuzzleConsole.Year2021.Day23;

public class Day23 : ISolver
{
    public static readonly int[] Caves = { 2,4,6,8 };

    public string[] Solve(string[] puzzle)
    {
        var game = PuzzleToGame(puzzle);

        return new string[] { SolveGame(game, new List<Move>()).ToString() };
    }

    public Game PuzzleToGame(string[] puzzle)
    {
        List<Amphipod> pods = new List<Amphipod>();

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

        col += 2;
        loc = new Location(col - 1, 1);
        type = PodTypeFromChar(line[col]);
        pods.Add(new Amphipod(false, false, loc, type));

        col += 2;
        loc = new Location(col - 1, 1);
        type = PodTypeFromChar(line[col]);
        pods.Add(new Amphipod(false, false, loc, type));

        col += 2;
        loc = new Location(col - 1, 1);
        type = PodTypeFromChar(line[col]);
        pods.Add(new Amphipod(false, false, loc, type));

        line = puzzle[3];
        col = 3;
        loc = new Location(col - 1, 2);
        type = PodTypeFromChar(line[col]);
        pods.Add(new Amphipod(false, false, loc, type));

        col += 2;
        loc = new Location(col - 1, 2);
        type = PodTypeFromChar(line[col]);
        pods.Add(new Amphipod(false, false, loc, type));

        col += 2;
        loc = new Location(col - 1, 2);
        type = PodTypeFromChar(line[col]);
        pods.Add(new Amphipod(false, false, loc, type));

        col += 2;
        loc = new Location(col - 1, 2);
        type = PodTypeFromChar(line[col]);
        pods.Add(new Amphipod(false, false, loc, type));

        return new Game(pods, 0);
    }

    public PodType PodTypeFromChar(char c) => c switch
    {
        'A' => PodType.Amber,
        'B' => PodType.Bronze,
        'C' => PodType.Copper,
        'D' => PodType.Dessert,
        _ => throw new NotImplementedException()
    };

    public int SolveGame(Game game, List<Move> moves)
    {
        if (game.Amphipods.All(p => p.MovedToCave || p.Location.Col == (int) p.Type))
        {
            return moves.Sum(m => m.Score);
        }

        var possibleMoves = new List<Move>();
        //vind alle mogelijke acties
        foreach (var pod in game.Amphipods)
        {
            possibleMoves.AddRange(pod.FindMoves(game));
        }

        var score = int.MaxValue;

        foreach (var move in  possibleMoves)
        {
            var pods = new List<Amphipod>(game.Amphipods);

            var p = pods.Find(p => p.Location == move.From);
            pods.Remove(p);
            pods.Add(p with
            {
                Location = move.To,
                MovedToHall = true,
                MovedToCave = move.To.Row > 0
            });

            var m = new List<Move>(moves);
            m.Add(move);
            var newGame = game with { Amphipods = pods };

            var newScore = SolveGame(newGame, m);
            score = newScore < score ? newScore : score;
        }

        return score;
    }
}

public record Game (List<Amphipod> Amphipods, int Score);

public record Amphipod(bool MovedToHall, bool MovedToCave, Location Location, PodType Type)
{
    public IEnumerable<Move> FindMoves(Game game)
    {
        var moves = new List<Move>();
        if (Location.Col != (int)Type || game.Amphipods.Any(p => p.Location.Col == (int)Type && p.Type != Type)) // and cave is empty from others
        {
            if (!MovedToHall && (Location.Row == 1 || !game.Amphipods.Any(p => p.Location.Row == 1 && p.Location.Col == Location.Col)))
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
                    moves.Add(new Move(Location, new Location((int)Type, 1), Type));
                }
                if (Location.Col < (int)Type &&  //move to left
                    !game.Amphipods.Any(p => p.Location.Row == 0 && p.Location.Col.Between(Location.Col + 1, (int)Type)) && //and the way is free
                    !game.Amphipods.Any(p => p.Location.Col == (int)Type && p.Type != Type)) // and cave is empty from others
                {
                    moves.Add(new Move(Location, new Location((int)Type, 1), Type));
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
