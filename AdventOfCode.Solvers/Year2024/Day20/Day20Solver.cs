
namespace AdventOfCode.Solvers.Year2024.Day20;

public class Day20Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        var maze = puzzle.Select(x => x.ToCharArray()).ToArray();

        (int row, int col) start = (0, 0);
        (int row, int col) end = (0, 0);

        for (int row = 0; row < maze.Length; row++)
        {
            for (int col = 0; col < maze[row].Length; col++)
            {
                if (maze[row][col] == 'S')
                {
                    start = (row, col);
                }
                if (maze[row][col] == 'E')
                {
                    end = (row, col);
                }
            }
        }

        var basetime = GetTime(maze, start, end);

        var minimal = IsRunningFromUnitTest() ? 50 : 100;

        var cheats = 0;

        for (int row = 1; row < maze.Length - 1; row++)
        {
            for (int col = 1; col < maze[row].Length - 1; col++)
            {
                if (maze[row][col] == '#')
                {
                    var newMaze = maze.Select(x => x.ToArray()).ToArray();
                    newMaze[row][col] = '.';
                    var time = GetTime(newMaze, start, end);

                    var diff = basetime - time;
                    if (diff >= minimal)
                    {
                        cheats++;
                    }
                }
            }
        }

        GiveAnswer1(cheats);
        GiveAnswer2("");
    }

    private int GetTime(char[][] maze, (int row, int col) start, (int row, int col) end)
    {
        var priorityQueue = new PriorityQueue<(int row, int col, int time), int>();

        var visited = new HashSet<(int row, int col)>();
        priorityQueue.Enqueue((start.row, start.col, 0), 0);

        while (priorityQueue.Count > 0)
        {
            var next = priorityQueue.Dequeue();
            visited.Add((next.row, next.col));

            if (next.row == end.row && next.col == end.col)
            {
                return next.time;
            }

            foreach (var (row, col) in new (int row, int col)[] { (0, 1), (0, -1), (1, 0), (-1, 0) })
            {
                var newRow = next.row + row;
                var newCol = next.col + col;
                if (maze[newRow][newCol] == '#')
                {
                    continue;
                }

                if (visited.Contains((newRow, newCol)))
                {
                    continue;
                }
                priorityQueue.Enqueue((newRow, newCol, next.time + 1), next.time + 1);
            }
        }

        logger.OnNext("No path found");
        return -1;
    }
}
