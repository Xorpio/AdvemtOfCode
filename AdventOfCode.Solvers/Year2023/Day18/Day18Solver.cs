using System.Reflection.Metadata;

namespace AdventOfCode.Solvers.Year2023.Day18;

public class Day18Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        var pos = new Point(0, 0);
        var points = new List<Point>();
        foreach(var line in puzzle)
        {
            var parts = line.Split(' ');
            for(int c = int.Parse(parts[1]); c > 0; c--)
            {
                switch(parts[0])
                {
                    case "R":
                        pos = new Point(pos.row, pos.col + 1);
                        break;
                    case "L":
                        pos = new Point(pos.row, pos.col - 1);
                        break;
                    case "U":
                        pos = new Point(pos.row - 1, pos.col);
                        break;
                    case "D":
                        pos = new Point(pos.row + 1, pos.col);
                        break;
                }
                points.Add(pos);
            }
        }

        var minRow = points.Min(p => p.row) - 1;
        var maxRow = points.Max(p => p.row) + 1;
        var minCol = points.Min(p => p.col) - 1;
        var maxCol = points.Max(p => p.col) + 1;

        var seen = new List<Point>();
        var queu = new Queue<Point>();
        queu.Enqueue(new Point(minRow, minCol));
        while (queu.Any())
        {
            var current = queu.Dequeue();

            if (seen.Contains(current) || points.Contains(current) || current.row < minRow || current.row > maxRow || current.col < minCol || current.col > maxCol)
            {
                continue;
            }

            seen.Add(current);

            queu.Enqueue(new Point(current.row + 1, current.col));
            queu.Enqueue(new Point(current.row - 1, current.col));
            queu.Enqueue(new Point(current.row, current.col + 1));
            queu.Enqueue(new Point(current.row, current.col - 1));
        }

        for(int row = minRow; row <= maxRow; row++)
        {
            var line = "";
            for(int col = minCol; col <= maxCol; col++)
            {
                if (points.Contains(new Point(row, col)))
                {
                    line += "#";
                }
                else if (seen.Contains(new Point(row, col)))
                {
                    line += ".";
                }
                else
                {
                    line += " ";
                }
            }
            // logger.OnNext(line);
        }

        double gridSize = (maxRow - minRow + 1) * (maxCol - minCol + 1);    
        logger.OnNext($"grid row size = {maxRow - minRow + 1}");
        logger.OnNext($"grid col size = {maxCol - minCol + 1}");
        logger.OnNext($"seen: {seen.Count}");
        double answer1 = gridSize - seen.Count;

        GiveAnswer1(answer1);
        GiveAnswer2("");
    }
}

public record Point(int row, int col);
