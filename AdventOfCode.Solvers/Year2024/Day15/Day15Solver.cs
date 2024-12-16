using System;
using System.Linq.Expressions;

namespace AdventOfCode.Solvers.Year2024.Day15;

public class Day15Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        var parts = string.Join(Environment.NewLine, puzzle)
            .Split(Environment.NewLine + Environment.NewLine);
        var p = parts[0].Split(Environment.NewLine).Select(s => s.ToArray()).ToArray();
        var moves = string.Join("", parts[1].Split(Environment.NewLine));
        // var map = new char[p.Length, p[0].Length];

        (int row, int col) = (0, 0);
        for(int r = 0; r < p.Length; r++)
        {
            for(int c = 0; c < p[0].Length; c++)
            {
                if (p[r][c] == '@')
                {
                    (row, col) = (r, c);
                }
            }
        }

            // logger.OnNext("Initial state");
            // for(int r = 0; r < p.Length; r++)
            // {
            //     string line = "";
            //     for(int c = 0; c < p[0].Length; c++)
            //     {
            //         line += p[r][c];
            //     }
            //     logger.OnNext(line);
            // }
            // logger.OnNext("");

        foreach(var move in moves)
        {
            switch(move)
            {
                case '^':
                    if (p[row][col] == '#')
                    {
                        continue;
                    }
                    if (p[row - 1][col] == '.')
                    {
                        p[row -1][col] = '@';
                        p[row][col] = '.';
                        row--;
                        continue;
                    }
                    if (p[row - 1][col] == 'O')
                    {
                        for(int ir = row -1; ir > 0; ir--)
                        {
                            if (p[ir][col] == '.')
                            {
                                p[ir][col] = 'O';
                                p[row][col] = '.';
                                p[row -1][col] = '@';
                                row--;
                                break;
                            }
                            if (p[ir][col] == '#')
                            {
                                break;
                            }
                        }
                    }
                    break;
                case 'v':
                    if (p[row][col] == '#')
                    {
                        continue;
                    }
                    if (p[row + 1][col] == '.')
                    {
                        p[row + 1][col] = '@';
                        p[row][col] = '.';
                        row++;
                        continue;
                    }
                    if (p[row + 1][col] == 'O')
                    {
                        for(int ir = row + 1; ir < p.Length; ir++)
                        {
                            if (p[ir][col] == '.')
                            {
                                p[ir][col] = 'O';
                                p[row][col] = '.';
                                p[row + 1][col] = '@';
                                row++;
                                break;
                            }
                            if (p[ir][col] == '#')
                            {
                                break;
                            }
                        }
                    }
                    break;
                case '<':
                    if (p[row][col] == '#')
                    {
                        continue;
                    }
                    if (p[row][col - 1] == '.')
                    {
                        p[row][col - 1] = '@';
                        p[row][col] = '.';
                        col--;
                        continue;
                    }
                    if (p[row][col - 1] == 'O')
                    {
                        for(int ic = col - 1; ic > 0; ic--)
                        {
                            if (p[row][ic] == '.')
                            {
                                p[row][ic] = 'O';
                                p[row][col] = '.';
                                p[row][col - 1] = '@';
                                col--;
                                break;
                            }
                            if (p[row][ic] == '#')
                            {
                                break;
                            }
                        }
                    }
                    break;
                case '>':
                    if (p[row][col] == '#')
                    {
                        continue;
                    }
                    if (p[row][col + 1] == '.')
                    {
                        p[row][col + 1] = '@';
                        p[row][col] = '.';
                        col++;
                        continue;
                    }
                    if (p[row][col + 1] == 'O')
                    {
                        for(int ic = col + 1; ic < p[0].Length; ic++)
                        {
                            if (p[row][ic] == '.')
                            {
                                p[row][ic] = 'O';
                                p[row][col] = '.';
                                p[row][col + 1] = '@';
                                col++;
                                break;
                            }
                            if (p[row][ic] == '#')
                            {
                                break;
                            }
                        }
                    }
                    break;
                default:
                    throw new Exception("Invalid move");
            }
            // logger.OnNext($"Move: {move}:");
            // for(int r = 0; r < p.Length; r++)
            // {
            //     string line = "";
            //     for(int c = 0; c < p[0].Length; c++)
            //     {
            //         line += p[r][c];
            //     }
            //     logger.OnNext(line);
            // }
            // logger.OnNext("");
        }

        var total = 0;

        for(int r = 0; r < p.Length; r++)
        {
            string line = "";
            for(int c = 0; c < p[0].Length; c++)
            {
                if (p[r][c] == 'O')
                {
                    total += (r * 100) + c;
                }

                line += p[r][c];
            }
            logger.OnNext(line);
        }
        GiveAnswer1(total);

        p = parts[0].Split(Environment.NewLine).Select(s => s.ToArray()).ToArray();
        var bigmap = new List<string>();

        for(int r = 0; r < p.Length; r++)
        {
            string line = "";
            for(int c = 0; c < p[0].Length; c++)
            {
                if (p[r][c] == 'O')
                {
                    line += "[]";
                }
                else if (p[r][c] == '@')
                {
                    line += "@.";
                }
                else if (p[r][c] == '#')
                {
                    line += "##";
                }
                else
                {
                    line += "..";
                }
            }
            bigmap.Add(line);
        }

        p = bigmap.Select(s => s.ToArray()).ToArray();

        for(int r = 0; r < p.Length; r++)
        {
            string line = "";
            for(int c = 0; c < p[0].Length; c++)
            {
                line += p[r][c];
            }
            // logger.OnNext(line);
        }

        for(int r = 0; r < p.Length; r++)
        {
            for(int c = 0; c < p[0].Length; c++)
            {
                if (p[r][c] == '@')
                {
                    (row, col) = (r, c);
                }
            }
        }
        
        char[][] backup;
        foreach(var move in moves)
        {
            switch(move)
            {
                case '^':
                    if (p[row - 1][col] == '#')
                    {
                        break;
                    }
                    if (p[row - 1][col] == '.')
                    {
                        p[row -1][col] = '@';
                        p[row][col] = '.';
                        row--;
                        break;
                    }
                    // Create a deep copy of the 2D array
                    backup = p.Select(row => row.ToArray()).ToArray();
                    MoveBox(p, row - 1, col, row - 2, col);
                    if (p[row - 1][col] == '.')
                    {
                        p[row -1][col] = '@';
                        p[row][col] = '.';
                        row--;
                    }
                    else
                    {
                        p = backup;
                    }
                    break;
                case 'v':
                    if (p[row + 1][col] == '#')
                    {
                        break;
                    }
                    if (p[row + 1][col] == '.')
                    {
                        p[row + 1][col] = '@';
                        p[row][col] = '.';
                        row++;
                        break;
                    }
                    backup = p.Select(row => row.ToArray()).ToArray();
                    MoveBox(p, row + 1, col, row + 2, col);
                    if (p[row + 1][col] == '.')
                    {
                        p[row + 1][col] = '@';
                        p[row][col] = '.';
                        row++;
                    }
                    else
                    {
                        p = backup;
                    }
                    break;
                case '<':
                    if (p[row][col - 1] == '#')
                    {
                        break;
                    }
                    if (p[row][col - 1] == '.')
                    {
                        p[row][col - 1] = '@';
                        p[row][col] = '.';
                        col--;
                        break;
                    }
                    backup = p.Select(row => row.ToArray()).ToArray();
                    MoveBox(p, row, col - 1, row, col - 2);
                    if (p[row][col - 1] == '.')
                    {
                        p[row][col - 1] = '@';
                        p[row][col] = '.';
                        col--;
                    }
                    else
                    {
                        p = backup;
                    }
                    break;

                case '>':
                    if (p[row][col + 1] == '#')
                    {
                        break;
                    }
                    if (p[row][col + 1] == '.')
                    {
                        p[row][col + 1] = '@';
                        p[row][col] = '.';
                        col++;
                        break;
                    }
                    backup = p.Select(row => row.ToArray()).ToArray();
                    MoveBox(p, row, col + 1, row, col + 2);
                    if (p[row][col + 1] == '.')
                    {
                        p[row][col + 1] = '@';
                        p[row][col] = '.';
                        col++;
                    }
                    break;
            }

            logger.OnNext($"Move: {move}:");
            for(int r = 0; r < p.Length; r++)
            {
                string line = "";
                for(int c = 0; c < p[0].Length; c++)
                {
                    line += p[r][c];
                }
                logger.OnNext(line);
            }
            logger.OnNext("");
        }

        total = 0;

        for(int r = 0; r < p.Length; r++)
        {
            string line = "";
            for(int c = 0; c < p[0].Length; c++)
            {
                if (p[r][c] == '[')
                {
                    total += (r * 100) + c;
                }

                line += p[r][c];
            }
            // logger.OnNext(line);
        }

        GiveAnswer2(total);
    }

    private void MoveBox(char[][] p, int rowstart, int colstart, int rowend, int colend)
    {
        if (p[rowstart][colstart] == '.')
            return;
        //move up/down
        if (colstart == colend)
        {
            if (p[rowstart][colstart] == '[')
            {
                MoveBox(p, rowend, colend, rowend + (rowend - rowstart), colend);
                MoveBox(p, rowend, colend + 1, rowend + (rowend - rowstart), colend + 1);

                if (p[rowend][colend] != '.' || p[rowend][colend + 1] != '.')
                {
                    return;
                }
                p[rowend][colend] = '[';
                p[rowend][colend + 1] = ']';
                p[rowstart][colstart] = '.';
                p[rowstart][colstart + 1] = '.';
                return;
            }
            
            if (p[rowstart][colstart] == ']')
            {
                MoveBox(p, rowend, colend, rowend + (rowend - rowstart), colend);
                MoveBox(p, rowend, colend - 1, rowend + (rowend - rowstart), colend - 1);

                if (p[rowend][colend] != '.' || p[rowend][colend - 1] != '.')
                {
                    return;
                }
                p[rowend][colend] = ']';
                p[rowend][colend - 1] = '[';
                p[rowstart][colstart] = '.';
                p[rowstart][colstart - 1] = '.';
                return;
            }
        }

        //move left/right
        if (colend > colstart)
        {
            if (p[rowstart][colstart] != '[')
                return;
            MoveBox(p, rowend, colend + 1, rowend, colend + 2);
            if (p[rowend][colend + 1] != '.')
            {
                return;
            }
            p[rowend][colend] = '[';
            p[rowend][colend + 1] = ']';
            p[rowstart][colstart] = '.';
            return;
        }
        
        if (colend < colstart)
        {
            if (p[rowstart][colstart] != ']')
                return;

            MoveBox(p, rowend, colend - 1, rowend, colend - 2);
            if (p[rowend][colend - 1] != '.')
            {
                return;
            }
            p[rowend][colend] = ']';
            p[rowend][colend - 1] = '[';
            p[rowstart][colstart] = '.';
            return;        
        }
    }
}
