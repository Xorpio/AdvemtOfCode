using System.Reflection.Metadata;

namespace AdventOfCode.Solvers.Year2023.Day18;

public class Day18Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        var pos = new Point(0, 0);
        var lines = new List<Line>();

        foreach(var line in puzzle)
        {
            // parse line from color
            var parts = line.Split(' ');
            switch(parts[0])
            {
                case "R":
                    lines.Add(new Line(pos, pos with { col = pos.col + int.Parse(parts[1]) }, true));
                    pos = pos with { col = pos.col + int.Parse(parts[1]) };
                    break;
                case "L":
                    lines.Add(new Line(pos with { col = pos.col - int.Parse(parts[1]) }, pos, true));
                    pos = pos with { col = pos.col - int.Parse(parts[1]) };
                    break;
                case "U":
                    lines.Add(new Line(pos with { row = pos.row - int.Parse(parts[1]) }, pos, false));
                    pos = pos with { row = pos.row - int.Parse(parts[1]) };
                    break;
                case "D":
                    lines.Add(new Line(pos, pos with { row = pos.row + int.Parse(parts[1]) }, false));
                    pos = pos with { row = pos.row + int.Parse(parts[1]) };
                    break;
            }
        }

        foreach(var l in lines)
        {
            logger.OnNext($"{l}");
        }

        double count = 0;

        var row = lines.Min(l => l.start.row) - 1;
        var endRow = lines.Max(l => l.end.row);


        logger.OnNext($"startRow: {row}");
        logger.OnNext($"endRow: {endRow}");

        while(row <= endRow)
        {
            // find all lines that intersect with this row

            row++;
        }

        GiveAnswer1("");
        GiveAnswer2("");
    }
}

public record Point(int row, int col);
public record Line(Point start, Point end, bool horizontal);
