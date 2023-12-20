using System.Reflection.Emit;
using Microsoft.VisualBasic.FileIO;

namespace AdventOfCode.Solvers.Year2023.Day15;

public class Day15Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        var parts = puzzle[0].Split(",");
        double answer1 = 0;

        List<List<(string Label, int focus)>> boxes = Enumerable.Range(0, 256).Select(_ => new List<(string, int)>()).ToList();

        foreach (var part in parts)
        {
            answer1 += Hash(part);

            var disciminator = part.Last();
            var label = disciminator  == '-' ? part[..^1] : part[..^2];
            var box = Hash(label);

            logger.OnNext($"{part} {label} {Hash(label)}");

            if (disciminator == '-')
            {
                logger.OnNext($"remevong {label} from {box}");
                var lens = boxes[box].FirstOrDefault(x => x.Label == label);
                if (lens != default)
                {
                    boxes[box].Remove(lens);
                }
            }
            else
            {
                var lens = boxes[box].FirstOrDefault(x => x.Label == label);
                var focus = int.Parse(part[^1].ToString());
                if (lens == default)
                {
                    logger.OnNext($"adding {label} to {box} with focus {focus}");
                    boxes[box].Add((label, focus));
                }
                else
                {
                    logger.OnNext($"replacing {label} to {box} with focus {focus}");
                    var index = boxes[box].IndexOf(lens);
                    boxes[box][index] = (label, focus);
                }
            }

        }

        double answer2 = 0;
        for(int box = 0; box < boxes.Count; box++)
        {
            for(int lens = 0; lens < boxes[box].Count; lens++)
            {
                answer2 += (1 + box) * (lens + 1) * boxes[box][lens].focus;
            }
        }

        GiveAnswer1(answer1);
        GiveAnswer2(answer2);
    }

    public int Hash(string inp)
    {
        int current = 0;
        foreach(var c in inp)
        {
            var charVal = (int)c;
            current += charVal;
            current *= 17;
            current %= 256;
        }

        return current;
    }
}
