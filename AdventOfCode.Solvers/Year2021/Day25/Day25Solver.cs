namespace AdventOfCode.Solvers.Year2021.Day25;

public class Day25Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        var maxCol = puzzle[0].Length;
        var maxRow = puzzle.Length;

        var cucumbers = new Dictionary<Point, char>();
        for (int row = 0; row < maxRow; row++)
        {
            for (int col = 0; col < maxCol; col++)
            {
                if (puzzle[row][col] == '.') continue;

                cucumbers.Add(new Point(row, col), puzzle[row][col]);
            }
        }

        PrintCucumbers(cucumbers, 0);

        var step = 0;
        List<(Point from, Point to)> ToMoveEast = new();
        List<(Point from, Point to)> ToMoveSouth = new();
        do
        {
            ToMoveEast = new ();
            ToMoveSouth = new ();
            var dir = '>';
            var cucumbersToMoveEast = cucumbers.Where(c => c.Value == dir).Select(c => c.Key).ToList();
            foreach(var point in cucumbersToMoveEast)
            {
                //check if we can move
                var nextPoint = dir switch
                {
                    '>' => new Point(point.row, (point.col + 1) % maxCol),
                    'v' => new Point((point.row + 1) % maxRow, point.col),
                    _ => throw new Exception("Unknown direction")
                };

                if (cucumbers.ContainsKey(nextPoint)) continue;

                ToMoveEast.Add((point, nextPoint));
            }

            foreach(var point in ToMoveEast)
            {
                cucumbers.Remove(point.from);
                cucumbers.Add(point.to, dir);
            }
            dir = 'v';
            var cucumbersToMoveSouth = cucumbers.Where(c => c.Value == dir).Select(c => c.Key).ToList();
            foreach(var point in cucumbersToMoveSouth)
            {
                //check if we can move
                var nextPoint = dir switch
                {
                    '>' => new Point(point.row, (point.col + 1) % maxCol),
                    'v' => new Point((point.row + 1) % maxRow, point.col),
                    _ => throw new Exception("Unknown direction")
                };

                if (cucumbers.ContainsKey(nextPoint)) continue;

                ToMoveSouth.Add((point, nextPoint));
            }

            foreach(var point in ToMoveSouth)
            {
                cucumbers.Remove(point.from);
                cucumbers.Add(point.to, dir);
            }
            step++;
        }while(ToMoveEast.Count > 0 || ToMoveSouth.Count > 0);
        PrintCucumbers(cucumbers, step);

        GiveAnswer1(step);
        GiveAnswer2("");
    }

    private void PrintCucumbers(Dictionary<Point, char> cucumbers, int step)
    {
        var maxRow = cucumbers.Max(c => c.Key.row);
        var maxCol = cucumbers.Max(c => c.Key.col);

        var line = "";
        logger.OnNext(line + $"Step {step}");

        for (int row = 0; row <= maxRow; row++)
        {
            for (int col = 0; col <= maxCol; col++)
            {
                if (cucumbers.ContainsKey(new Point(row, col)))
                {
                    line += cucumbers[new Point(row, col)];
                }
                else
                {
                    line += ".";
                }
            }
            logger.OnNext(line);
            line = "";
        }

        logger.OnNext("");
    }
}

public record Point(int row, int col);
