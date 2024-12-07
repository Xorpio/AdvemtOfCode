using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solvers.Year2024.Day3;

public class Day3Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {

        var input = string.Join(" ", puzzle);
        GiveAnswer1(part1(input));

        GiveAnswer2(part2(input));
    }
    public decimal part1(string input)
    {
        decimal x = 0;
        var match = Regex.Matches(input, @"(mul\(\d{1,3},\d{1,3}\))");

        foreach (var group in match)
        {
            var split = group.ToString()[4..].Split(',');
            var a = int.Parse(split[0]);
            var b = int.Parse(split[1][..^1]);
            x += a * b;
        }
        return x;
    }

    public decimal part2(string input)
    {
        decimal count = 0;

        while (input.Contains("don't()"))
        {
            var inpBeforeDont = input[..input.IndexOf("don't()")];
            input = input[input.IndexOf("don't()")..];
            count += part1(inpBeforeDont);
            if (input.Contains("do()"))
                input = input[(input.IndexOf("do()") + 4)..];
        }

        count += part1(input);

        return count;
    }
}
