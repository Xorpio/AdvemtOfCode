
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace AdventOfCode.Solvers.Year2024.Day20;

public class Day20Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        //var maze = puzzle.Select(x => x.ToCharArray()).ToArray();

        (int row, int col) start = (0, 0);
        (int row, int col) end = (0, 0);

        for (int row = 0; row < puzzle.Length; row++)
        {
            for (int col = 0; col < puzzle[row].Length; col++)
            {
                if (puzzle[row][col] == 'S')
                {
                    start = (row, col);
                }
                if (puzzle[row][col] == 'E')
                {
                    end = (row, col);
                }
            }
        }

        var map = new Dictionary<(int row, int col), int>();

        var count = 0;
        while (!map.ContainsKey(end))
        {
            map.Add(start, count);
            foreach (var (row, col) in new (int row, int col)[] { (0, 1), (0, -1), (1, 0), (-1, 0) })
            {
                var newRow = start.row + row;
                var newCol = start.col + col;
                if (puzzle[newRow][newCol] != '#' && !map.ContainsKey((newRow, newCol)))
                {
                    start = (newRow, newCol);
                    count++;
                    break;
                }
            }
        }

        var minimalCheat = IsRunningFromUnitTest() ? 50 : 100;
        var cheats = 0;
        var cheats2 = 0;
        var done = new HashSet<((int row, int col) start, (int row, int col) end)>();
        foreach (var (keystart, valuestart) in map)
        {
            foreach (var (keyend, valueend) in map)
            {
                if (valuestart >= valueend)
                {
                    continue;
                }

                var distance = CalculateManhattanDistance(keystart, keyend);

                if (distance < 3 && ((valuestart - valueend + distance) * -1) >= minimalCheat)
                {
                    cheats++;
                }
                if (distance < 21 && ((valuestart - valueend + distance) * -1) >= minimalCheat)
                {
                    cheats2++;
                }
            }
        }

        GiveAnswer1(cheats);

        GiveAnswer2(cheats2);
    }

    public int CalculateManhattanDistance((int row, int col) point1, (int row, int col) point2)
    {
        return Math.Abs(point1.row - point2.row) + Math.Abs(point1.col - point2.col);
    }
}
