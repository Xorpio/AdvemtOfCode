using System.Security;

namespace AdventOfCode.Solvers.Year2024.Day5;

public class Day5Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        var lookup = new Dictionary<string, IList<string>>();

        var answer1 = 0;

        bool parsing = true;
        foreach(var line in puzzle)
        {
            if (string.IsNullOrEmpty(line))
            {
                parsing = false;
                continue;
            }

            if (parsing)
            {
                var parts = line.Split('|');
                if (!lookup.ContainsKey(parts[0]))
                {
                    lookup.Add(parts[0], []);
                }

                lookup[parts[0]].Add(parts[1]);
            }
            else
            {
                var pages = line.Split(',');
                if (validateLine(pages, lookup))
                {
                    answer1 += int.Parse(pages[(int)decimal.Floor(pages.Length / 2)]);
                }
            }
        }

        GiveAnswer1(answer1);
        GiveAnswer2("");
    }

    public bool validateLine(string[] lineParts, Dictionary<string, IList<string>> lookup)
    {
        var hist = new HashSet<string>();
        foreach(var part in lineParts)
        {
            if (lookup.ContainsKey(part) && lookup[part].Any(hist.Contains))
            {
                return false;
            }
            hist.Add(part);
        }
        return true;
    }
}
