using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;

namespace AdventOfCode.Solvers.Year2024.Day12;

public class Day12Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        var seen = new HashSet<(int row, int col)>();

        var total = 0;
        var total2 = 0;

        for (int row = 0; row < puzzle.Length; row++)
        {
            for (int col = 0; col < puzzle[row].Length; col++)
            {
                if (!seen.Contains((row, col)))
                {
                    var region = 0;
                    var fence = 0;

                    var queue = new Queue<(int row, int col)>();
                    queue.Enqueue((row, col));
                    var seenregion = new HashSet<(int row, int col)>();


                    var plant = puzzle[row][col];

                    while (queue.Count > 0)
                    {
                        var item = queue.Dequeue();

                        if (seenregion.Contains(item))
                        {
                            continue;
                        }

                        seenregion.Add(item);

                        region++;

                        //check up
                        if (item.row > 0)
                        {
                            if (puzzle[item.row - 1][item.col] == plant && !seenregion.Contains((item.row - 1, item.col)))
                            {
                                queue.Enqueue((item.row - 1, item.col));
                            }
                            else if (puzzle[item.row - 1][item.col] != plant)
                            {
                                fence++;

                            }
                        }
                        else
                        {
                            fence++;

                        }

                        //check down
                        if ((item.row + 1) < puzzle[row].Length)
                        {

                            if (puzzle[item.row + 1][item.col] == plant && !seenregion.Contains((item.row + 1, item.col)))
                            {
                                queue.Enqueue((item.row + 1, item.col));
                            }
                            else if (puzzle[item.row + 1][item.col] != plant)
                            {
                                fence++;


                            }
                        }
                        else
                        {

                            fence++;

                        }

                        // check left
                        if (item.col > 0)
                        {
                            if (puzzle[item.row][item.col - 1] == plant && !seenregion.Contains((item.row, item.col - 1)))
                            {
                                queue.Enqueue((item.row, item.col - 1));
                            }
                            else if (puzzle[item.row][item.col - 1] != plant)
                            {
                                fence++;

                            }
                        }
                        else
                        {
                            fence++;

                        }

                        //check right
                        if ((item.col + 1) < puzzle[0].Length)
                        {
                            if (puzzle[item.row][item.col + 1] == plant && !seenregion.Contains((item.row, item.col + 1)))
                            {
                                queue.Enqueue((item.row, item.col + 1));
                            }
                            else if (puzzle[item.row][item.col + 1] != plant)
                            {
                                fence++;

                            }
                        }
                        else
                        {
                            fence++;

                        }
                    }

                    var maxRow = seenregion.Max(m => m.row);
                    var minRow = seenregion.Min(m => m.row);
                    var maxCol = seenregion.Max(m => m.col);
                    var minCol = seenregion.Min(m => m.col);

                    // look down
                    (char? i, char? o) hist = (null, null);
                    var sides = 0;
                    for (int r = minRow; r <= maxRow; r++)
                    {
                        hist = (null, null);
                        for (int c = minCol; c <= maxCol; c++)
                        {
                            if (!seenregion.Contains((r, c)))
                            {
                                hist = (puzzle[r][c], c == maxCol ? null : puzzle[r][c + 1]);
                                continue;
                            }

                            if (puzzle[r][c] == plant && hist.i != plant)
                            {
                                if (r == maxRow || puzzle[r + 1][c] != plant)
                                {
                                    sides++;
                                }
                            }
                            if (r != maxRow && hist.i == plant && hist.o == plant && puzzle[r + 1][c] != plant && puzzle[r][c] == plant)
                            {
                                sides++;
                            }

                            hist = (puzzle[r][c], r == maxRow ? null : puzzle[r + 1][c]);
                        }
                    }

                    // look right
                    for (int c = minCol; c <= maxCol; c++)
                    {
                        hist = (null, null);
                        for (int r = minRow; r <= maxRow; r++)
                        {
                            if (!seenregion.Contains((r, c)))
                            {
                                hist = (puzzle[r][c], c == maxCol ? null : puzzle[r][c + 1]);
                                continue;
                            }
                            if (puzzle[r][c] == plant && hist.i != plant)
                            {
                                if (c == maxCol || puzzle[r][c + 1] != plant)
                                {
                                    sides++;
                                }
                            }

                            if (c != maxCol && hist.i == plant && hist.o == plant && puzzle[r][c + 1] != plant && puzzle[r][c] == plant)
                            {
                                sides++;
                            }

                            hist = (puzzle[r][c], c == maxCol ? null : puzzle[r][c + 1]);
                        }
                    }

                    // look up
                    for (int r = maxRow; r >= minRow; r--)
                    {
                        hist = (null, null);
                        for (int c = maxCol; c >= minCol; c--)
                        {
                            if (!seenregion.Contains((r, c)))
                            {
                                hist = (puzzle[r][c], c == maxCol ? null : puzzle[r][c + 1]);
                                continue;
                            }
                            if (puzzle[r][c] == plant && hist.i != plant)
                            {
                                if (r == minRow || puzzle[r - 1][c] != plant)
                                {
                                    sides++;
                                }
                            }
                            if (r != minRow && hist.i == plant && hist.o == plant && puzzle[r - 1][c] != plant && puzzle[r][c] == plant)
                            {
                                sides++;
                            }

                            hist = (puzzle[r][c], r == minRow ? null : puzzle[r - 1][c]);
                        }
                    }

                    // look right
                    for (int c = maxCol; c >= minCol; c--)
                    {
                        hist = (null, null);
                        for (int r = maxRow; r >= minRow; r--)
                        {
                            if (!seenregion.Contains((r, c)))
                            {
                                hist = (puzzle[r][c], c == maxCol ? null : puzzle[r][c + 1]);
                                continue;
                            }
                            if (puzzle[r][c] == plant && hist.i != plant)
                            {
                                if (c == minCol || puzzle[r][c - 1] != plant)
                                {
                                    sides++;
                                }
                            }
                            if (c != minCol && hist.i == plant && hist.o == plant && puzzle[r][c - 1] != plant && puzzle[r][c] == plant)
                            {
                                sides++;
                            }

                            hist = (puzzle[r][c], c == minCol ? null : puzzle[r][c - 1]);
                        }
                    }


                    //logger.OnNext($"Plant: {plant} Region: {region}, Fence: {fence} = price {region * fence}");
                    total += region * fence;

                    logger.OnNext($"Plant: {plant} Region: {region}, Sides: {sides} = price {region * sides}");
                    total2 += region * sides;

                    seen.UnionWith(seenregion);
                }
            }
        }

        GiveAnswer1(total);
        GiveAnswer2(total2);
    }
}
