using System.Runtime.ExceptionServices;

namespace PuzzleConsole.Year2015.Day6;

public class Day6 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
        var grid = new bool[1000][];
        var gridPart2 = new int[1000][];

        for (var index = 0; index < grid.Length; index++)
        {
            grid[index] = new bool[1000];
            gridPart2[index] = new int[1000];
        }

        foreach (var line in puzzle)
        {
            var command = line.Split(' ');

            var t = command[^1];
            var f = command[^3];

            var from = f.Split(',')
                .Select(l => int.Parse(l))
                .ToArray();
            var to = t.Split(',')
                .Select(l => int.Parse(l))
                .ToArray();

            for (var x = 0; x < grid.Length; x++)
            {
                if (!Between(x, from[0], to[0]))
                {
                    continue;
                }

                for (var y = 0; y < grid[x].Length; y++)
                {
                    if (!Between(y, from[1], to[1]))
                    {
                        continue;
                    }

                    if (command[0] == "toggle")
                    {
                        grid[x][y] = !grid[x][y];
                        gridPart2[x][y] += 2;
                    }
                    else if (command[1] == "on")
                    {
                        grid[x][y] = true;
                        gridPart2[x][y]++;
                    }
                    else
                    {
                        grid[x][y] = false;
                        if (gridPart2[x][y] > 0)
                            gridPart2[x][y]--;
                    }
                }
            }
        }

        var answer = grid.Select(g => g.Where(b => b).Count()).Sum();
        var answerPart2 = gridPart2.Select(g => g.Sum()).Sum();


        return new []{ answer.ToString(), answerPart2.ToString() };
    }

    private bool Between(int item, int start, int end)
    {
        return item >= start && item <= end;
    }
}