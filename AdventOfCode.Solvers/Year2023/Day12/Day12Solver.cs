using System.Runtime.InteropServices;

namespace AdventOfCode.Solvers.Year2023.Day12;

public class Day12Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        var answer = 0;
        double answer2 = 0;
        foreach(var line in puzzle)
        {
            var ans = solveLine(line);
            answer += ans;

            var ans2 = solveLineSmart(line);

            answer2 += ans2;

            logger.OnNext($"{line} - answer: {ans} answer2: {ans2}");
        }
        GiveAnswer1(answer);
        GiveAnswer2(answer2);
    }

    public int solveLine(string line)
    {
        var parts = line.Split(' ');
        var inp = parts[0];
        var springs = parts[1].Split(',').Select(int.Parse).ToArray();

        var pattern = new List<int>();
        int count = 0;

        var lines = createPattern(springs, inp.Length);
        // lines = lines.Select(l => l[0..inp.Length]).Distinct().ToArray();
        foreach(var l in lines)
        {
            var correct = true;
            for(var i = 0; i < inp.Length; i++)
            {
                if(inp[i] == '.' && l[i] == '#')
                {
                    correct = false;
                    break;
                }

                if (inp[i] == '#' && l[i] == '.')
                {
                    correct = false;
                    break;
                }
            }

            if(correct)
            {
                count++;
            }
        }

        return count;
    }
    public double solveLineSmart(string line)
    {
        var parts = line.Split(' ');
        double first = solveLine($"{parts[0]}? {parts[1]}");
        double middle = solveLine($"?{parts[0]}? {parts[1]}");
        double end = solveLine($"?{parts[0]} {parts[1]}");

        logger.OnNext($"first: {first} middle: {middle} end: {end}");

        if (parts[0].EndsWith("#"))
        {
            return first * first * first * first * first;
        }

        return first * end * end * end * end;
    }
    
    private IEnumerable<string> createPattern(int[] pattern, int maxLength)
    {
        var patterns = new List<string>();
        var l = pattern[0];

    	var r = pattern[1..].Sum() + pattern[1..].Length - 1;

        for(var i = 0; i < maxLength - r - pattern[0]; i++)
        {
            string p = "".PadRight(i, '.') + "".PadRight(pattern[0], '#');

            if (pattern.Length > 1)
            {
                var nested = createPattern(pattern[1..], maxLength - p.Length - 1);
                patterns.AddRange(nested.Select(n => (p + '.' + n)));
            }
            else
            {
                patterns.Add(p.PadRight(maxLength, '.'));
            }
        }

        return patterns.ToArray();
    }
}
