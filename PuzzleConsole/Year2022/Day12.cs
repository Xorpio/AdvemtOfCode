using Spectre.Console;

namespace PuzzleConsole.Year2022.Day12;

public class Day12 : ISolver
{
    private Dictionary<Coords, char> maze;
    public string[] Solve(string[] puzzle)
    {
        maze = new Dictionary<Coords, char>();

        for (var row = 0; row < puzzle.Length; row++)
        {
            var line = puzzle[row];
            for (var col = 0; col < line.Length; col++)
            {
                var c = line[col];
                maze.Add(new Coords(row,col), c);
            }
        }

        var attempts = new Queue<HashSet<Coords>>();

        var start = maze.FirstOrDefault(c => c.Value == 'S').Key;
        var end = maze.FirstOrDefault(c => c.Value == 'E').Key;

        var first = new HashSet<Coords>();
        first.Add(start);

        return new[] { FindPath(first, end).ToString() };

        // while (attempts.TryDequeue(out var set))
        // {
        //     var last = set.Last();
        //     //north
        //     var north = last with { row = last.row - 1 };
        //     if (TryAddAttempt(maze, north, last, set, attempts))
        //     {
        //         return new string[] { (set.Count).ToString() };
        //     }
        //
        //     var east = last with { col = last.col + 1 };
        //     if(TryAddAttempt(maze, east, last, set, attempts))
        //     {
        //         return new string[] { (set.Count).ToString() };
        //     }
        //
        //     var south = last with { row = last.row + 1 };
        //     if(TryAddAttempt(maze, south, last, set, attempts))
        //     {
        //         return new string[] { (set.Count).ToString() };
        //     }
        //
        //     var west = last with { col = last.col - 1 };
        //     if(TryAddAttempt(maze, west, last, set, attempts))
        //     {
        //         return new string[] { (set.Count).ToString() };
        //     }
        //
        //     Console.WriteLine($"Set: {set.Count} -  Cycle: {count}");
        //
        //     count++;
        // }

    }

    public int FindPath(HashSet<Coords> path, Coords end)
    {
        var last = path.Last();

        Console.WriteLine ($"Depth: {path.Count}, {last}");

        var newCoords = new Coords[]
        {
            last with { row = last.row - 1 },
            last with { col = last.col + 1 },
            last with { row = last.row + 1 },
            last with { col = last.col - 1 },
        };

        newCoords = newCoords.OrderBy(a => a + end).ToArray();

        var results = new HashSet<int>();
        foreach (var cord in newCoords)
        {
            var newset = new HashSet<Coords>(path);
            if (TryAddAttempt(cord, last, newset))
            {
                if (cord == end)
                {
                    return newset.Count - 1;
                }

                var res = FindPath(newset, end);

                if (res < int.MaxValue)
                    return res;
            }
        }

        return int.MaxValue;
    }

    private bool TryAddAttempt(Coords newCoords, Coords last, HashSet<Coords> set)
    {
        if (!maze.ContainsKey(newCoords))
        {
            return false;
        }

        if ((GetVal(maze[last]) + 1) >= GetVal(maze[newCoords]))
        {
            return set.Add(newCoords);
        }

        return false;
    }

    public int GetVal(char c) => c switch
    {
        'S' => GetVal('a'),
        'E' => GetVal('z'),
        _ => (int)c
    };

}

public record Coords
{
    public Coords(int row, int col)
    {
        this.row = row;
        this.col = col;
    }

    public int row { get; init; }
    public int col { get; init; }

    public void Deconstruct(out int row, out int col)
    {
        row = this.row;
        col = this.col;
    }

    public static int operator +(Coords a, Coords b)
    {
        return Math.Abs((a.col - b.col)) +
               Math.Abs((a.row - b.row));
    }
}