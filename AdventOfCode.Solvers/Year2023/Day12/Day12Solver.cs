using System.Runtime.InteropServices;

namespace AdventOfCode.Solvers.Year2023.Day12;

public class Day12Solver : BaseSolver
{
    private readonly Dictionary<string, double> _cache = new();
    private double cacheHit = 0;
    public override void Solve(string[] puzzle)
    {
        double answer = 0;
        double answer2 = 0;
        foreach(var line in puzzle)
        {
            var parts = line.Split(' ');
            var numbers = parts[1].Split(',').Select(int.Parse).ToArray();

            var ans = solveRecursive(parts[0], null, numbers);
            answer += ans;

            var bigline =
                $"{parts[0]}?{parts[0]}?{parts[0]}?{parts[0]}?{parts[0]} {parts[1]},{parts[1]},{parts[1]},{parts[1]},{parts[1]}";
            parts = bigline.Split(' ');
            numbers = parts[1].Split(',').Select(int.Parse).ToArray();

            var ans2 = solveRecursive(parts[0], null, numbers);
            answer2 += ans2;

            logger.OnNext($"{line} - answer: {ans} answer3: {ans2}");
        }

        GiveAnswer1(answer);
        GiveAnswer2(answer2);
    }

    public double solveRecursive(string inp, int? remaining, int[] others)
    {
        var key = $"{inp} ({remaining}) {string.Join(',', others)}";
        if (_cache.ContainsKey(key))
        {
            cacheHit++;
            return _cache[key];
        }

        if (string.IsNullOrEmpty(inp) && (remaining != null || others.Length > 0))
        {
            _cache.Add(key, 0);
            return _cache[key];
        }

        if (inp.Length == 1)
        {
            if (inp[0] == '#')
            {
                if ((remaining == 1 && others.Length == 0) || (remaining == null && others.Length == 1 && others[0] == 1))
                {
                    _cache.Add(key, 1);
                    return _cache[key];
                }

                _cache.Add(key, 0);
                return _cache[key];
            }

            if (inp[0] == '.')
            {
                if ((remaining != null && remaining > 0) || others.Length > 0)
                {
                    _cache.Add(key, 0);
                    return _cache[key];
                }

                _cache.Add(key, 1);
                return _cache[key];
            }

            if (
                (remaining == null && others.Length == 0) ||
                (remaining == null && others is [1]) ||
                (remaining is < 2 && others.Length == 0))
            {
                _cache.Add(key, 1);
                return _cache[key];
            }

            _cache.Add(key, 0);
            return _cache[key];

        }

        switch(inp[0])
        {
            case '#':
                if (remaining == null)
                {
                    if (others.Length == 0)
                    {
                        _cache.Add(key, 0);
                        return _cache[key];
                    }
                    remaining = others[0];
                    others = others[1..];
                }

                if (remaining == 0)
                {
                    _cache.Add(key, 0);
                    return _cache[key];
                }

                _cache.Add(key, solveRecursive(inp[1..], remaining - 1, others));
                return _cache[key];
            case '.':
                if (remaining != null &&  remaining > 0)
                {
                    _cache.Add(key, 0);
                    return _cache[key];
                }

                _cache.Add(key, solveRecursive(inp[1..], null, others));
                return _cache[key];
            case '?':
                if (remaining == null)
                {
                    if (others.Length > 0)
                    {
                        _cache.Add(key, solveRecursive(inp[1..], null, others) + solveRecursive(inp[1..], others[0] - 1, others[1..]));
                        return _cache[key];
                    }
                    
                    _cache.Add(key, solveRecursive(inp[1..], null, others));
                    return _cache[key];
                }

                if (remaining == 0)
                {
                    _cache.Add(key, solveRecursive(inp[1..], null, others));
                    return _cache[key];
                }

                _cache.Add(key, solveRecursive(inp[1..], remaining - 1, others));
                return _cache[key];
        }

        throw new Exception("Should not happen");
    }
}
