using Spectre.Console;

namespace PuzzleConsole.Year2022.Day4;

public class Day4 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
        var count = 0;
        var count2 = 0;

        foreach (var line in puzzle)
        {
            var (part1, part2) = GetScore(line);

            count += part1;
            count2 += part2;
        }

        return new[] { count.ToString(), count2.ToString() };
    }

    private (int part1, int part2) GetScore(string line)
    {
        var splitted = line.Split(",")
            .SelectMany(s => s.Split("-"))
            .Select(s => int.Parse(s)).ToArray();

        int part1 = 0;
        int part2 = 0;

        if ((splitted[0] <= splitted[2] && splitted[1] >= splitted[3]) ||
            (splitted[0] >= splitted[2] && splitted[1] <= splitted[3]))
        {
            part1++;
        }

        if (
            splitted[0].Between(splitted[2], splitted[3]) ||
            splitted[1].Between(splitted[2], splitted[3]) ||
            splitted[2].Between(splitted[0], splitted[1]) ||
            splitted[3].Between(splitted[0], splitted[1])
        ) {
            part2++;
        }

        return (part1, part2);
    }
}

static class BetweenExtension
{
    public static bool Between(this int i, int a, int b)
    {
        return (i >= a && i <= b);
    }
}