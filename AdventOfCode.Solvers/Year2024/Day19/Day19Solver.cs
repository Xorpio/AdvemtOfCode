using AdventOfCode.Solvers.Year2023.Day18;

namespace AdventOfCode.Solvers.Year2024.Day19;

public class Day19Solver : BaseSolver
{
    public Dictionary<string, decimal> cache = new Dictionary<string, decimal>();
    public override void Solve(string[] puzzle)
    {
        var patterns = new Dictionary<char, IList<string>>();
        var parts = puzzle[0].Split(", ");

        foreach (var part in parts)
        {
            if (!patterns.ContainsKey(part[0]))
                patterns.Add(part[0], new List<string>());

            patterns[part[0]].Add(part);
        }

        decimal done = 0;
        var total = puzzle.Length - 3;
        decimal valid = 0;
        decimal validp2 = 0;
        foreach (var line in puzzle[2..])
        {
            logger.OnNext($"try sole {line}");
            var answer = TrySolve(line, patterns);
            if (answer > 0)
            {
                valid++;
                validp2 += answer;
            }

            logger.OnNext($"{++done}/{total}, Valid: {valid}");
        }

        GiveAnswer1(valid);
        GiveAnswer2(validp2);
    }

    public decimal TrySolve(string towel, Dictionary<char, IList<string>> patterns)
    {
        if (cache.ContainsKey(towel))
            return cache[towel];

        if (patterns.ContainsKey(towel[0]) == false)
        {
            cache[towel] = 0;
            return 0;
        }

        cache[towel] = 0;
        decimal answers = 0;
        foreach (var p in patterns[towel[0]])
        {
            if (p == towel)
            {
                cache[towel] += 1;
            }
            else if (towel.StartsWith(p))
            {
                var sub = TrySolve(towel[p.Length..], patterns);
                if (sub > 0)
                {
                    answers += sub;
                    cache[towel] += sub;
                }
            }
        }

        return cache[towel];
    }
}
