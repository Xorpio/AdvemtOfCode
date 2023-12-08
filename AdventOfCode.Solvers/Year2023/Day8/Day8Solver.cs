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
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        step = 0;
        do
        {
            var i = instruction[step % instruction.Length];
            var temp = new List<string>();

            foreach(var k in keys)
            {
                temp.Add(i switch
                {
                    'R' => map[k].right,
                    'L' => map[k].left,
                    _ => throw new Exception("Unknown instruction")
                });
            }

            keys = temp;
            step++;

            if (step % 100000 == 0)
            {
                logger.OnNext($"{step} {stopwatch.Elapsed}");
            }
        } while(!keys.All(k =>k.EndsWith("Z")));        


        GiveAnswer2(step);
    }
}
