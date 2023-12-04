
using System.Globalization;

namespace AdventOfCode.Solvers.Year2023.Day4;

public class Day4Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        decimal answer1 = 0;

        var cards = Enumerable.Repeat(1, puzzle.Length).ToList();
        var position = 0;

        foreach(var line in puzzle)
        {
            var cardrun = 1;
            bool first = true;
            do
            {
                var nums = line.Split(':').Last();
                var winningNumbers = nums.Split('|').First().Split(' ')
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(int.Parse).Order().ToList();
                var numbersYouHave = nums.Split('|').Last().Split(' ')
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(int.Parse).Order().ToList();

                var matches = winningNumbers.Intersect(numbersYouHave).ToList().Count();

                for(int i = 1; i <= matches; i++)
                {
                    if (position + i < cards.Count)
                    {
                        cards[position + i]++;
                    }
                }

                if(matches > 0 && first)
                {
                    answer1 += (decimal)Math.Pow(2, matches - 1);
                }

                first = false;
                cardrun++;
            } while(cards[position] >= cardrun);
            position++;
        }

        GiveAnswer1(answer1.ToString());
        GiveAnswer2(cards.Sum().ToString());
    }
}
