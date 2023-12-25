using System.Collections.Specialized;
using System.Diagnostics;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Xml;

namespace AdventOfCode.Solvers.Year2023.Day17;

public class Day17Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        Dictionary<(Point point, string dir), (Point p, string d, int h)[]> graph = [];
        var intPuzzle = puzzle.Select(l => l.ToArray().Select(c => int.Parse(c.ToString())).ToArray()).ToArray();
        for (var row = 0; row < puzzle.Length; row++)
        {
            var line = intPuzzle[row];
            for (var col = 0; col < line.Length; col++)
            {
                for (int dir = 0; dir < 4; dir++)
                {
                    var wind = dir switch
                    {
                        0 => "NE",
                        1 => "NW",
                        2 => "SE",
                        3 => "SW",
                        _ => throw new Exception()
                    };

                    var neighbours = new List<(Point p, string d, int h)>();

                    int h = 0;
                    if (wind == "NE" || wind == "SE")
                    {
                        h = 0;
                        for(int i = 1; i < 4; i++)
                        {
                            //go east
                            if (col + i < line.Length)
                            {
                                h += line[col + i];
                                neighbours.Add((new Point(row, col + i), "NW", h));
                                neighbours.Add((new Point(row, col + i), "SW", h));
                            }
                        }
                    }
                    if (wind == "SE" || wind == "SW")
                    {
                        //go south
                        h = 0;
                        for(int i = 1; i < 4; i++)
                        {
                            if (row + i < intPuzzle.Length)
                            {
                                h += intPuzzle[row + i][col];
                                neighbours.Add((new Point(row + i, col), "NE", h));
                                neighbours.Add((new Point(row + i, col), "NW", h));
                            }
                        }
                    }
                    if (wind == "NW" || wind == "SW")
                    {
                        //go west
                        h = 0;
                        for(int i = 1; i < 4; i++)
                        {
                            if (col - i >= 0)
                            {
                                h += line[col - i];
                                neighbours.Add((new Point(row, col - i), "NE", h));
                                neighbours.Add((new Point(row, col - i), "SE", h));
                            }
                        }
                    }
                    if (wind == "NE" || wind == "NW")
                    {
                        //go north
                        h = 0;
                        for(int i = 1; i < 4; i++)
                        {
                            if (row - i >= 0)
                            {
                                h += intPuzzle[row - i][col];
                                neighbours.Add((new Point(row - i, col), "SE", h));
                                neighbours.Add((new Point(row - i, col), "SW", h));
                            }
                        }
                    }

                    graph.Add((new Point(row, col), wind), neighbours.ToArray());
                }
            }
        }

        var start = new Point(0, 0);
        var end = new Point(puzzle.Length - 1, puzzle[0].Length - 1);

        PriorityQueue<(Point point, string dir), int> queue = new();
        queue.Enqueue((start, "SE"), start + end);
        Dictionary<(Point point, string dir), (Point p, string dir)> visited = new();
        Dictionary<(Point point, string dir), int> score = new()
        {
            { (start, "SE"), 0 }
        };

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (current.point == end)
            {
                break;
            }

            foreach(var node in graph[current])
            {
                // var newPath = visited[current].path;

                // if (current.row > node.p.row)
                // {
                //     newPath += "N";
                // }
                // else if (current.row < node.p.row)
                // {
                //     newPath += "S";
                // }
                // else if (current.col > node.p.col)
                // {
                //     newPath += "W";
                // }
                // else if (current.col < node.p.col)
                // {
                //     newPath += "E";
                // }

                // if (newPath[1..] == "NNN" || newPath[1..] == "SSS" || newPath[1..] == "EEE" || newPath[1..] == "WWW")
                // {
                //     continue;
                // }

                var newHeat = score[current] + node.h;

                if (!score.ContainsKey((node.p, node.d)) || newHeat < score[(node.p, node.d)])
                {
                    score[(node.p, node.d)] = newHeat;
                    queue.Enqueue((node.p, node.d), newHeat + (node.p + end));
                    visited[(node.p, node.d)] = current;
                }
            }
        }

        var answer = score.First(p => p.Key.point == end).Value;

        List<(Point pf, string df, Point pt, string dt)> path = new();
        var v = score.First(p => p.Key.point == end).Key;
        while (true)
        {
            if (v.point == start)
            {
                break;
            }

            logger.OnNext($"from {visited[v]} to {v}");

            path.Add((visited[v].p, visited[v].dir, v.point, v.dir));   

            v = visited[v];
        }

        var charPuzzle = puzzle.Select(l => l.ToArray()).ToArray();
        foreach(var p in path)
        {
            if (p.pf.row > p.pt.row)
            {
                for(int row = p.pf.row; row > p.pt.row; row--)
                {
                    charPuzzle[row][p.pf.col] = '^';
                }
            }
            if (p.pf.row < p.pt.row)
            {
                for(int row = p.pf.row; row < p.pt.row; row++)
                {
                    charPuzzle[row][p.pf.col] = 'v';
                }
            }
            if (p.pf.col > p.pt.col)
            {
                for(int col = p.pf.col; col > p.pt.col; col--)
                {
                    charPuzzle[p.pf.row][col] = '<';
                }
            }
            if (p.pf.col < p.pt.col)
            {
                for(int col = p.pf.col; col < p.pt.col; col++)
                {
                    charPuzzle[p.pf.row][col] = '>';
                }
            }
        }

        foreach(var line in charPuzzle)
        {
            logger.OnNext(new string(line));
        }

        GiveAnswer1(answer);

        GiveAnswer2("");
    }
}

public record Point(int row, int col)
{
    public static int operator +(Point a, Point b) => Math.Abs(a.col - b.col) + Math.Abs(a.row - b.row);
}
