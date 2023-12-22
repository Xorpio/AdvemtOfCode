using System.Diagnostics;
using System.Runtime.Intrinsics.Arm;

namespace AdventOfCode.Solvers.Year2023.Day17;

public class Day17Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        Dictionary<Point, (Point p, int h)[]> graph = [];
        for (var row = 0; row < puzzle.Length; row++)
        {
            var line = puzzle[row];
            for (var col = 0; col < line.Length; col++)
            {
                var neighbours = new List<(Point p, int h)>();
                int i = 1;
                var h = 0;
                //go east
                if (col + i < line.Length)
                {
                    h += int.Parse(line[col + i].ToString());
                    neighbours.Add((new Point(row, col + i), h));
                }
                //go south
                h = 0;
                    if (row + i < puzzle.Length)
                    {
                        h += int.Parse(puzzle[row + i][col].ToString());
                        neighbours.Add((new Point(row + i, col), h));
                    }
                //go west
                h = 0;
                    if (col - i >= 0)
                    {
                        h += int.Parse(line[col - i].ToString());
                        neighbours.Add((new Point(row, col - i), h));
                    }
                //go north
                h = 0;
                    if (row - i >= 0)
                    {
                        h += int.Parse(puzzle[row - i][col].ToString());
                        neighbours.Add((new Point(row - i, col), h));
                    }

                graph.Add(new Point(row, col), neighbours.ToArray());
            }
        }

        var start = new Point(0, 0);
        var end = new Point(puzzle.Length - 1, puzzle[0].Length - 1);

        PriorityQueue<Point, int> queue = new();
        queue.Enqueue(start, 0);
        Dictionary<Point, (Point? p, string path)> visited = new();
        visited.Add(start, (null, "---"));
        Dictionary<(Point point, string path), int> score = new();
        score.Add((start, "---"), 0);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (current == end)
            {
                break;
            }

            foreach(var node in graph[current])
            {
                var newPath = visited[current].path;

                if (current.row > node.p.row)
                {
                    newPath += "N";
                }
                else if (current.row < node.p.row)
                {
                    newPath += "S";
                }
                else if (current.col > node.p.col)
                {
                    newPath += "W";
                }
                else if (current.col < node.p.col)
                {
                    newPath += "E";
                }

                if (newPath[1..] == "NNN" || newPath[1..] == "SSS" || newPath[1..] == "EEE" || newPath[1..] == "WWW")
                {
                    continue;
                }

                var newHeat = score[(current, visited[current].path)] + node.h;

                if (!score.ContainsKey((node.p, newPath)) || newHeat < score[(node.p, newPath)])
                {
                    score[(node.p, newPath[1..])] = newHeat;
                    queue.Enqueue(node.p, newHeat + (node.p + end));
                    visited[node.p] = (current, newPath[1..]);
                }
            }
        }

        var answer = score.First(p => p.Key.point == end).Value;

        var v = end;
        while (true)
        {
            if (v == start)
            {
                break;
            }

            logger.OnNext($"from {visited[v].p} to {v}");
            v = visited.First(kv => kv.Key == v).Value.p;
        }

        GiveAnswer1(answer);

        GiveAnswer2("");
    }
}

public record Point(int row, int col)
{
    public static int operator +(Point a, Point b) => Math.Abs(a.col - b.col) + Math.Abs(a.row - b.row);
}
