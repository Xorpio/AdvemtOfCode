using System.IO.IsolatedStorage;
using System.Reflection.Metadata;

namespace AdventOfCode.Solvers.Year2023.Day18;

public class Day18Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        var count = SolvePart(true, puzzle);

        GiveAnswer1(count);
        count = SolvePart(false, puzzle);
        GiveAnswer2(count);
    }

    public double SolvePart(bool isOne, string[] puzzle)
    {
        var pos = new Point(0, 0);
        var lines = new List<Line>();
        var corners = new List<Point>();

        foreach(var line in puzzle)
        {
            var parts = isOne ? line.Split(' ') : GetParts2(line);
            switch(parts[0])
            {
                case "R":
                    lines.Add(new Line(pos, pos with { col = pos.col + double.Parse(parts[1]) }, true));
                    pos = pos with { col = pos.col + double.Parse(parts[1]) };
                    break;
                case "L":
                    lines.Add(new Line(pos with { col = pos.col - double.Parse(parts[1]) }, pos, true));
                    pos = pos with { col = pos.col - double.Parse(parts[1]) };
                    break;
                case "U":
                    lines.Add(new Line(pos with { row = pos.row - double.Parse(parts[1]) }, pos, false));
                    pos = pos with { row = pos.row - double.Parse(parts[1]) };
                    break;
                case "D":
                    lines.Add(new Line(pos, pos with { row = pos.row + double.Parse(parts[1]) }, false));
                    pos = pos with { row = pos.row + double.Parse(parts[1]) };
                    break;
            }

            corners.Add(pos);
        }

        foreach(var l in lines)
        {
            // logger.OnNext($"{l}");
        }

        double count = 0;

        double row = lines.Min(l => l.start.row) - 1;
        double startRow = lines.Min(l => l.start.row);
        double endRow = lines.Max(l => l.end.row);
        double starCol = lines.Min(l => l.start.col);
        double endCol = lines.Max(l => l.end.col);

        // logger.OnNext($"startRow: {row}");
        // logger.OnNext($"endRow: {endRow}");

        List<(double start, double end)> blocks = [];

        var rows =  lines.Where(l => l.horizontal).Select(l => l.start.row).Distinct().OrderBy(r => r);
        double rowNum = rows.First();

        var startBlock = true;
        while (rowNum <= endRow)
        {
            // logger.OnNext($"rowNum: {row} to {rowNum - 1}");
            foreach(var block in blocks)
            {
                count += (block.end - block.start + 1) * (rowNum - row);
            }

            blocks.Clear();

            double[] cols = FindCols(lines, rowNum);

            for (int i = 0; i < cols.Length; i++)
            {
                if (startBlock)
                {
                    var corner = corners.Where(c => c.row == rowNum && c.col == cols[i]).FirstOrDefault();
                    if (corner == null)
                    {
                        blocks.Add((cols[i], 0));
                        startBlock = false;
                        continue;
                    }

                    var line1 = lines.Where(l => !l.horizontal && (corner == l.start || corner == l.end)).First();
                    var line2 = lines.Where(l => !l.horizontal && (new Point(rowNum, cols[i + 1]) == l.start || new Point(rowNum, cols[i + 1]) == l.end)).First();
                    if ((line1.start.row < rowNum && line2.start.row < rowNum) || (line1.end.row > rowNum && line2.end.row > rowNum))
                    {
                        blocks.Add((corner.col, cols[i + 1]));
                        i++;
                        continue;
                    } 

                    blocks.Add((corner.col, 0));
                    i++;
                    startBlock = false;
                }
                else if (!startBlock)
                {
                    var corner = corners.Where(c => c.row == rowNum && c.col == cols[i]).FirstOrDefault();
                    if (corner == null)
                    {
                        blocks[^1] = (blocks[^1].start, cols[i]);
                        startBlock = true;
                        continue;
                    }

                    var line1 = lines.Where(l => !l.horizontal && (corner == l.start || corner == l.end)).First();
                    var line2 = lines.Where(l => !l.horizontal && (new Point(rowNum, cols[i + 1]) == l.start || new Point(rowNum, cols[i + 1]) == l.end)).First();
                    if ((line1.start.row < rowNum && line2.start.row < rowNum) || (line1.end.row > rowNum && line2.end.row > rowNum))
                    {
                        // blocks[^1] = (blocks[^1].start, corner.col);
                        // blocks.Add((cols[i + 1], 0));
                        i++;
                        continue;
                    } 

                    blocks[^1] = (blocks[^1].start, cols[i + 1]);
                    i++;
                    startBlock = true;
                }
            }

            if (!startBlock)
            {
                throw new Exception($"Blocks not closed on line {rowNum}");
            }

            row = rowNum;

            if (rows.Contains(rowNum))
            {
                rowNum++;
            }
            else
            {
                // find next row
                try
                {
                    rowNum = rows.Where(r => r > rowNum).First();
                }
                catch (Exception)
                {
                    rowNum = endRow + 1;
                }
            }
        }

        foreach(var block in blocks)
        {
            count += block.end - block.start + 1;
        }

        return count;
    }

    private string[] GetParts2(string line)
    {
        var parts = line.Split('#');
        var l = parts[1][..^1];

         // parse l from hex to decimal
        var count = Convert.ToInt32(l[..^1], 16);

        var dir = l[^1] switch
        {
            '0' => "R",
            '1' => "D",
            '2' => "L",
            '3' => "U",
            _ => throw new Exception($"Unknown direction {l[^1]}")
        };

        return [dir, count.ToString()];
    }

    private double[] FindCols(List<Line> lines, double rowNum)
    {
        var cols = new List<double>();
        cols.AddRange(lines.Where(l => l.horizontal && l.start.row == rowNum).SelectMany(l => new[] { l.start, l.end }).Select(p => p.col));

        foreach(var vertLine in lines.Where(l => !l.horizontal))
        {
            if (vertLine.start.row < rowNum && vertLine.end.row > rowNum)
            {
                cols.Add(vertLine.start.col);
            }
        }

        return cols.OrderBy(c => c).ToArray();
    }
}

public record Point(double row, double col);
public record Line(Point start, Point end, bool horizontal);
