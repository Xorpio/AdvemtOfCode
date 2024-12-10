using AdventOfCode.Solvers.Year2023.Day18;

namespace AdventOfCode.Solvers.Year2024.Day8;

public class Day8Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        HashSet<(int row, int col)> location = new();
        HashSet<(int row, int col)> location2 = new();

        var antennas = new Dictionary<char, IList<(int row, int col)>>();

        for (int row = 0; row < puzzle.Length; row++)
        {
            for (int col = 0; col < puzzle[row].Length; col++)
            {
                if (puzzle[row][col] == '.')
                {
                    continue;
                }

                if (!antennas.ContainsKey(puzzle[row][col]))
                {
                    antennas.Add(puzzle[row][col], new List<(int row, int col)>());
                }

                antennas[puzzle[row][col]].Add((row, col));

                foreach (var (r, c) in antennas[puzzle[row][col]])
                {
                    if (r == row && c == col)
                    {
                        continue;
                    }

                    var absRow = Math.Abs(r - row);
                    var absCol = Math.Abs(c - col);

                    if (r == row)
                    {
                        location.Add((r, c - absCol));
                        location.Add((r, col + absCol));

                        var count = 1;
                        while (c - (count * absCol) >= 0 || col + (count * absCol) <= puzzle[0].Length)
                        {
                            location2.Add((r, c - (count * absCol)));
                            location2.Add((r, col + (count * absCol)));
                            count++;
                        }
                    }
                    else if (c == col)
                    {
                        location.Add((r - absRow, c));
                        location.Add((row + absRow, c));

                        var count = 1;
                        while (r - (count * absRow) >= 0 || row + (count * absRow) <= puzzle.Length)
                        {
                            location2.Add((r - (count * absRow), c));
                            location2.Add((row + (count * absRow), c));
                            count++;
                        }
                    }
                    else if (c > col)
                    {
                        location.Add((r - absRow, c + absCol));
                        location.Add((row + absRow, col - absCol));

                        var count = 1;
                        while (r - (count * absRow) >= 0 || row + (count * absRow) <= puzzle.Length || c + (count * absCol) <= puzzle[0].Length || col - (count * absCol) >= 0)
                        {
                            location2.Add((r - (count * absRow), c + (count * absCol)));
                            location2.Add((row + (count * absRow), col - (count * absCol)));
                            count++;
                        }
                    }
                    else if (c < col)
                    {
                        location.Add((r - absRow, c - absCol));
                        location.Add((row + absRow, col + absCol));

                        var count = 1;
                        while (r - (count * absRow) >= 0 || row + (count * absRow) <= puzzle.Length || c - (count * absCol) >= 0 || col + (count * absCol) <= puzzle[0].Length)
                        {
                            location2.Add((r - (count * absRow), c - (count * absCol)));
                            location2.Add((row + (count * absRow), col + (count * absCol)));
                            count++;
                        }
                    }
                }
            }
        }

        foreach (var (r, c) in antennas.Where(l => l.Value.Count > 1).SelectMany(l => l.Value))
        {
            location2.Add((r, c));
        }

        foreach (var line in puzzle)
        {
            logger.OnNext(line);
        }

        GiveAnswer1(
            location
            .Where(l => l.row >= 0 && l.row < puzzle.Length && l.col >= 0 && l.col < puzzle[0].Length)
            .Count()
        );
        GiveAnswer2(
            location2
            .Where(l => l.row >= 0 && l.row < puzzle.Length && l.col >= 0 && l.col < puzzle[0].Length)
            .Count()
        );
    }
}
