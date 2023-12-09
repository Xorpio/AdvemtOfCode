using System.ComponentModel.Design;
using System.Diagnostics;
using System.Dynamic;

namespace AdventOfCode.Solvers.Year2023.Day8;

public class Day8Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        var map = new Dictionary<string, (string left, string right)>();

        string key = "";
        foreach (var line in puzzle.Skip(2))
        {
            var parts = line.Split(" = ");
            key = parts[0];
            parts = parts[1].Split(", ");
            var left = parts[0][1..].Trim();
            var right = parts[1][..^1].Trim();

            map.Add(key, (left, right));
        }

        foreach(var m in map)
        {
            logger.OnNext($"{m.Key} = {m.Value}");
        }

        key = "AAA";
        var step = 0;
        var instruction = puzzle[0];
        do
        {
            var i = instruction[step % instruction.Length];
            key = i switch
            {
                'R' => map[key].right,
                'L' => map[key].left,
                _ => throw new Exception("Unknown instruction")
            };

            step++;
        } while(key != "ZZZ");

        GiveAnswer1(step);

        var keys = map.Select(k => k.Key).Where(k => k.EndsWith("A")).ToList();
        foreach(var k in keys)
        {
            logger.OnNext($"{k} = {map[k]}");
        }

        var loops = new Dictionary<double, double>();
        var answer = 1;
        foreach(var k in keys)
        {
            key = k;
            step = 0;
            do
            {
                var i = instruction[step % instruction.Length];

                key = i switch
                {
                    'R' => map[key].right,
                    'L' => map[key].left,
                    _ => throw new Exception("Unknown instruction")
                };

                step++;

            } while(!key.EndsWith("Z"));

            logger.OnNext($"{k} = {step}");
            loops.Add(step, 0);
        }
        var combinations = numbers.SelectMany((x, i) => numbers.Skip(i + 1), (x, y) => (x, y));

        //find biggest common
        var time = new Stopwatch();
        time.Start();
        double c = 1;
        double current = 0;
        do
        {
            var lowest = loops.OrderBy(l => l.Value).First();
            loops[lowest.Key] += lowest.Key;
            current = loops[lowest.Key];

            if (current > c)
            {
                c = c * 10;
                logger.OnNext($"{c} - {current} elapsed {time.Elapsed}. digits: {c.ToString().Length}");
            }
        } while(loops.Any(l => l.Value != current));

        time.Stop();

        GiveAnswer2(loops.First().Value);
    }
}
