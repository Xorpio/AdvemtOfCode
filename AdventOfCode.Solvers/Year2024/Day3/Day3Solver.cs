using System.Text.RegularExpressions;

namespace AdventOfCode.Solvers.Year2024.Day3;

public class Day3Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        decimal x = 0;

        var input = string.Join(" ", puzzle);
        var match = Regex.Matches(input, @"(mul\(\d{1,3},\d{1,3}\))");

        foreach (var group in match)
        {
            var split = group.ToString()[4..].Split(',');
            var a = int.Parse(split[0]);
            var b = int.Parse(split[1][..^1]);
            x += a * b;
        }
        GiveAnswer1(x);

        GiveAnswer2(part2(input));
    }

    public int part2(string input)
    {
        var mulLoc = input.IndexOf("mul");
        var dontLoc = input.IndexOf("don't()");
        return 0;
    }
}
