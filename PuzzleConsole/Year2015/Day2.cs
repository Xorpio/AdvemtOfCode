namespace PuzzleConsole.Year2015.Day2;

public class Day2 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
        var answer = puzzle
            .Select(l => SolveLine(l))
            .ToArray()
            .Sum()
            .ToString();
        
        var answerRibbon = puzzle
            .Select(l => SolveRibbon(l))
            .ToArray()
            .Sum()
            .ToString();

        return new string[] { answer, answerRibbon };
    }

    public int SolveLine(string s)
    {
        var dimensions = s.Split('x')
            .Select(s => int.Parse(s)).ToArray();

        var a = dimensions[0] * dimensions[1];
        var b = dimensions[0] * dimensions[2];
        var c = dimensions[1] * dimensions[2];

        var lowest = (new int[] { a, b, c }).Min();

        return (new int[] { a, a, b, b, c, c, lowest }).Sum();
    }

    public int SolveRibbon(string s)
    {
        var dimensions = s.Split('x')
            .Select(s => int.Parse(s))
            .OrderBy(l => l)
            .ToArray();

        return (new int[]
        {
            dimensions[0],
            dimensions[0],
            dimensions[1],
            dimensions[1],
            dimensions[0] * dimensions[1] * dimensions[2]
        }).Sum();
    }
}