
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

        (List<(int row, int col, Direction dir)> path, int score) = Dijkstra(puzzle, start, end, Direction.East);

        GiveAnswer1(score);
        GiveAnswer2(path.Distinct().Count() - 1);
        for (int row = 0; row < puzzle.Length; row++)
        {
            var line = "";
            for (int col = 0; col < puzzle[row].Length; col++)
            {
                if (path.Any(p => p.row == row && p.col == col))
                {
                    //line += path.First(p => p.row == row && p.col == col).dir switch
                    //{
                    //    Direction.North => "^",
                    //    Direction.East => ">",
                    //    Direction.South => "v",
                    //    Direction.West => "<",
                    //    _ => throw new Exception("Invalid direction")
                    //};
                    line += "O";
                }
                else
                {
                    line += puzzle[row][col];
                }
            }
            logger.OnNext(line);
        }

        //var queue = new Queue<(int row, int col, Direction dir, int score)>();
        //var winningPath = new HashSet<(int row, int col)>();

        //foreach(var p in path)
        //    winningPath.Add((p.row, p.col));

        //foreach(var p in path)
        //{
        //    foreach (var d in directions)
        //    {
        //        var newRow = p.row + d.row;
        //        var newCol = p.col + d.col;
        //        if (puzzle[newRow][newCol] == '#' || winningPath.Contains((newRow, newCol)))
        //            continue;

        //        var newDirection = d switch
        //        {
        //            (-1, 0) => Direction.North,
        //            (0, 1) => Direction.East,
        //            (1, 0) => Direction.South,
        //            (0, -1) => Direction.West,
        //            _ => throw new Exception("Invalid direction")
        //        };

        //        var newScore = 0;
        //        switch (p.dir)
        //        {
        //            case Direction.North:
        //                if (d == directions[0])
        //                    newScore += 1;
        //                else
        //                    newScore += 1001;
        //                break;
        //            case Direction.East:
        //                if (d == directions[1])
        //                    newScore += 1;
        //                else
        //                    newScore += 1001;
        //                break;
        //            case Direction.South:
        //                if (d == directions[2])
        //                    newScore += 1;
        //                else
        //                    newScore += 1001;
        //                break;
        //            case Direction.West:
        //                if (d == directions[3])
        //                    newScore += 1;
        //                else
        //                    newScore += 1001;
        //                break;
        //        }
        //        queue.Enqueue((newRow, newCol, newDirection, p.score + newScore));
        //    }
        //}

        //while (queue.Count > 0)
        //{
        //    logger.OnNext(queue.Count.ToString());
        //    var next = queue.Dequeue();

        //    var dijkstra = Dijkstra(puzzle, (next.row, next.col), end, next.dir, maxScore);
        //    if (dijkstra.Count == 0)
        //        continue;

        //    if (dijkstra.Last().score + next.score <= maxScore)
        //    {
        //        foreach(var p in dijkstra)
        //        {
        //            winningPath.Add((p.row, p.col));
        //            foreach (var d in directions)
        //            {
        //                var newRow = p.row + d.row;
        //                var newCol = p.col + d.col;
        //                if (puzzle[newRow][newCol] == '#' || winningPath.Contains((newRow, newCol)))
        //                    continue;

        //                var newDirection = d switch
        //                {
        //                    (-1, 0) => Direction.North,
        //                    (0, 1) => Direction.East,
        //                    (1, 0) => Direction.South,
        //                    (0, -1) => Direction.West,
        //                    _ => throw new Exception("Invalid direction")
        //                };

        //                var newScore = 0;
        //                switch (p.dir)
        //                {
        //                    case Direction.North:
        //                        if (d == directions[0])
        //                            newScore += 1;
        //                        else
        //                            newScore += 1001;
        //                        break;
        //                    case Direction.East:
        //                        if (d == directions[1])
        //                            newScore += 1;
        //                        else
        //                            newScore += 1001;
        //                        break;
        //                    case Direction.South:
        //                        if (d == directions[2])
        //                            newScore += 1;
        //                        else
        //                            newScore += 1001;
        //                        break;
        //                    case Direction.West:
        //                        if (d == directions[3])
        //                            newScore += 1;
        //                        else
        //                            newScore += 1001;
        //                        break;
        //                }
        //                queue.Enqueue((newRow, newCol, newDirection, p.score + newScore));
        //            }
        //        }
        //    }
        //}

        //for (int row = 0; row < puzzle.Length; row++)
        //{
        //    var line = "";
        //    for (int col = 0; col < puzzle[row].Length; col++)
        //    {
        //        if (winningPath.Contains((row, col)))
        //        {
        //            line += "0";
        //        }
        //        else
        //        {
        //            line += puzzle[row][col];
        //        }
        //    }
        //    logger.OnNext(line);
        //}

        //GiveAnswer2(winningPath.Count());
    }

    public (List<(int row, int col, Direction dir)>, int score) Dijkstra(string[] puzzle, (int row, int col) start, (int row, int col) end, Direction dir)
    {
        var distances = new Dictionary<(int row, int col, Direction dir), int>();
        var maxScore = int.MaxValue;

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

        var previous = new Dictionary<(int row, int col, Direction dir), List<(int row, int col, Direction dir)>>();
        while (priorityQueue.Count > 0)
        {
            var current = priorityQueue.Min;
            priorityQueue.Remove(current);

            if (current.node.row == end.row && current.node.col == end.col)
            {
                maxScore = current.score;
            }

            if (current.score > maxScore)
                continue;

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

                if (newScore > maxScore || current.score > maxScore)
                    continue;

                if (distances[(newRow, newCol, newDirection)] >= newScore)
                {
                    priorityQueue.Remove((distances[(newRow, newCol, newDirection)], (newRow, newCol, newDirection)));
                    distances[(newRow, newCol, newDirection)] = newScore;
                    priorityQueue.Add((newScore, (newRow, newCol, newDirection)));

                    if (!previous.ContainsKey((newRow, newCol, newDirection)))
                        previous[(newRow, newCol, newDirection)] = new List<(int row, int col, Direction dir)>();

                    previous[(newRow, newCol, newDirection)].Add(current.node);
                }
            }
        }

        return (ReconstructPath(previous, (end.row, end.col, dir)), maxScore);
    }

    static List<(int row, int col, Direction dir)> ReconstructPath(Dictionary<(int row, int col, Direction dir), List<(int row, int col, Direction dir)>> previous, (int row, int col, Direction dir) end)
    {
        var path = new List<(int row, int col, Direction dir)>();
        var current = previous.First(p => p.Key.row == end.row && p.Key.col == end.col).Key;
        var q = new Queue<(int row, int col, Direction dir)>();
        q.Enqueue(current);

        while (q.Count > 0)
        {
            var next = q.Dequeue();
            if (previous.ContainsKey(next))
            {
                foreach (var p in previous[next])
                {
                    q.Enqueue(p);
                    path.Add((p.row, p.col, p.dir));
                    current = p;
                }
            }
        }
        //while (previous.ContainsKey(current))
        //{
        //    path.Add((current.row, current.col, current.dir, score));
        //    current = previous[current];
        //    if (current.dir != path.Last().dir)
        //        score += 1001;
        //    else
        //        score += 1;
        //}
        //path.Add((current.row, current.col, current.dir));
        path.Reverse();
        //path = path.Select(p => (p.row, p.col, p.dir, score - p.score)).ToList();
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
