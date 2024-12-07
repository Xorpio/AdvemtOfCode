namespace AdventOfCode.Solvers.Year2024.Day7;

public class Day7Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        decimal total = 0;

        foreach(var line in puzzle)
        {
            var parts = line.Split(": ");
            var inputs = parts[1].Split(" ").Select(x => decimal.Parse(x)).ToArray();
            var expected = decimal.Parse(parts[0]);

            var isValid = recCalc(expected, inputs, []);
            if (isValid)
            {
                total += expected;
            }
        }

        GiveAnswer1(total);
        GiveAnswer2("");
    }

    private bool recCalc(decimal expected, decimal[] inputs, Opp[] opps)
    {
        //validate
        if ((opps.Length + 1) == inputs.Length)
        {
            var total = inputs[0];

            var c = 1;

            foreach(var o in opps)
            {
                total = o switch {
                    Opp.mult => total * inputs[c],
                    _ => total + inputs[c],
                };

                c++;

                if (total > expected)
                    return false;
            }

            return total == expected;
        }

        if (recCalc(expected, inputs, opps.Append(Opp.mult).ToArray()))
        {
            return true;
        }

        if (recCalc(expected, inputs, opps.Append(Opp.plus).ToArray()))
        {
            return true;
        }

        return false;
    }
}

enum Opp
{
    plus,
    mult
}
