namespace AdventOfCode.Solvers.Year2024.Day4;

public class Day4Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        decimal countPart1 = 0;
        decimal countPart2 = 0;
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
                    countPart1++;
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
                    countPart1++;
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
                    countPart1++;
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
                    countPart1++;
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
                    countPart1++;
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
                    countPart1++;
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
                    countPart1++;
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
                    countPart1++;
                }

                //up pos
                // S.S
                // .A.
                // M.M
                if (
                    c > 0 && c < puzzle[0].Length - 1 && r > 0 && r < puzzle.Length - 1 &&
                    puzzle[r][c] == 'A' &&
                    puzzle[r - 1][c - 1] == 'S' &&
                    puzzle[r - 1][c + 1] == 'S' &&
                    puzzle[r + 1][c - 1] == 'M' &&
                    puzzle[r + 1][c + 1] == 'M'
                    )
                {
                    countPart2++;
                }
                //Left pos
                // M.S
                // .A.
                // M.S
                if (
                    c > 0 && c < puzzle[0].Length - 1 && r > 0 && r < puzzle.Length - 1 &&
                    puzzle[r][c] == 'A' &&
                    puzzle[r - 1][c - 1] == 'M' &&
                    puzzle[r - 1][c + 1] == 'S' &&
                    puzzle[r + 1][c - 1] == 'M' &&
                    puzzle[r + 1][c + 1] == 'S'
                    )
                {
                    countPart2++;
                }
                //Right pos
                // S.M
                // .A.
                // S.M
                if (
                    c > 0 && c < puzzle[0].Length - 1 && r > 0 && r < puzzle.Length - 1 &&
                    puzzle[r][c] == 'A' &&
                    puzzle[r - 1][c - 1] == 'S' &&
                    puzzle[r - 1][c + 1] == 'M' &&
                    puzzle[r + 1][c - 1] == 'S' &&
                    puzzle[r + 1][c + 1] == 'M'
                    )
                {
                    countPart2++;
                }
                //up pos
                // M.M
                // .A.
                // S.S
                if (
                    c > 0 && c < puzzle[0].Length - 1 && r > 0 && r < puzzle.Length - 1 &&
                    puzzle[r][c] == 'A' &&
                    puzzle[r - 1][c - 1] == 'M' &&
                    puzzle[r - 1][c + 1] == 'M' &&
                    puzzle[r + 1][c - 1] == 'S' &&
                    puzzle[r + 1][c + 1] == 'S'
                    )
                {
                    countPart2++;
                }
            }
        }
        GiveAnswer1(countPart1);
        GiveAnswer2(countPart2);
    }
}
