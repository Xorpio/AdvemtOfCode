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
        var scenicScore = 0;
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

            var newScore = GetScenicScore(x, y, grid);
            if (newScore > scenicScore)
            {
                scenicScore = newScore;
            }
        }

        return new string[]
        {
            visible.ToString(),
            scenicScore.ToString()
        };
    }

    private int GetScenicScore(int x, int y, int[,] grid)
    {
        var north = ToNorth(x, y, grid);
        var east = ToEast(x, y, grid);
        var south = ToSouth(x, y, grid);
        var west = ToWest(x, y, grid);

        return north * east * south * west;
    }

    private int ToNorth(int x, int y, int[,] grid)
    {
        if (x == 0)
        {
            return 0;
        }

        var seen = 0;
        for (var xx = x - 1; xx >= 0; xx--)
        {
            if (grid[xx, y] < grid[x, y])
            {
                seen++;
            }
            else
            {
                return seen + 1;
            }
        }

        return seen;
    }

    private int ToEast(int x, int y, int[,] grid)
    {
        if (y == grid.GetLength(1) - 1)
        {
            return 0;
        }

        var seen = 0;
        for (var yy = y + 1; yy < grid.GetLength(1); yy++)
        {
            if (grid[x, yy] < grid[x, y])
            {
                seen++;
            }
            else
            {
                return seen + 1;
            }
        }

        return seen;
    }

    private int ToSouth(int x, int y, int[,] grid)
    {
        if (x == grid.GetLength(0) - 1)
        {
            return 0;
        }

        var seen = 0;
        for (var xx = x + 1; xx < grid.GetLength(0); xx++)
        {
            if (grid[xx, y] < grid[x, y])
            {
                seen++;
            }
            else
            {
                return seen + 1;
            }
        }

        return seen;
    }

    private int ToWest(int x, int y, int[,] grid)
    {
        if (y == 0)
        {
            return 0;
        }

        var seen = 0;
        for (var yy = y - 1; yy > 0; yy--)
        {
            if (grid[x, yy] < grid[x, y])
            {
                seen++;
            }
            else
            {
                return seen + 1;
            }
        }

        return seen;
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