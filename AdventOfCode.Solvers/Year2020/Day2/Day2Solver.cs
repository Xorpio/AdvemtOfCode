namespace AdventOfCode.Solvers.Year2020.Day2;

public class Day2Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        var answer1 = 0;
        var answer2 = 0;

        foreach (var line in puzzle)
        {
            var parts = line.Split(':');
            var password = parts[1].Trim().ToArray();

            parts = parts[0].Split(' ');
            var nums = parts[0].Split('-')
                .Select(int.Parse).ToArray();

            var letter = parts[1].Trim().First();

            var occurences = password.Where(c => c == letter).Count();
            if(occurences >= nums[0] && occurences <= nums[1])
            {
                answer1++;
            }

            if(password[nums[0] - 1] == letter ^ password[nums[1] - 1] == letter)
            {
                answer2++;
            }
        }

        GiveAnswer1($"{answer1}");
        GiveAnswer2($"{answer2}");
    }
}
