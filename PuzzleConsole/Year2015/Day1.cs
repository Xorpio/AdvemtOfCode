namespace PuzzleConsole.Year2015.Day1;

public class Day1 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
        var answer1 = 0;
        var answer2 = 0;
        for(int i = 0; i < puzzle[0].Length; i++)
        {
            answer1 += puzzle[0][i] == '(' ? 1 : -1;

            if (answer2 == 0 && answer1 < 0)
                answer2 = i + 1;
        }

        return new string[] { answer1.ToString(), answer2.ToString() };
    }
}
