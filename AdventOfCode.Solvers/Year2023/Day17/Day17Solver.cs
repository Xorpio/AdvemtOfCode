using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Xml;

namespace AdventOfCode.Solvers.Year2023.Day17;

public class Day17Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        int[,] grid = new int[puzzle.Length, puzzle[0].Length];
        for (int row = 0; row < puzzle.Length; row++)
        {
            for (int col = 0; col < puzzle[0].Length; col++)
            {
                grid[row, col] = int.Parse(puzzle[row][col].ToString());
            }
        }

        PriorityQueue<(Point point, string dir), int> queue = new();
        List<(Point point, string dir)> visited = new();
        Dictionary<(Point point, string dir), (Point point, string dir, int heat)> history = new();

        var start = new Point(0, 0);
        var end = new Point(grid.GetLength(0) - 1, grid.GetLength(1) - 1);

        queue.Enqueue((start, "V"), 0);
        queue.Enqueue((start, ">"), 0);

        history[(start, "V")] = (start, "V", 0);
        history[(start, ">")] = (start, ">", 0);

        while(queue.Count > 0)
        {
            var (nextPoint, nextDir) = queue.Dequeue();
            var currentHeat = history[(nextPoint, nextDir)].heat;

            if (visited.Contains((nextPoint, nextDir)))
            {
                continue;
            }

            visited.Add((nextPoint, nextDir));

            if (nextPoint == end)
            {
                break;
            }

            if (nextDir == ">" || nextDir == "<") 
            {
                int heatUp = currentHeat;
                int heatDown = currentHeat;
                for (int i = 1; i < 4; i++)
                {
                    var up = nextPoint with { row = nextPoint.row - i };
                    if (up.row >= 0)
                    {
                        heatUp += grid[up.row, up.col];
                        if (!history.ContainsKey((up, "^")) || history[(up, "^")].heat > heatUp)
                        {
                            history[(up, "^")] = (nextPoint, nextDir, heatUp);
                            queue.Enqueue((up, "^"), heatUp);
                        }
                    }

                    var down = nextPoint with { row = nextPoint.row + i };
                    if (down.row < grid.GetLength(0))
                    {
                        heatDown += grid[down.row, down.col];
                        if (!history.ContainsKey((down, "V")) || history[(down, "V")].heat > heatDown)
                        {
                            history[(down, "V")] = (nextPoint, nextDir, heatDown);
                            queue.Enqueue((down, "V"), heatDown);
                        }
                    }
                }
            }

            if (nextDir == "^" || nextDir == "V")
            {
                int heatLeft = currentHeat;
                int heatRight = currentHeat;
                for (int i = 1; i < 4; i++)
                {
                    var left = nextPoint with { col = nextPoint.col - i };
                    if (left.col >= 0)
                    {
                        heatLeft += grid[left.row, left.col];
                        if (!history.ContainsKey((left, "<")) || history[(left, "<")].heat > heatLeft)
                        {
                            history[(left, "<")] = (nextPoint, nextDir, heatLeft);
                            queue.Enqueue((left, "<"), heatLeft);
                        }
                    }

                    var right = nextPoint with { col = nextPoint.col + i };
                    if (right.col < grid.GetLength(1))
                    {
                        heatRight += grid[right.row, right.col];
                        if (!history.ContainsKey((right, ">")) || history[(right, ">")].heat > heatRight)
                        {
                            history[(right, ">")] = (nextPoint, nextDir, heatRight);
                            queue.Enqueue((right, ">"), heatRight);
                        }
                    }
                }
            }
        }

        var charPuzzle = puzzle.Select(x => x.ToCharArray()).ToArray();

        var key = history.OrderBy(kv => kv.Value.heat).First(x => x.Key.point == end).Key;
        GiveAnswer1(history[key].heat);
        while(key.point != start)
        {
            logger.OnNext($"{key} {history[key]}");
            charPuzzle[history[key].point.row][history[key].point.col] = key.dir[0];
            key = (history[key].point, history[key].dir);
        }

        foreach(var row in charPuzzle)
        {
            logger.OnNext(new string(row));
        }

        GiveAnswer2(0);
    }
}

public record Point(int row, int col)
{
    public static int operator +(Point a, Point b) => Math.Abs(a.col - b.col) + Math.Abs(a.row - b.row);
}
