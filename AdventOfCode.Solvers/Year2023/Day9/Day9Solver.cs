

namespace AdventOfCode.Solvers.Year2023.Day9;

public class Day9Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        double part1 = 0;
        double part2 = 0;
        foreach(var line in puzzle)
        {
            var numbers = line.Split(' ').Select(double.Parse).ToArray();
            var a = solveline(numbers);
            logger.OnNext($"Answer: {line} + {a}");
            part1 += numbers.Last() + a.last;
            part2 += numbers.First() - a.first;
        }
        GiveAnswer1(part1);
        GiveAnswer2(part2);
    }

    private (double first, double last) solveline(double[] numbers)
    {
        var all0 = true;
        double[] intervals = new double[numbers.Length - 1];
        for(var i = 0; i < numbers.Length - 1; i++)
        {
            intervals[i] = numbers[i + 1] - numbers[i];
            if (intervals[i] != 0)
            {
                all0 = false;
            }
        }

        if (all0)
        {
            return (0,0);
        }
        
        var solve = solveline(intervals);

        return (intervals.First() - solve.first, intervals.Last() + solve.last);
    }
}
