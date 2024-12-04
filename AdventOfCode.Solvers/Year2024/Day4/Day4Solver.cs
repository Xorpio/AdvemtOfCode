namespace AdventOfCode.Solvers.Year2024.Day4;

public class Day4Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        decimal count = 0;
        for (int r = 0; r < puzzle.Length; r++)
        {
            for (int c = 0; c < puzzle[0].Length; c++)
            {
                //right
                if (
                    c + 3 < puzzle[0].Length &&
                    puzzle[r][c] == 'X' &&
                    puzzle[r][c + 1] == 'M' &&
                    puzzle[r][c + 2] == 'A' &&
                    puzzle[r][c + 3] == 'S'
                )
                {
                    count++;
                }
                //right & Down
                if (
                    c + 3 < puzzle[0].Length && r + 3 < puzzle.Length &&
                    puzzle[r][c] == 'X' &&
                    puzzle[r + 1][c + 1] == 'M' &&
                    puzzle[r + 2][c + 2] == 'A' &&
                    puzzle[r + 3][c + 3] == 'S'
                )
                {
                    count++;
                }
                // Down
                if (
                    r + 3 < puzzle.Length &&
                    puzzle[r][c] == 'X' &&
                    puzzle[r + 1][c] == 'M' &&
                    puzzle[r + 2][c] == 'A' &&
                    puzzle[r + 3][c] == 'S'
                )
                {
                    count++;
                }
                // Down & Left
                if (
                    r + 3 < puzzle.Length && c - 3 >= 0 &&
                    puzzle[r][c] == 'X' &&
                    puzzle[r + 1][c - 1] == 'M' &&
                    puzzle[r + 2][c - 2] == 'A' &&
                    puzzle[r + 3][c - 3] == 'S'
                )
                {
                    count++;
                }
                // Left
                if (
                    c - 3 >= 0 &&
                    puzzle[r][c] == 'X' &&
                    puzzle[r][c - 1] == 'M' &&
                    puzzle[r][c - 2] == 'A' &&
                    puzzle[r][c - 3] == 'S'
                )
                {
                    count++;
                }

                // Left & Up
                if (
                    c - 3 >= 0 && r - 3 >= 0 &&
                    puzzle[r][c] == 'X' &&
                    puzzle[r - 1][c - 1] == 'M' &&
                    puzzle[r - 2][c - 2] == 'A' &&
                    puzzle[r - 3][c - 3] == 'S'
                )
                {
                    count++;
                }
                // Up
                if (
                    r - 3 >= 0 &&
                    puzzle[r][c] == 'X' &&
                    puzzle[r - 1][c] == 'M' &&
                    puzzle[r - 2][c] == 'A' &&
                    puzzle[r - 3][c] == 'S'
                )
                {
                    count++;
                }
                // Up & Right
                if (
                    r - 3 >= 0 && c + 3 < puzzle[0].Length &&
                    puzzle[r][c] == 'X' &&
                    puzzle[r - 1][c + 1] == 'M' &&
                    puzzle[r - 2][c + 2] == 'A' &&
                    puzzle[r - 3][c + 3] == 'S'
                )
                {
                    count++;
                }
            }
        }
        GiveAnswer1(count);
        GiveAnswer2("");
    }
}
