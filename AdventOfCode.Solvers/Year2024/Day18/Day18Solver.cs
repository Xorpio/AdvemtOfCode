namespace AdventOfCode.Solvers.Year2024.Day18;

public class Day18Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        var from = IsRunningFromUnitTest() ? 12 : 1024;

        GiveAnswer1(CalcScore(puzzle[..from]));

        while (true)
        {
            from++;
            var score = CalcScore(puzzle[..from]);
            if (score == 0)
            {
                break;
            }
        }

        GiveAnswer2(puzzle[from - 1]);
    }
    public int CalcScore(string[] puzzle)
    {
        (int maxRow, int maxCol) = IsRunningFromUnitTest()
            ? (6, 6)
            : (70, 70);


        var grid = new bool[maxRow + 1, maxCol + 1];

        foreach (var line in puzzle)
        {
            var coords = line.Split(",").Select(int.Parse).ToArray();
            grid[coords[1], coords[0]] = true;
        }

        var queue = new PriorityQueue<(int row, int col, int score), int>();
        queue.Enqueue((0, 0, 0), 0);

        var visited = new HashSet<(int row, int col)>();

        while (queue.Count > 0)
        {
            var next = queue.Dequeue();
            if (next.row == maxRow && next.col == maxCol)
            {
                return next.score;
            }

            if (visited.Contains((next.row, next.col)))
            {
                continue;
            }
            visited.Add((next.row, next.col));

            foreach (var (row, col) in new (int row, int col)[] { (0, 1), (0, -1), (1, 0), (-1, 0) })
            {
                var newRow = next.row + row;
                var newCol = next.col + col;
                if (newRow < 0 || newRow > maxRow || newCol < 0 || newCol > maxCol)
                {
                    continue;
                }
                if (grid[newRow, newCol])
                {
                    continue;
                }
                queue.Enqueue((newRow, newCol, next.score + 1), next.score + 1);
            }
        }

        return 0;
    }
}
