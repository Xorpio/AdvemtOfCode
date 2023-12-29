using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode.Solvers.Year2023.Day21;

public class Day21Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        var evenPoints = 0;
        var oddPoints = 0;
        Point start = new Point(0, 0);
        char[,] grid = new char[puzzle.Length, puzzle[0].Length];
        for (int row = 0; row < puzzle.Length; row++)
        {
            for (int col = 0; col < puzzle[0].Length; col++)
            {
                grid[row, col] = puzzle[row][col];
                if (puzzle[row][col] == 'S')
                {
                    start = new Point(row, col);
                    if ((row + col) % 2 == 0)
                    {
                        logger.OnNext("Start is even");
                    }
                    else{
                        logger.OnNext("Start is odd");}
                }

                if ((row + col) % 2 == 0 && puzzle[row][col] != '#')
                {
                    evenPoints++;
                }
                else
                {
                    oddPoints++;
                }
            }
        }

            logger.OnNext($"even: {evenPoints} odd: {oddPoints}");

        int maxRow = grid.GetLength(0);
        int maxCol = grid.GetLength(1);

        HashSet<(Point point, int steps)> queue = new();

        queue.Add((start, 0));

        double steps = puzzle.Length > 20 ? 64 : 6;
        List<double> stepsList = new();

        for (int i = 0; i < steps; i++)
        {
            stepsList.Add(queue.Count());
            while(queue.Any(s => s.steps == i))
            {
                var next = queue.First(s => s.steps == i);
                queue.Remove(next);

                var (row, col) = next.point;
                if (row > 0 && grid[row - 1, col] != '#')
                {
                    queue.Add((new Point(row - 1, col), i + 1));
                }
                if (row < maxRow - 1 && grid[row + 1, col] != '#')
                {
                    queue.Add((new Point(row + 1, col), i + 1));
                }
                if (col > 0 && grid[row, col - 1] != '#')
                {
                    queue.Add((new Point(row, col - 1), i + 1));
                }
                if (col < maxCol - 1 && grid[row, col + 1] != '#')
                {
                    queue.Add((new Point(row, col + 1), i + 1));
                }
            }
        }

        // steps = steps == 6 ? 100 : 26501365;

        GiveAnswer1(queue.Count);

        HashSet<(Point point, int steps, Point dimension)> queue2 = new();
        queue2.Add((start, 0, new Point(0, 0)));

        steps = puzzle.Length > 20 ? 500 : 500;

        stepsList.Clear();

        Dictionary<Point, int> first = [];
        for (int i = 0; i <= steps; i++)
        {
            stepsList.Add(queue2.Count());
            while(queue2.Any(s => s.steps == i))
            {
                var next = queue2.First(s => s.steps == i);
                queue2.Remove(next);

                var (row, col) = next.point;
                if (row > 0 && grid[row - 1, col] != '#')
                {
                    queue2.Add((new Point(row - 1, col), i + 1, next.dimension));
                }
                if (row < maxRow - 1 && grid[row + 1, col] != '#')
                {
                    queue2.Add((new Point(row + 1, col), i + 1, next.dimension));
                }
                if (col > 0 && grid[row, col - 1] != '#')
                {
                    queue2.Add((new Point(row, col - 1), i + 1, next.dimension));
                }
                if (col < maxCol - 1 && grid[row, col + 1] != '#')
                {
                    queue2.Add((new Point(row, col + 1), i + 1, next.dimension));
                }

                if (row == 0)
                {
                    queue2.Add((new Point(maxRow - 1, col), i + 1, next.dimension with { row = next.dimension.row - 1 }));
                }
                if (row == maxRow - 1)
                {
                    queue2.Add((new Point(0, col), i + 1, next.dimension with { row = next.dimension.row + 1 }));
                }
                if (col == 0)
                {
                    queue2.Add((new Point(row, maxCol -1), i + 1, next.dimension with { col = next.dimension.col - 1 }));
                }
                if (col == maxCol - 1)
                {
                    queue2.Add((new Point(row, 0), i + 1, next.dimension with { col = next.dimension.col + 1 }));
                }
            }
            //
            // logger.OnNext($"After {i} steps");
            // foreach (var d in queue2.Select(l => l.dimension).Distinct())
            // {
            //     var hash = "";
            //
            //     if (!first.ContainsKey(d))
            //     {
            //         first[d] = i;
            //     }
            //
            //     if (d != new Point(0, 1))
            //         continue;
            //
            //     for (int r = 0; r < maxRow; r++)
            //     {
            //         var line = "";
            //         for (int c = 0; c < maxCol; c++)
            //         {
            //             if (queue2.Any(s => s.point == new Point(r, c) && s.dimension == d))
            //             {
            //                 if (!first.ContainsKey(d))
            //                 {
            //                     first[d] = i;
            //                 }
            //
            //                 line += "O";
            //             }
            //             else
            //             {
            //                 line += grid[r, c];
            //             }
            //         }
            //
            //         logger.OnNext(line);
            //         hash += line;
            //     }
            //
            //     // foreach(var p in queue2.Where(s => s.dimension == d).Select(s => s.point))
            //     // {
            //     //     hash += $"({p.row},{p.col})";
            //     // }
            //     //
            //     // //get md5 string of hash
            //     // using MD5 md5Hash = MD5.Create();
            //     // byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(hash));
            //     // StringBuilder sBuilder = new();
            //     // for (int j = 0; j < data.Length; j++)
            //     // {
            //     //     sBuilder.Append(data[j].ToString("x2"));
            //     // }
                // var sBuilder = "";
                // logger.OnNext($"{sBuilder} - {d} - {first[d]}" );
            // }

            // logger.OnNext("");
        }

        for (var index = 1; index < stepsList.Count; index++)
        {
            var s = stepsList[index];
            logger.OnNext($"from {stepsList[index -1]} to {s}. {index} + {s - index} | diff: {s - stepsList[index - 1]} = {index} + {s - stepsList[index - 1] - index}");
        }

        GiveAnswer2(queue.Count);
    }
}

public record Point(int row, int col);
