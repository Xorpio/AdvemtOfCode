using System.Reactive.Linq;

namespace PuzzleConsole.Year2021;

public class Day8 : ISolver
{
    public string[] Solve(string[] puzzleInput)
    {
        var puzzle = puzzleInput.Select(l => l.Split(" | "))
            .Select(ls => new { Segments = ls[0], Numbers = ls[1].Split(' ') })
            .ToList();

        var lengts = puzzle.SelectMany(p => p.Numbers)
            .Select(s => s.Length);

        var answer = lengts.Where(l => l == 2 || l == 3 || l == 4 || l == 7).Count();

        var answer2 = puzzleInput.Select(l => SolveLine(l)).Sum();

        return new string[] { answer.ToString(), answer2.ToString() };
    }

    public int SolveLine (string input)
    {
        var linesplit = input.Split(" | ");
        var segments = linesplit[0].Split(' ').Select(s => string.Concat(s.OrderBy(c => c)));
        var numbers = linesplit[1].Split(' ').Select(s => string.Concat(s.OrderBy(c => c)));

        var digits = new Dictionary<string, string>();

        digits.Add(segments.Where(s => s.Length == 2).First(), "1");
        digits.Add(segments.Where(s => s.Length == 3).First(), "7");
        digits.Add(segments.Where(s => s.Length == 4).First(), "4");
        digits.Add(segments.Where(s => s.Length == 7).First(), "8");

        var fives = segments.Where(s => s.Length == 5).ToList();
        var sixes = segments.Where(s => s.Length == 6).ToList();

        var chars1 = digits.Where(kv => kv.Value == "1").First().Key.ToArray();
        var three = fives.Where(f => chars1.All(c => f.Contains(c))).First();
        digits.Add(three, "3");
        fives.Remove(three);

        var chars3 = three.ToArray();
        var nine = sixes.Where(f => chars3.All(c => f.Contains(c))).First();
        digits.Add(nine, "9");
        sixes.Remove(nine);

        var chars8 = digits.Where(kv => kv.Value == "8").First().Key.ToList();
        var chars9 = nine.ToList();
        chars9.ForEach(c => chars8.Remove(c));
        var leftBottom = chars8.First();

        var five = fives.Where(f => !f.Contains(leftBottom)).First();
        var two = fives.Where(f => f.Contains(leftBottom)).First();
        digits.Add(five, "5");
        digits.Add(two, "2");

        var zero = sixes.Where(f => chars1.All(c => f.Contains(c))).First();
        var six = sixes.Where(f => f != zero).First();

        digits.Add(six, "6");
        digits.Add(zero, "0");

        var solution = "";

        foreach (var num in numbers)
        {
            solution += digits[num];
        }

        return int.Parse(solution);
    }
}
