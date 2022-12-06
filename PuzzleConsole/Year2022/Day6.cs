namespace PuzzleConsole.Year2022.Day6;

public class Day6 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
        var line = puzzle[0];
        for (var index = 3; index < line.Length; index++)
        {
            var arr = new char[]
            {
                line[index],
                line[index - 1],
                line[index - 2],
                line[index - 3],
            };
            if (arr.Distinct().Count() > 3)
            {
                return new[] { (index + 1).ToString() };
            }
        }

        throw new Exception("No answer found");
    }
}