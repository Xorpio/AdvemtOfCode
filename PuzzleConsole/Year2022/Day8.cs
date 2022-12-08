namespace PuzzleConsole.Year2022.Day8;

public class Day8 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
        var grid = new int[puzzle[0].Length, puzzle.Count()];

        for (var y = 0; y < puzzle.Length; y++)
        {
            var line = puzzle[y];
            for (var x = 0; x < line.Length; x++)
            {
                var c = line[x];
                var n = int.Parse(c.ToString());

                grid[x, y] = n;
            }
        }

        var visible = 0;
        for (var x = 0; x < grid.GetLength(0); x++)
        for (var y = 0; y < grid.GetLength(1); y++)
        {
            if (fromNorth(x, grid, y) ||
                fromEast(x, grid, y) ||
                fromSouth(x, grid, y) ||
                fromWest(x, grid, y))
            {
                visible++;
            }
        }

        return new string[]
        {
            visible.ToString()
        };
    }

    private bool fromNorth(int x, int[,] grid, int y)
    {
        for (var xx = 0; xx < x; xx++)
        {
            if (grid[xx, y] >= grid[x, y])
            {
                return false;
            }
        }

        return true;
    }

    private bool fromEast(int x, int[,] grid, int y)
    {
        for (var yy = grid.GetLength(1) - 1; yy > y; yy--)
        {
            if (grid[x, yy] >= grid[x, y])
            {
                return false;
            }
        }

        return true;
    }

    private bool fromSouth(int x, int[,] grid, int y)
    {
        for (var xx = grid.GetLength(0) - 1; xx > x; xx--)
        {
            if (grid[xx, y] >= grid[x, y])
            {
                return false;
            }
        }

        return true;
    }

    private bool fromWest(int x, int[,] grid, int y)
    {
        for (var yy = 0; yy < y; yy++)
        {
            if (grid[x, yy] >= grid[x, y])
            {
                return false;
            }
        }

        return true;
    }
}