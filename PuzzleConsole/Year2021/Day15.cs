namespace PuzzleConsole.Year2021.Day15;

public class Day15 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
        return puzzle;
    }

    public int FindPath(int[,] puzzle, int x, int y, List<string> visited)
    {
        visited.Add($"{x},{y}");

        if (x == puzzle.GetLength(0) - 1 && y == puzzle.GetLength(1))
        {
            return puzzle[x, y];
        }

        var look = new List<(int x, int y)>();
        if (x > 0 && !visited.Contains($"{x -1}.{y}"))
        {
            look.Add(new(x - 1, y));
        }
        if (y > 0 && !visited.Contains($"{x}.{y - 1}"))
        {
            look.Add(new(x, y - 1));
        }
        if (x < puzzle.GetLength(0) && !visited.Contains($"{x + 1}.{y}"))
        {
            look.Add(new(x + 1, y));
        }
        if (y < puzzle.GetLength(1) && !visited.Contains($"{x}.{y + 1}"))
        {
            look.Add(new(x, y + 1));
        }

        var answers = new List<int>();
        foreach (var l in look)
        {
            answers.Add(FindPath(puzzle, l.x, l.y, visited));
        }

        return answers.Min();
    }
}
