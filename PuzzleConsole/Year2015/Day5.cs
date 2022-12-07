using System.Text.RegularExpressions;

namespace PuzzleConsole.Year2015.Day5;

public class Day5 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
        var count = puzzle.Select(p => SolveLine(p))
            .Where(b => b)
            .Count();

        var count2 = puzzle.Select(p => SolveLinePart2(p))
            .Where(b => b)
            .Count();

        return new[] { count.ToString(), count2.ToString() };
    }

    public bool SolveLine(string line)
    {
        if (
            line.Contains("ab")
            || line.Contains("cd")
            || line.Contains("pq")
            || line.Contains("xy")
        )
        {
            return false;
        }

        var check = false;
        for (var index = 1; index < line.Length; index++)
        {
            var range = line[(index - 1)..(index + 1)];
            if (range[0] == range[1])
            {
                check = true;
            }
        }

        if (!check)
        {
            return false;
        }

        var vowels = new char[] { 'a', 'e', 'i', 'o', 'u' };

        return line.Where(c => vowels.Contains(c)).Count() > 2;
    }

    public bool SolveLinePart2(string line)
    {
        var regexes = new Regex[]
        {
            new Regex("(..).*\\1"),
            new Regex("(.).\\1")
        };

        return regexes.All(r => r.IsMatch(line));

    }
}