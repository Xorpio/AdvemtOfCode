
using System.Runtime.Intrinsics.Arm;

namespace AdventOfCode.Solvers.Year2023.Day17;

public class Day17Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {

        List<(Point point, double heat, string path)> visited = new();
        List<(Point point, double heat, string path)> potential = new();

        var start = new Point(0, 0);
        var end = new Point(puzzle.Length - 1, puzzle[0].Length - 1);

        potential.Add((start, 0, ""));

        (Point point, double heat, string path) next = default;

        var score = double.MaxValue;
        
        var count = 0;
        do
        {
            if (potential.Count == 0)
            {
                break;
            }
            next = potential.OrderBy(x => x.heat + ((x.point + end) * 9)).First();

            potential.Remove(next);
            
            if (next.heat > score)
            {
                continue;
            }

            if (next.point.row < 0 || next.point.row >= puzzle.Length || next.point.col < 0 || next.point.col >= puzzle[0].Length)
            {
                continue;
            }

            if (next.point == end)
            {
                score = score > next.heat ? next.heat : score;
                logger.OnNext($"Found path {next.path} with heat {next.heat}");
                continue;
            }

            var pathHistory = next.path.Length > 3 ? next.path[^3..] : next.path;
            var last = next.path.Length > 0 ? next.path[^1] : ' ';

            if (pathHistory != "NNN" && next.point.row > 0 && last != 'S')
            {
                potential.Add((new Point(next.point.row - 1, next.point.col), next.heat + int.Parse(puzzle[next.point.row -1][next.point.col].ToString()), next.path + "N"));
            }
            if (next.point.row < puzzle.Length - 1 && pathHistory != "SSS" && last != 'N')
            {
                potential.Add((new Point(next.point.row + 1, next.point.col), next.heat + int.Parse(puzzle[next.point.row + 1][next.point.col].ToString()), next.path + "S"));
            }
            if (pathHistory != "WWW" && next.point.col > 0 && last != 'E')
            {
                potential.Add((new Point(next.point.row, next.point.col - 1), next.heat + int.Parse(puzzle[next.point.row][next.point.col - 1].ToString()), next.path + "W"));
            }
            if (next.point.col < puzzle[0].Length - 1 && pathHistory != "EEE" && last != 'W')
            {
                potential.Add((new Point(next.point.row, next.point.col + 1), next.heat + int.Parse(puzzle[next.point.row][next.point.col + 1].ToString()), next.path + "E"));
            }

            potential.RemoveAll(x => x.heat > score);

            count++;
        }while (potential.Count > 0);

        GiveAnswer1(score);
        GiveAnswer2("");
    }
}

public record Point(int row, int col)
{
    public static int operator +(Point a, Point b) => Math.Abs(a.col - b.col) + Math.Abs(a.row - b.row);
}
