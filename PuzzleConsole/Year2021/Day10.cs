using System.Reactive.Linq;

namespace PuzzleConsole.Year2021;

public class Day10 : ISolver
{
    public string[] Solve(string[] puzzleInput)
    {
        var lines= puzzleInput.Select(line => isCorupt(line));
        var linesScore = lines.Where(r => r.res)
            .Select(r => r.s switch {
                ")" => 3,
                "]" => 57,
                "}" => 1197,
                ">" => 25137,
                _ => throw new NotSupportedException($"{r.s} is Not supported")
            }).Sum();

        var incompplete = lines.Where(r => !r.res)
            .Select(r => r.s)
            .Select(r => new {line = r, score = calcScore(r)})
            .Select(r => { Console.WriteLine($"{r.line} geeft: {r.score}"); return r.score;})
            .ToList();

        incompplete.Sort();
        Console.WriteLine(incompplete.Count());
        double h = incompplete.Count() / 2;
        int half = (int)Math.Ceiling(h);
        var answer = incompplete[half];

        return new string[] { linesScore.ToString(), answer.ToString()};
    }

    public decimal calcScore(string input)
    {
        decimal score = 0;
        foreach (var c in input) {
            score = score * 5;
            score += c switch {
                ')' => 1,
                ']' => 2,
                '}'=> 3,
                '>' => 4,
                _ => throw new NotSupportedException($"{c} is Not supported")
            };
        }
        return score;
    }

    public (bool res, string s) isCorupt(string line)
    {
        var stack = new Stack<string>();

        var open = new string[] { "[", "{", "(", "<" };
        var close = new string[] { "]", "}", ")", ">" };

        for (int i = 0; i < line.Length; i++)
        {
            var s = line[i].ToString();
            if (open.Contains(s))
            {
                stack.Push(s switch {
                    "{" => "}",
                    "<" => ">",
                    "[" => "]",
                    "(" => ")",
                    _ => throw new NotSupportedException($"{s} is Not supported")
                });
            }
            else
            {
                var pop = stack.Pop();
                if (pop != s)
                {
                    return new (true, s);
                }
            }
        }
        return new (false, string.Concat(stack));
    }
}
