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

        var start = maze.FirstOrDefault(c => c.Value == 'S').Key;
        var end = maze.FirstOrDefault(c => c.Value == 'E').Key;

        var part1 = FindPath(start, end);

        var starts = maze.Where(c => c.Value == 'a');

        return new[]
        {
            part1.ToString(),
            starts.Select(s => FindPath(s.Key, end))
                .OrderBy(c => c)
                .First()
                .ToString()
        };
    }

    private int FindPath(Coords start, Coords end)
    {
        var visited = new Dictionary<Coords, int>();
        var potential = new List<(Coords c, int Steps)>();

        potential.Add(new(start, 0));

        while (potential.Any())
        {
            var current = potential.OrderBy(i => i.Steps + (i.c + end))
                .First();
            potential.Remove(current);

            if (current.c == end)
            {
                return current.Steps;
            }

            if (!visited.TryAdd(current.c, current.Steps))
            {
                continue;
            }

            var directions = new List<Coords>()
            {
                current.c with { row = current.c.row - 1 },
                current.c with { col = current.c.col + 1 },
                current.c with { row = current.c.row + 1 },
                current.c with { col = current.c.col - 1 }
            };

            potential.AddRange(directions
                .Where(d => !visited.ContainsKey(d))
                .Where(d => TryAddAttempt(d, current.c))
                .Select(d => (d, current.Steps + 1)));
        }

        return int.MaxValue;
    }

    private bool TryAddAttempt(Coords newCoords, Coords last)
    {
        if (!maze.ContainsKey(newCoords))
        {
            return false;
        }

        if ((GetVal(maze[last]) + 1) >= GetVal(maze[newCoords]))
        {
            return true;
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