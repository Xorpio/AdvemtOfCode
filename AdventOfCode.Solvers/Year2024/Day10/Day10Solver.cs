
namespace AdventOfCode.Solvers.Year2024.Day10;

public class Day10Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        var total = 0;
        var total2 = 0;
        for (int row = 0; row < puzzle.Length; row++)
        {
            for (int col = 0; col < puzzle[row].Length; col++)
            {
                if (puzzle[row][col] == '0')
                {

                    total += CountTrailHeads(puzzle, row, col).Count;
                    total2 += CountTrails(puzzle, row, col);
                }
            }
        }
        GiveAnswer1(total);
        GiveAnswer2(total2);
    }

    private HashSet<(int row, int col)> CountTrailHeads(string[] puzzle, int row, int col)
    {
        if (puzzle[row][col] == '9')
        {
            return new HashSet<(int row, int col)>() { (row, col) };
        }

        var total = new HashSet<(int row, int col)>();
        var step = int.Parse(puzzle[row][col].ToString());
        if (row + 1 < puzzle.Length && int.Parse(puzzle[row + 1][col].ToString()) == step + 1)
        {
            total.UnionWith(CountTrailHeads(puzzle, row + 1, col));
        }
        if (row - 1 >= 0 && int.Parse(puzzle[row - 1][col].ToString()) == step + 1)
        {
            total.UnionWith(CountTrailHeads(puzzle, row - 1, col));
        }
        if (col + 1 < puzzle[row].Length && int.Parse(puzzle[row][col + 1].ToString()) == step + 1)
        {
            total.UnionWith(CountTrailHeads(puzzle, row, col + 1));
        }
        if (col - 1 >= 0 && int.Parse(puzzle[row][col - 1].ToString()) == step + 1)
        {
            total.UnionWith(CountTrailHeads(puzzle, row, col - 1));
        }

        return total;
    }
    private int CountTrails(string[] puzzle, int row, int col)
    {
        if (puzzle[row][col] == '9')
        {
            return 1;
        }

        var total = 0;
        var step = int.Parse(puzzle[row][col].ToString());
        if (row + 1 < puzzle.Length && int.Parse(puzzle[row + 1][col].ToString()) == step + 1)
        {
            total += CountTrails(puzzle, row + 1, col);
        }
        if (row - 1 >= 0 && int.Parse(puzzle[row - 1][col].ToString()) == step + 1)
        {
            total += CountTrails(puzzle, row - 1, col);
        }
        if (col + 1 < puzzle[row].Length && int.Parse(puzzle[row][col + 1].ToString()) == step + 1)
        {
            total += CountTrails(puzzle, row, col + 1);
        }
        if (col - 1 >= 0 && int.Parse(puzzle[row][col - 1].ToString()) == step + 1)
        {
            total += CountTrails(puzzle, row, col - 1);
        }

        return total;
    }
}
