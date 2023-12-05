namespace PuzzleConsole.Year2022.Day2;

public class Day2 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
        var score = 0;
        var score2 = 0;

        foreach (var line in puzzle)
        {
            var input = GetInput(line);
            var rps = GetRockPaperSciscor(input);
            var outcome = RockPaperScicor(rps);

            var outcome2 = GetResult(input);
            score2 += outcome2;
            score += outcome;
        }

        return new[] { $"{score}", $"{score2}" };
    }

    private int GetResult((string A, string B) input) => (input.A, input.B) switch
        {
            //rock
            ("A", "Y") => (1 + 3),
            ("A", "X") => (3 + 0),
            ("A", "Z") => (2 + 6),
            //paper
            ("B", "Y") => (2 + 3),
            ("B", "X") => (1 + 0),
            ("B", "Z") => (3 + 6),
            //scissors
            ("C", "Y") => (3 + 3),
            ("C", "X") => (2 + 0),
            ("C", "Z") => (1 + 6),
            _ => throw new NotImplementedException()
        };

    private (string A, string B) GetInput(string line)
    {
        var split = line.Split(' ');

        return (split[0], split[1]);
    }

    private (RockPaperScissors A, RockPaperScissors B) GetRockPaperSciscor((String A, String B) input)
    {
        var a = input.A switch
        {
            "A" => RockPaperScissors.Rock,
            "B" => RockPaperScissors.Paper,
            _ => RockPaperScissors.Scissors,
        }
        ;
        var b = input.B switch
        {
            "Y" => RockPaperScissors.Paper,
            "X" => RockPaperScissors.Rock,
            _ => RockPaperScissors.Scissors,
        }
        ;

        return (a, b);
    }

    private int RockPaperScicor((RockPaperScissors A, RockPaperScissors B) getRockPaperSciscor) =>
        (getRockPaperSciscor.B, getRockPaperSciscor.A) switch
        {
            (RockPaperScissors.Rock, RockPaperScissors.Rock) => (1 + 3),
            (RockPaperScissors.Rock, RockPaperScissors.Paper) => (1 + 0),
            (RockPaperScissors.Rock, RockPaperScissors.Scissors) => (1 + 6),
            (RockPaperScissors.Paper, RockPaperScissors.Rock) => (2 + 6),
            (RockPaperScissors.Paper, RockPaperScissors.Paper) => (2 + 3),
            (RockPaperScissors.Paper, RockPaperScissors.Scissors) => (2 + 0),
            (RockPaperScissors.Scissors, RockPaperScissors.Rock) => (3 + 0),
            (RockPaperScissors.Scissors, RockPaperScissors.Paper) => (3 + 6),
            (RockPaperScissors.Scissors, RockPaperScissors.Scissors) => (3 + 3),
            _ => throw new NotImplementedException()
        };
}

public enum RockPaperScissors
{
    Rock,
    Paper,
    Scissors,
}