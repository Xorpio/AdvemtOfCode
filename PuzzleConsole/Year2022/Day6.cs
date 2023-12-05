namespace PuzzleConsole.Year2022.Day6;

public class Day6 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
        var answer1 = 0;
        var answer2 = 0;
        var line = puzzle[0];
        for (var index = 3; index < line.Length; index++)
        {
            var arr = line.Skip(index - 3)
                .Take(4);
            if (arr.Distinct().Count() > 3)
            {
                answer1 = (index + 1);
                break;
            }
        }

        for (var index = 13; index < line.Length; index++)
        {
            var arr = line.Skip(index - 13)
                .Take(14);
            if (arr.Distinct().Count() > 13)
            {
                answer2 = (index + 1);
                break;
            }
        }

        return new[] { (answer1).ToString(), answer2.ToString() };
    }
}