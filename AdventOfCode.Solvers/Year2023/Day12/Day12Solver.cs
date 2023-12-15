using System.Runtime.InteropServices;

namespace AdventOfCode.Solvers.Year2023.Day12;

public class Day12Solver : BaseSolver
{
    private readonly Dictionary<string, int> _cache = new();
    private double cacheHit = 0;
    public override void Solve(string[] puzzle)
    {
        var answer = 0;
        double answer2 = 0;
        foreach(var line in puzzle)
        {
            var parts = line.Split(' ');
            var numbers = parts[1].Split(',').Select(int.Parse).ToArray();

            var ans = solveRecursive(parts[0], null, numbers);
            // var ans = solveLine(line);
            answer += ans;

            var bigline =
                $"{parts[0]}?{parts[0]}?{parts[0]}?{parts[0]}?{parts[0]} {parts[1]},{parts[1]},{parts[1]},{parts[1]},{parts[1]}";
            parts = bigline.Split(' ');
            numbers = parts[1].Split(',').Select(int.Parse).ToArray();

            var ans2 = solveRecursive(parts[0], null, numbers);
            // if (ans != ans2)
            //     throw new Exception($"Wrong answer for line {line} answer: {ans} answer2: {ans2}");
            answer2 += ans2;

            logger.OnNext($"{line} - answer: {ans} answer2: {ans2}");
        }

        logger.OnNext($"cache hit: {cacheHit} - {_cache.Count}");

        foreach(var c in _cache)
        {
            logger.OnNext($"{c.Key} - {c.Value}");
        }

        GiveAnswer1(answer);
        GiveAnswer2(answer2);
    }

    public int solveRecursive(string inp, int? remaining, int[] others)
    {
        var key = $"{inp} ({remaining}) {string.Join(',', others)}";
        if (_cache.ContainsKey(key))
        {
            cacheHit++;
            // logger.OnNext($"cache: {key} - value: {_cache[key]}");
            return _cache[key];
        }

        if (string.IsNullOrEmpty(inp) && (remaining != null || others.Length > 0))
        {
            _cache.Add(key, 0);
            // logger.OnNext($"cache: {key} - value: {_cache[key]}");
            return _cache[key];
        }

        if (inp.Length == 1)
        {
            if (inp[0] == '#')
            {
                if ((remaining == 1 && others.Length == 0) || (remaining == null && others.Length == 1 && others[0] == 1))
                {
                    _cache.Add(key, 1);
                    // logger.OnNext($"cache: {key} - value: {_cache[key]}");
                    return _cache[key];
                }

                _cache.Add(key, 0);
                // logger.OnNext($"cache: {key} - value: {_cache[key]}");
                return _cache[key];
            }

            if (inp[0] == '.')
            {
                if ((remaining != null && remaining > 0) || others.Length > 0)
                {
                    _cache.Add(key, 0);
                    // logger.OnNext($"cache: {key} - value: {_cache[key]}");
                    return _cache[key];
                }

                _cache.Add(key, 1);
            // logger.OnNext($"cache: {key} - value: {_cache[key]}");
                return _cache[key];
            }

            if (
                (remaining == null && others.Length == 0) ||
                (remaining == null && others is [1]) ||
                (remaining is < 2 && others.Length == 0))
            {
                _cache.Add(key, 1);
            // logger.OnNext($"cache: {key} - value: {_cache[key]}");
                return _cache[key];
            }

            _cache.Add(key, 0);
            // logger.OnNext($"cache: {key} - value: {_cache[key]}");
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
            // logger.OnNext($"cache: {key} - value: {_cache[key]}");
                        return _cache[key];
                    }
                    remaining = others[0];
                    others = others[1..];
                }

                if (remaining == 0)
                {
                    _cache.Add(key, 0);
            // logger.OnNext($"cache: {key} - value: {_cache[key]}");
                    return _cache[key];
                }

                _cache.Add(key, solveRecursive(inp[1..], remaining - 1, others));
            // logger.OnNext($"cache: {key} - value: {_cache[key]}");
                return _cache[key];
            case '.':
                if (remaining != null &&  remaining > 0)
                {
                    _cache.Add(key, 0);
            // logger.OnNext($"cache: {key} - value: {_cache[key]}");
                    return _cache[key];
                }

                _cache.Add(key, solveRecursive(inp[1..], null, others));
            // logger.OnNext($"cache: {key} - value: {_cache[key]}");
                return _cache[key];
            case '?':
                if (remaining == null)
                {
                    if (others.Length > 0)
                    {
                        _cache.Add(key, solveRecursive(inp[1..], null, others) + solveRecursive(inp[1..], others[0] - 1, others[1..]));
            // logger.OnNext($"cache: {key} - value: {_cache[key]}");
                        return _cache[key];
                    }
                    
                    _cache.Add(key, solveRecursive(inp[1..], null, others));
            // logger.OnNext($"cache: {key} - value: {_cache[key]}");
                    return _cache[key];
                }

                if (remaining == 0)
                {
                    _cache.Add(key, solveRecursive(inp[1..], null, others));
            // logger.OnNext($"cache: {key} - value: {_cache[key]}");
                    return _cache[key];
                }

                _cache.Add(key, solveRecursive(inp[1..], remaining - 1, others));
            // logger.OnNext($"cache: {key} - value: {_cache[key]}");
                return _cache[key];
        }

        throw new Exception("Should not happen");
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

        // logger.OnNext($"first: {first} middle: {middle} end: {end}");

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
