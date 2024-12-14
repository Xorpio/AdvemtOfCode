using System.Text.RegularExpressions;

namespace AdventOfCode.Solvers.Year2024.Day13;

public class Day13Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        var sum1 = 0;
        decimal sum2 = 0;
        for (int i = 0; i < puzzle.Length; i += 4)
        {
            sum1 += SolvePart1(puzzle[i..(i + 3)]);
            sum2 += SolvePart2(puzzle[i..(i + 3)]);
        }
        GiveAnswer1(sum1);
        GiveAnswer2(sum2);
    }

    public int SolvePart1(string[] puzzle)
    {
        //extract nubers by regex
        var match = Regex.Matches(puzzle[0], @"(\d+)");
        (int x, int y) A = (int.Parse(match[0].Value), int.Parse(match[1].Value));

        match = Regex.Matches(puzzle[1], @"(\d+)");
        (int x, int y) B = (int.Parse(match[0].Value), int.Parse(match[1].Value));
        match = Regex.Matches(puzzle[2], @"(\d+)");
        (int x, int y) Prize = (int.Parse(match[0].Value), int.Parse(match[1].Value));

        var aPressed = 0;
        var bPressed = 0;

        var costs = new List<int>();
        do
        {
            bPressed = 0;
            do
            {
                if ((A.x * aPressed) + (B.x * bPressed) == Prize.x &&
                    (A.y * aPressed) + (B.y * bPressed) == Prize.y
                )
                {
                    costs.Add((aPressed * 3) + bPressed);
                }
                bPressed++;
            } while (bPressed < 100);
            aPressed++;
        } while (aPressed < 100);

        return costs.Any() ? costs.Min() : 0;
    }

    public decimal SolvePart2(string[] puzzle)
    {
        //extract nubers by regex
        var match = Regex.Matches(puzzle[0], @"(\d+)");
        (double x, double y) A = (double.Parse(match[0].Value), double.Parse(match[1].Value));
        match = Regex.Matches(puzzle[1], @"(\d+)");
        (double x, double y) B = (double.Parse(match[0].Value), double.Parse(match[1].Value));
        match = Regex.Matches(puzzle[2], @"(\d+)");
        (double x, double y) Prize = (double.Parse(match[0].Value) + 10000000000000, double.Parse(match[1].Value) + 10000000000000);

        double min = 0;
        var max = new List<double>() { Prize.x, Prize.y }.Max();

        var minTan = Math.Atan2(Prize.x, Prize.y);
        var maxTan = Math.Atan2(A.x * max, A.y * max);

        var bTan = Math.Atan2(B.x, B.y);

        double presses = 0;

        while (min != max)
        {
            //half point
            var half = Math.Floor((max - min) / 2) + min;

            if ((Prize.x - (A.x * half)) <= 0 || (Prize.y - (A.y * half)) <= 0)
            {
                max = half;
                continue;
            }

            var tan = Math.Atan2(Prize.x - (A.x * half), Prize.y - ( A.y * half));

            if (tan == bTan)
            {
                min = max;
                presses = half;
                break;
            }

            if ((minTan > bTan && tan < bTan) || minTan < bTan && tan > bTan)
            {
                max = half;
            }
            else
            {
                min = half;
            }

            if (max - min <= 1)
                break;
        }


        var x = A.x * presses;
        var y = A.y * presses;

        var acost = presses * 3;

        if ((Prize.x - x) % B.x == 0 && (Prize.y - y) % B.y == 0)
        {
            logger.OnNext($"Presses: {acost} {((Prize.x - x) / B.x)}");
            return (decimal)(acost + ((Prize.x - x) / B.x));
        }

        logger.OnNext($"Presses: not found");
        return 0;
    }
}
