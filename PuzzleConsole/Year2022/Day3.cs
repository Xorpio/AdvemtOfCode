namespace PuzzleConsole.Year2022.Day3;

public class Day3 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
        var score = 0;
        var score2 = 0;

        for (var index = 0; index < puzzle.Length; index++)
        {
            var line = puzzle[index];
            int half = (line.Length / 2) is int ? (int)(line.Length / 2) : -1;

            (var left, var right) = (line[0..half], line[half..]);

            score += GetScore(left, right);

            if ((index + 1) % 3 == 0)
            {
                char p = line.First(c => puzzle[index - 1].Contains(c) && puzzle[index - 2].Contains(c));
                score2 += ((int)p > 96)
                    ? (int)p - 96
                : (int)p - 38;
            }
        }

        return new[] { score.ToString(), score2.ToString() };
    }

    private int GetScore(string left, string right)
    {
        char p = left.First(c => right.Contains(c));

        return ((int)p > 96)
            ? (int)p - 96
        : (int)p - 38;
    }
}