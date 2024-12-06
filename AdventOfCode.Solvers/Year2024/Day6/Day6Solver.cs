namespace AdventOfCode.Solvers.Year2024.Day6;

public class Day6Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        HashSet<(int row, int col, char x)> visited = new();
        (int row, int col) Startposition = (0, 0);

        for (int i = 0; i < puzzle.Length; i++)
        {
            if (puzzle[i].Contains("^"))
            {
                Startposition = (i, puzzle[i].IndexOf("^"));
                break;
            }
        }

        char direction = '^';
        try
        {
            var position = Startposition;
            while (true)
            {
                visited.Add((position.row, position.col, puzzle[position.row][position.col]));

                switch (direction)
                {
                    case '^':
                        if (puzzle[position.row - 1][position.col] == '#')
                        {
                            direction = '>';
                        }
                        else
                            position.row--;
                        break;
                    case '>':
                        if (puzzle[position.row][position.col + 1] == '#')
                        {
                            direction = 'v';
                        }
                        else
                            position.col++;
                        break;
                    case 'v':
                        if (puzzle[position.row + 1][position.col] == '#')
                        {
                            direction = '<';
                        }
                        else
                            position.row++;
                        break;
                    case '<':
                        if (puzzle[position.row][position.col - 1] == '#')
                        {
                            direction = '^';
                        }
                        else
                            position.col--;
                        break;
                    default:
                        throw new Exception("Invalid direction");
                }
            }
        }
        catch (Exception e)
        {
            GiveAnswer1(visited.Count);
        }

        var count = 0;
        foreach (var loc in visited)
        {
            if ((loc.row, loc.col) == Startposition)
                continue;
            try
            {
                var visited2 = new HashSet<(int row, int col, char x)>();
                var puzzleCopy = puzzle.Select(x => x).ToArray();
                puzzleCopy[loc.row] = puzzleCopy[loc.row].Substring(0, loc.col) + '#' + puzzleCopy[loc.row].Substring(loc.col + 1);
                var position = Startposition;
                direction = '^';

                while (true)
                {
                    if (visited2.Contains((position.row, position.col, direction)))
                    {
                        break;
                    }
                    visited2.Add((position.row, position.col, direction));

                    switch (direction)
                    {
                        case '^':
                            if (puzzleCopy[position.row - 1][position.col] == '#')
                            {
                                direction = '>';
                            }
                            else
                                position.row--;
                            break;
                        case '>':
                            if (puzzleCopy[position.row][position.col + 1] == '#')
                            {
                                direction = 'v';
                            }
                            else
                                position.col++;
                            break;
                        case 'v':
                            if (puzzleCopy[position.row + 1][position.col] == '#')
                            {
                                direction = '<';
                            }
                            else
                                position.row++;
                            break;
                        case '<':
                            if (puzzleCopy[position.row][position.col - 1] == '#')
                            {
                                direction = '^';
                            }
                            else
                                position.col--;
                            break;
                        default:
                            throw new Exception("Invalid direction");
                    }
                }
                count++;
            }
            catch (Exception e)
            {
                //do nothing
            }

        }
        GiveAnswer2(count);
    }
}
