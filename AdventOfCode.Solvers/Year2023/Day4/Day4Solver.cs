
using System.Globalization;

namespace AdventOfCode.Solvers.Year2023.Day4;

public class Day4Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        decimal answer = 0;

        foreach(var line in puzzle)
        {
            var nums = line.Split(':').Last();
            var winningNumbers = nums.Split('|').First().Split(' ')
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Select(int.Parse).Order().ToList();
            var numbersYouHave = nums.Split('|').Last().Split(' ')
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Select(int.Parse).Order().ToList();

            var matches = winningNumbers.Intersect(numbersYouHave).ToList().Count();

            if(matches > 0)
            {
                answer += (decimal)Math.Pow(2, matches - 1);
            }
        }

        GiveAnswer1(answer.ToString());
    }
}
