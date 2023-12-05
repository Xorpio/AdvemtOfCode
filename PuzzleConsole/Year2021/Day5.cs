namespace PuzzleConsole.Year2021;

public struct Xy
{
    public int X { get; set; }
    public int Y { get; set; }
}

public class Day5 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
        var xy = puzzle.ToList()
            .SelectMany(l => l.Split("->"))
            .Select(s => s.Trim())
            .Select(s => s.Split(','))
            .Select(ss => new Xy { X = Int32.Parse(ss[0]), Y = Int32.Parse(ss[1]) });

        var maxX = xy.Max(xy => xy.X);
        var maxY = xy.Max(xy => xy.X);

        var grid = new int[maxX + 1, maxY + 1];
        var grid2 = new int[maxX + 1, maxY + 1];

        puzzle.ToList()
            .Select(l => l.Split(" -> "))
            .Select(s => new
            {
                From = new Xy { X = Int32.Parse(s[0].Split(',')[0]), Y = Int32.Parse(s[0].Split(',')[1]) },
                To = new Xy { X = Int32.Parse(s[1].Split(',')[0]), Y = Int32.Parse(s[1].Split(',')[1]) }
            })
            .ToList()
            .ForEach(l =>
            {
                if (l.From.X == l.To.X || l.From.Y == l.To.Y)
                {
                    drawLine(grid, l.From, l.To);
                }

                drawLine(grid2, l.From, l.To);
            });

        // return new[] { line, line };


        // var gridEnum = grid.GetEnumerator();
        var count = 0;
        foreach (var num in grid)
        {
            if (num > 1)
                count++;
        }
        var count2 = 0;
        foreach (var num in grid2)
        {
            if (num > 1)
                count2++;
        }

        return new[] { $"{count}", count2.ToString() };
    }

    private void drawLine(int[,] grid, Xy @from, Xy to)
    {
        var fromX = @from.X;
        var fromY = @from.Y;
        var toX = to.X;
        var toY = to.Y;

        grid[fromX, fromY]++;

        do
        {
            if (fromX != toX)
            {
                fromX += (fromX > toX) ? -1 : 1;
            }

            if (fromY != toY)
            {
                fromY += (fromY > toY) ? -1 : 1;
            }

            grid[fromX, fromY]++;
        }
        while (fromX != toX || fromY != toY) ;
    }
}
