using System.Linq.Expressions;

namespace AdventOfCode.Solvers.Year2024.Day7;

public class Day7Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        decimal total = 0;
        decimal total2 = 0;

        foreach (var line in puzzle)
        {
            var parts = line.Split(": ");
            var inputs = parts[1].Split(" ").Select(x => decimal.Parse(x)).ToArray();
            var expected = decimal.Parse(parts[0]);

            var isValid = recCalc(expected, inputs, []);
            if (isValid)
            {
                total += expected;
            }

            isValid = recCalc(expected, inputs, [], true);
            if (isValid)
            {
                total2 += expected;
            }
        }

        GiveAnswer1(total);
        GiveAnswer2(total2);
    }

    private bool recCalc(decimal expected, decimal[] inputs, Opp[] opps, bool isPart2 = false)
    {
        //validate
        if ((opps.Length + 1) == inputs.Length)
        {
            var total = inputs[0];

            var c = 1;

            foreach (var o in opps)
            {
                total = o switch
                {
                    Opp.mult => total * inputs[c],
                    Opp.comb => decimal.Parse($"{total}{inputs[c]}"),
                    _ => total + inputs[c],
                };

                c++;

                if (total > expected)
                    return false;
            }

            return total == expected;
        }

        if (recCalc(expected, inputs, opps.Append(Opp.mult).ToArray(), isPart2))
        {
            return true;
        }

        if (recCalc(expected, inputs, opps.Append(Opp.plus).ToArray(), isPart2))
        {
            return true;
        }

        if (isPart2)
        {
            if (recCalc(expected, inputs, opps.Append(Opp.comb).ToArray(), isPart2))
            {
                return true;
            }
        }

        return false;
    }
}

enum Opp
{
    plus,
    mult,
    comb
}
