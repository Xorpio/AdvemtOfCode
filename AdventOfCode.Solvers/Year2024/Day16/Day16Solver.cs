
using System.Runtime.CompilerServices;
using System.Security;

namespace AdventOfCode.Solvers.Year2024.Day16;

public class Day16Solver : BaseSolver
{
    public static (int row, int col)[] directions =
    [
        (-1, 0), // North
        (0, 1), // East
        (1, 0), // South
        (0, -1), // West
    ];

    public override void Solve(string[] puzzle)
    {
        (int row, int col) start = (0, 0);
        (int row, int col) end = (0, 0);

        for (int row = 0; row < puzzle.Length; row++)
        {
            for (int col = 0; col < puzzle[row].Length; col++)
            {
                if (puzzle[row][col] == 'S')
                {
                    start = (row, col);
                }
                else if (puzzle[row][col] == 'E')
                {
                    end = (row, col);
                }
            }
        }

        var path = Dijkstra(puzzle, start, end, Direction.East);

        GiveAnswer1(path.Max(p => p.score));
        for (int row = 0; row < puzzle.Length; row++)
        {
            var line = "";
            for (int col = 0; col < puzzle[row].Length; col++)
            {
                if (path.Any(p => p.row == row && p.col == col))
                {
                    line += path.First(p => p.row == row && p.col == col).dir switch
                    {
                        Direction.North => "^",
                        Direction.East => ">",
                        Direction.South => "v",
                        Direction.West => "<",
                        _ => throw new Exception("Invalid direction")
                    };
                }
                else
                {
                    line += puzzle[row][col];
                }
            }
            logger.OnNext(line);
        }

        GiveAnswer2("?");
    }

    public List<(int row, int col, Direction dir, int score)> Dijkstra(string[] puzzle, (int row, int col) start, (int row, int col) end, Direction dir)
    {
        var distances = new Dictionary<(int row, int col, Direction dir), int>();

        for (int row = 0; row < puzzle.Length; row++)
        {
            for (int col = 0; col < puzzle[row].Length; col++)
            {
                if (puzzle[row][col] == '#')
                    continue;

                if (puzzle[row + 1][col] != '#')
                    distances.Add((row, col, Direction.North), int.MaxValue);
                if (puzzle[row][col - 1] != '#')
                    distances.Add((row, col, Direction.East), int.MaxValue);
                if (puzzle[row - 1][col] != '#')
                    distances.Add((row, col, Direction.South), int.MaxValue);
                if (puzzle[row][col + 1] != '#')
                    distances.Add((row, col, Direction.West), int.MaxValue);
            }
        }
        distances[(start.row, start.col, dir)] = 0;

        var priorityQueue = new SortedSet<(int score, (int row, int col, Direction dir) node)>
        {
            (0, (start.row, start.col, Direction.East))
        };

        var previous = new Dictionary<(int row, int col, Direction dir), (int row, int col, Direction dir)>();
        while (priorityQueue.Count > 0)
        {
            var current = priorityQueue.Min;
            priorityQueue.Remove(current);

            if (current.node.row == end.row && current.node.col == end.col)
            {
                return ReconstructPath(previous, (end.row, end.col, Direction.East));
            }

            foreach (var direction in directions)
            {
                var newRow = current.node.row + direction.row;
                var newCol = current.node.col + direction.col;

                if (puzzle[newRow][newCol] == '#')
                    continue;

                var newDirection = direction switch
                {
                    (-1, 0) => Direction.North,
                    (0, 1) => Direction.East,
                    (1, 0) => Direction.South,
                    (0, -1) => Direction.West,
                    _ => throw new Exception("Invalid direction")
                };

                var newScore = current.score;
                switch (current.node.dir)
                {
                    case Direction.North:
                        if (direction == directions[0])
                            newScore += 1;
                        else
                            newScore += 1001;
                        break;
                    case Direction.East:
                        if (direction == directions[1])
                            newScore += 1;
                        else
                            newScore += 1001;
                        break;
                    case Direction.South:
                        if (direction == directions[2])
                            newScore += 1;
                        else
                            newScore += 1001;
                        break;
                    case Direction.West:
                        if (direction == directions[3])
                            newScore += 1;
                        else
                            newScore += 1001;
                        break;
                }

                if (distances[(newRow, newCol, newDirection)] > newScore)
                {
                    priorityQueue.Remove((distances[(newRow, newCol, newDirection)], (newRow, newCol, newDirection)));
                    distances[(newRow, newCol, newDirection)] = newScore;
                    priorityQueue.Add((newScore, (newRow, newCol, newDirection)));
                    previous[(newRow, newCol, newDirection)] = current.node;
                }
            }
        }

        return new();
    }

    static List<(int row, int col, Direction dir, int score)> ReconstructPath(Dictionary<(int row, int col, Direction dir), (int row, int col, Direction dir)> previous, (int row, int col, Direction dir) end)
    {
        var score = 0;
        var path = new List<(int row, int col, Direction dir, int score)>();
        var current = previous.First(p => p.Key.row == end.row && p.Key.col == end.col).Key;
        while (previous.ContainsKey(current))
        {
            path.Add((current.row, current.col, current.dir, score));
            current = previous[current];
            if (current.dir != path.Last().dir)
                score += 1001;
            else
                score += 1;
        }
        path.Add((current.row, current.col, current.dir, score));
        path.Reverse();
        path = path.Select(p => (p.row, p.col, p.dir, score - p.score)).ToList();
        return path;
    }

}

public enum Direction
{
    North,
    East,
    South,
    West
}
