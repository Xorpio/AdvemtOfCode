namespace PuzzleConsole.Year2022.Day1;

public class Day1 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
        var elfs = new List<int>();

        var count = 0;
        foreach (var line in puzzle)
        {
            if (int.TryParse(line, out var cal))
            {
                count += cal;
            }
            else
            {
                elfs.Add(count);
                count = 0;
            }
        }
        elfs.Add(count);

        var answer = elfs.Max();

        var answer2 = elfs.OrderByDescending(i => i)
            .Take(3)
            .Sum();

        return new string[] { answer.ToString(), answer2.ToString() };
    }
}