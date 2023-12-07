namespace AdventOfCode.Solvers.Year2023.Day6;

public class Day6Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        var timeAllowed = puzzle[0].Split(" ").Where(s => !string.IsNullOrWhiteSpace(s)).Skip(1).Select(int.Parse).ToArray();
        var distances = puzzle[1].Split(" ").Where(s => !string.IsNullOrWhiteSpace(s)).Skip(1).Select(int.Parse).ToArray();

        var inputs = timeAllowed.Zip(distances).Select(t => new { time = t.First, distance = t.Second }).ToArray();

        var answer1 = 1;

        string input2Time = "";
        string distance2Time = "";
        decimal wins = 0;

        foreach(var input in inputs)
        {
            input2Time += input.time.ToString();
            distance2Time += input.distance.ToString();
            wins = 0;
            for(int i = 1; i < input.time; i++)
            {
                var distance = i * (input.time - i);
                if(distance > input.distance)
                {
                    wins++;
                }
            }

            answer1 *= (int)wins;
        }

        var distance2 = decimal.Parse(distance2Time);
        var time2 = decimal.Parse(input2Time);
        logger.OnNext($"Distance: {distance2}");
        logger.OnNext($"Time: {time2}");
        var winning = false;
        wins = 0;
        for(decimal i = 1; i < time2; i++)
        {
            var distance = i * (time2 - i);
            if(distance > distance2)
            {
                if(wins == 0)
                {
                    logger.OnNext($"Winning at {i} with {distance}");
                    winning = true;
                }
                wins++;
            }
            else if(winning)
            {
                logger.OnNext($"Losing at {i} with {distance}");
                winning = false;
            }
        }

        
        GiveAnswer1(answer1);
        GiveAnswer2(wins);
    }
}
