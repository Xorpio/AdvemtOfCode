
using System.Runtime.Intrinsics.Arm;

namespace AdventOfCode.Solvers.Year2023.Day17;

public class Day17Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        Point start = new(0, 0);
        Point end = new(puzzle.Length - 1, puzzle[0].Length - 1);

        Dictionary<(Point p, string steps), int> visited = [];
        List<(Point c, string Steps, int heat)> potential = [];

        potential.Add((start, "-", 0));
        potential.Add((start, "|", 0));

        do
        {
            var current = potential.OrderBy(p => p.heat + (p.c + end)).First();
            potential.Remove(current);

            if (!visited.TryAdd((current.c, current.Steps), current.heat))
            {
                continue;
            }

            var directions = new List<(Point p, string s)>();

            if (current.c == end)
            {
                GiveAnswer1(current.heat.ToString());
                break;
            }

            if (current.Steps != "---")
            {
                var newSteps = (current.Steps.Length > 0 && current.Steps[0] == '-') ?
                    current.Steps + "-" :
                    "-";

                directions.Add((current.c with { col = current.c.col + 1 }, newSteps));
                directions.Add((current.c with { col = current.c.col - 1 }, newSteps));
            }
            if (current.Steps != "|||")
            {
                var newSteps = (current.Steps.Length > 0 && current.Steps[0] == '|') ?
                    current.Steps + "|" :
                    "|";

                directions.Add((current.c with { row = current.c.row + 1 }, newSteps));
                directions.Add((current.c with { row = current.c.row - 1 }, newSteps));
            }

            foreach (var d in directions)
            {
                if (!visited.ContainsKey((d.p, d.s))
                    && d.p.col >= 0 && d.p.col < puzzle[0].Length && d.p.row >= 0 && d.p.row < puzzle.Length
                    )
                {
                    potential.Add((d.p, d.s, current.heat + int.Parse(puzzle[d.p.row][d.p.col].ToString())));
                }
            }

        } while (potential.Any());

        GiveAnswer1("");
        GiveAnswer2("");
    }
}

public record Point(int row, int col)
{
    public static int operator +(Point a, Point b) => Math.Abs(a.col - b.col) + Math.Abs(a.row - b.row);
}
