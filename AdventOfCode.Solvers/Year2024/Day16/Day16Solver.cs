
using System.Security;

namespace AdventOfCode.Solvers.Year2024.Day16;

public class Day16Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        (int startrow, int startcol, int endrow, int endcol) = (0, 0, 0, 0);

        for (int row = 0; row < puzzle.Length; row++)
        {
            for (int col = 0; col < puzzle[row].Length; col++)
            {
                if (puzzle[row][col] == 'S')
                {
                    startrow = row;
                    startcol = col;
                }
                else if (puzzle[row][col] == 'E')
                {
                    endrow = row;
                    endcol = col;
                }
            }
        }

        var score = dijkstra(puzzle, startrow, startcol, endrow, endcol);

        GiveAnswer1(score);
        GiveAnswer2("");
    }
    private decimal dijkstra(string[] puzzle, int startrow, int startcol, int endrow, int endcol)
    {
        var unvisited = new Dictionary<(int row, int col, Direction dir), decimal>();
        var Visited = new Dictionary<(int row, int col, Direction dir), decimal>();

        //initialize the nodes
        for (int row = 0; row < puzzle.Length; row++)
        {
            for (int col = 0; col < puzzle[row].Length; col++)
            {
                if (puzzle[row][col] == '#')
                    continue;

                unvisited.Add((row, col, Direction.North), decimal.MaxValue);
                unvisited.Add((row, col, Direction.East), decimal.MaxValue);
                unvisited.Add((row, col, Direction.South), decimal.MaxValue);
                unvisited.Add((row, col, Direction.West), decimal.MaxValue);
            }
        }

        unvisited[(startrow, startcol, Direction.East)] = 0;

        while (unvisited.Count > 0)
        {
            var next = unvisited.OrderBy(x => x.Value).First();

            if (next.Key.row == endrow && next.Key.col == endcol)
            {
                return next.Value;
            }

            unvisited.Remove(next.Key);
            Visited.Add(next.Key, next.Value);

            switch (next.Key.dir)
            {
                case Direction.North:
                    if (unvisited.ContainsKey((next.Key.row - 1, next.Key.col, next.Key.dir)) && unvisited[(next.Key.row - 1, next.Key.col, next.Key.dir)] > next.Value + 1)
                    {
                        unvisited[(next.Key.row - 1, next.Key.col, next.Key.dir)] = next.Value + 1;
                    }
                    if (unvisited.ContainsKey((next.Key.row, next.Key.col, Direction.West)) && unvisited[(next.Key.row, next.Key.col, Direction.West)] > next.Value + 1000)
                    {
                        unvisited[(next.Key.row, next.Key.col, Direction.West)] = next.Value + 1000;
                    }
                    if (unvisited.ContainsKey((next.Key.row, next.Key.col, Direction.East)) && unvisited[(next.Key.row, next.Key.col, Direction.East)] > next.Value + 1000)
                    {
                        unvisited[(next.Key.row, next.Key.col, Direction.East)] = next.Value + 1000;
                    }
                    if (unvisited.ContainsKey((next.Key.row, next.Key.col, Direction.South)) && unvisited[(next.Key.row, next.Key.col, Direction.South)] > next.Value + 2000)
                    {
                        unvisited[(next.Key.row, next.Key.col, Direction.South)] = next.Value + 2000;
                    }
                    break;
                case Direction.East:
                    if (unvisited.ContainsKey((next.Key.row, next.Key.col + 1, next.Key.dir)) && unvisited[(next.Key.row, next.Key.col + 1, next.Key.dir)] > next.Value + 1)
                    {
                        unvisited[(next.Key.row, next.Key.col + 1, next.Key.dir)] = next.Value + 1;
                    }
                    if (unvisited.ContainsKey((next.Key.row, next.Key.col, Direction.North)) && unvisited[(next.Key.row, next.Key.col, Direction.North)] > next.Value + 1000)
                    {
                        unvisited[(next.Key.row, next.Key.col, Direction.North)] = next.Value + 1000;
                    }
                    if (unvisited.ContainsKey((next.Key.row, next.Key.col, Direction.South)) && unvisited[(next.Key.row, next.Key.col, Direction.South)] > next.Value + 1000)
                    {
                        unvisited[(next.Key.row, next.Key.col, Direction.South)] = next.Value + 1000;
                    }
                    if (unvisited.ContainsKey((next.Key.row, next.Key.col, Direction.West)) && unvisited[(next.Key.row, next.Key.col, Direction.West)] > next.Value + 2000)
                    {
                        unvisited[(next.Key.row, next.Key.col, Direction.West)] = next.Value + 2000;
                    }
                    break;
                case Direction.South:
                    if (unvisited.ContainsKey((next.Key.row + 1, next.Key.col, next.Key.dir)) && unvisited[(next.Key.row + 1, next.Key.col, next.Key.dir)] > next.Value + 1)
                    {
                        unvisited[(next.Key.row + 1, next.Key.col, next.Key.dir)] = next.Value + 1;
                    }
                    if (unvisited.ContainsKey((next.Key.row, next.Key.col, Direction.West)) && unvisited[(next.Key.row, next.Key.col, Direction.West)] > next.Value + 1000)
                    {
                        unvisited[(next.Key.row, next.Key.col, Direction.West)] = next.Value + 1000;
                    }
                    if (unvisited.ContainsKey((next.Key.row, next.Key.col, Direction.East)) && unvisited[(next.Key.row, next.Key.col, Direction.East)] > next.Value + 1000)
                    {
                        unvisited[(next.Key.row, next.Key.col, Direction.East)] = next.Value + 1000;
                    }
                    if (unvisited.ContainsKey((next.Key.row, next.Key.col, Direction.North)) && unvisited[(next.Key.row, next.Key.col, Direction.North)] > next.Value + 2000)
                    {
                        unvisited[(next.Key.row, next.Key.col, Direction.North)] = next.Value + 2000;
                    }
                    break;
                case Direction.West:
                    if (unvisited.ContainsKey((next.Key.row, next.Key.col - 1, next.Key.dir)) && unvisited[(next.Key.row, next.Key.col - 1, next.Key.dir)] > next.Value + 1)
                    {
                        unvisited[(next.Key.row, next.Key.col - 1, next.Key.dir)] = next.Value + 1;
                    }
                    if (unvisited.ContainsKey((next.Key.row, next.Key.col, Direction.North)) && unvisited[(next.Key.row, next.Key.col, Direction.North)] > next.Value + 1000)
                    {
                        unvisited[(next.Key.row, next.Key.col, Direction.North)] = next.Value + 1000;
                    }
                    if (unvisited.ContainsKey((next.Key.row, next.Key.col, Direction.South)) && unvisited[(next.Key.row, next.Key.col, Direction.South)] > next.Value + 1000)
                    {
                        unvisited[(next.Key.row, next.Key.col, Direction.South)] = next.Value + 1000;
                    }
                    if (unvisited.ContainsKey((next.Key.row, next.Key.col, Direction.East)) && unvisited[(next.Key.row, next.Key.col, Direction.East)] > next.Value + 2000)
                    {
                        unvisited[(next.Key.row, next.Key.col, Direction.East)] = next.Value + 2000;
                    }
                    break;
            }
        }

        return 0;
    }

    private decimal dijkstra2(string[] puzzle, int startrow, int startcol, int endrow, int endcol)
    {
        var unvisited = new PriorityQueue<(int row, int col, Direction dir, int score), int>();
        var visited = new Dictionary<(int row, int col, Direction dir), int>();

        unvisited.Enqueue((startrow, startcol, Direction.East, 0), 0);

        while (unvisited.Count > 0)
        {
            var next = unvisited.Dequeue();
            if (visited.ContainsKey((next.row, next.col, next.dir)))
            {
                continue;
            }

            visited.Add((next.row, next.col, next.dir), next.score);

            if (next.row == endrow && next.col == endcol)
            {
                return next.score;
            }

            var northScore = visited[(next.row, next.col, next.dir)] + (next.dir == Direction.South
                ? 2000
                : next.dir == Direction.North
                    ? 0
                    : 1000);

            var eastScore = visited[(next.row, next.col, next.dir)] + (next.dir == Direction.West
                ? 2000
                : next.dir == Direction.East
                    ? 0
                    : 1000);

            var southScore = visited[(next.row, next.col, next.dir)] + (next.dir == Direction.North
                ? 2000
                : next.dir == Direction.South
                    ? 0
                    : 1000);

            var westScore = visited[(next.row, next.col, next.dir)] + (next.dir == Direction.East
                ? 2000
                : next.dir == Direction.West
                    ? 0
                    : 1000);

            //add turns
            if (!visited.ContainsKey((next.row, next.col, Direction.North)))
            {
                unvisited.Enqueue((next.row, next.col, Direction.North, next.score + northScore), (next.score + northScore) * 1);
            }
            if (!visited.ContainsKey((next.row, next.col, Direction.East)))
            {
                unvisited.Enqueue((next.row, next.col, Direction.East, next.score + eastScore), (next.score + eastScore) * 1);
            }
            if (!visited.ContainsKey((next.row, next.col, Direction.South)))
            {
                unvisited.Enqueue((next.row, next.col, Direction.South, next.score + southScore), (next.score + southScore) * 1);
            }
            if (!visited.ContainsKey((next.row, next.col, Direction.West)))
            {
                unvisited.Enqueue((next.row, next.col, Direction.West, next.score + westScore), (next.score + westScore) * 1);
            }

            //move forward
            switch (next.dir)
            {
                case Direction.North:
                    if (next.row > 0 && puzzle[next.row - 1][next.col] != '#' && !visited.ContainsKey((next.row - 1, next.col, next.dir)))
                    {
                        unvisited.Enqueue((next.row - 1, next.col, next.dir, next.score + 1), (next.score + 1) * 1);
                    }
                    break;
                case Direction.East:
                    if (next.col < puzzle[next.row].Length - 1 && puzzle[next.row][next.col + 1] != '#' && !visited.ContainsKey((next.row, next.col + 1, next.dir)))
                    {
                        unvisited.Enqueue((next.row, next.col + 1, next.dir, next.score + 1), (next.score + 1) * 1);
                    }
                    break;
                case Direction.South:
                    if (next.row < puzzle.Length - 1 && puzzle[next.row + 1][next.col] != '#' && !visited.ContainsKey((next.row + 1, next.col, next.dir)))
                    {
                        unvisited.Enqueue((next.row + 1, next.col, next.dir, next.score + 1), (next.score + 1) * 1);
                    }
                    break;
                case Direction.West:
                    if (next.col > 0 && puzzle[next.row][next.col - 1] != '#' && !visited.ContainsKey((next.row, next.col - 1, next.dir)))
                    {
                        unvisited.Enqueue((next.row, next.col - 1, next.dir, next.score + 1), (next.score + 1) * 1);
                    }
                    break;
            }
        }

        return decimal.MaxValue;
    }
}

public enum Direction
{
    North,
    East,
    South,
    West
}
