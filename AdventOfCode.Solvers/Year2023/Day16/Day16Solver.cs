using System.Data;

namespace AdventOfCode.Solvers.Year2023.Day16;

public class Day16Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        var answer1 = SolveMaze(puzzle, 0, -1, '>');

        var max = 0;
        int maxRow = puzzle.Length;
        int maxCol = puzzle[0].Length;

        for(int rowStart = 0; rowStart < maxRow; rowStart++)
        {
            var a = SolveMaze(puzzle, rowStart, -1, '>');
            if (a > max)
            {
                max = a;
            }
            a = SolveMaze(puzzle, rowStart, puzzle[0].Length, '<');
            if (a > max)
            {
                max = a;
            }
        }

        for (int colStart = 0; colStart < maxCol; colStart++)
        {
            var a = SolveMaze(puzzle, -1, colStart, 'v');
            if (a > max)
            {
                max = a;
            }
            a = SolveMaze(puzzle, puzzle.Length, colStart, '^');
            if (a > max)
            {
                max = a;
            }
        }

        GiveAnswer1(answer1.ToString());
        GiveAnswer2(max);
    }

    public int SolveMaze(string[] puzzle, int rowstart, int colStart, char dirStart)
    {
        Queue<(int row, int col, char dir)> queue = [];
        HashSet<(int row, int col)> visited = [];
        HashSet<(int row, int col, char dir)> done = [];

        queue.Enqueue((rowstart, colStart, dirStart));

        int maxRow = puzzle.Length;
        int maxCol = puzzle[0].Length;

        do
        {
            var next = queue.Dequeue();

            if (done.Contains(next))
            {
                continue;
            }

            done.Add(next);

            var (row, col) = (next.dir) switch
            {
                '>' => (next.row, next.col + 1),
                '<' => (next.row, next.col - 1),
                '^' => (next.row - 1, next.col),
                'v' => (next.row + 1, next.col),
                _ => throw new NotImplementedException()
            };

            if (row < 0 || row >= maxRow || col < 0 || col >= maxCol)
            {
                continue;
            }

            visited.Add((row, col));

            if (puzzle[row][col] == '.')
            {
                queue.Enqueue((row, col, next.dir));
            }
            else if (puzzle[row][col] == '\\')
            {
                queue.Enqueue((row, col, next.dir switch
                {
                    '>' => 'v',
                    '<' => '^',
                    '^' => '<',
                    'v' => '>',
                    _ => throw new NotImplementedException()
                }));
            }
            else if (puzzle[row][col] == '/')
            {
                queue.Enqueue((row, col, next.dir switch
                {
                    '>' => '^',
                    '<' => 'v',
                    '^' => '>',
                    'v' => '<',
                    _ => throw new NotImplementedException()
                }));
            }
            else if (puzzle[row][col] == '-')
            {
                if (next.dir == '>' || next.dir == '<')
                {
                    queue.Enqueue((row, col, next.dir));
                }
                else
                {
                    queue.Enqueue((row, col, '<'));
                    queue.Enqueue((row, col, '>'));
                }
            }
            else if (puzzle[row][col] == '|')
            {
                if (next.dir == '^' || next.dir == 'v')
                {
                    queue.Enqueue((row, col, next.dir));
                }
                else
                {
                    queue.Enqueue((row, col, '^'));
                    queue.Enqueue((row, col, 'v'));
                }
            }

            if (visited.Count > puzzle.Sum(l => l.Length))
            {
                throw new Exception("Infinite loop detected");
            }
        } while (queue.Count > 0);

        return visited.Count;
    }
}
