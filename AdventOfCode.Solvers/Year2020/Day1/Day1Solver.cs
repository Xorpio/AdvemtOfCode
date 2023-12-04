namespace AdventOfCode.Solvers.Year2020.Day1;

public class Day1Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        var numbers = puzzle.Select(int.Parse).ToList();

        foreach (var num1 in numbers)
        {
            foreach (var num2 in numbers)
            {
                if (num1 == num2 || num1 + num2 > 2020) continue;

                if (num1 + num2 == 2020)
                {
                    GiveAnswer1((num1 * num2).ToString());
                }

                foreach (var num3 in numbers)
                {
                    if (num1 + num2 + num3 == 2020)
                    {
                        GiveAnswer2((num1 * num2 * num3).ToString());
                    }
                }
            }
        }
    }
}
