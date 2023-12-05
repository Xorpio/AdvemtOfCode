namespace PuzzleConsole.Year2021.Day7;

public class Day7 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
        var fishList = puzzle[0].Split(',')
            .Select(s => int.Parse(s))
            .ToList();

        var solutions = new List<int>();
        var solutions2 = new List<int>();
        for (int i = fishList.Min(); i <= fishList.Max(); i++)
        {
            var fuel = 0;
            var fuel2 = 0;
            foreach (var fish in fishList)
            {
                var f = Math.Abs(fish - i);
                fuel += f;
                fuel2 += Enumerable.Range(1, f).Sum();
            }
            solutions.Add(fuel);
            solutions2.Add(fuel2);
        }

        return new string[] { solutions.Min().ToString() ,
         solutions2.Min().ToString() };
    }
}
