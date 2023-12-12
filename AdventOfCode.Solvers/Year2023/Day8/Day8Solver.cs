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

        //find least common multiple between loops
        GiveAnswer2(FindLeastCommonMultiple(loops.Keys.ToList()));
        return;

    }

    public double FindLeastCommonMultiple(List<double> numbers)
    {
        double lcm = numbers[0];

        for (int i = 1; i < numbers.Count; i++)
        {
            lcm = CalculateLCM(lcm, numbers[i]);
        }

        return lcm;
    }

    private double CalculateLCM(double a, double b)
    {
        return (a * b) / CalculateGCD(a, b);
    }

    private double CalculateGCD(double a, double b)
    {
        while (b != 0)
        {
            double remainder = a % b;
            a = b;
            b = remainder;
        }

        return a;
    }
}
