using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

namespace AdventOfCode.Solvers.Year2023.Day5;

public class Day5Solver : BaseSolver
{
    string[] _puzzle = Array.Empty<string>();
    public override void Solve(string[] puzzle)
    {
        _puzzle = puzzle;
        var ranges = new List<(decimal start, decimal range)>();

        var seeds = puzzle[1].Split(" ").Select(decimal.Parse).ToList();

        //part 1 
        ranges = seeds.Select(s => (s, (decimal)1)).ToList();
        GiveAnswer1(FindLowestInRange(ranges).ToString());

        ranges = new();
        //part 2
        for(int i = 0; i < seeds.Count(); i += 2)
        {
            ranges.Add((seeds[i], seeds[i + 1]));
        }

        GiveAnswer2(FindLowestInRange(ranges).ToString());  
    }

    private decimal FindLowestInRange(List<(decimal start, decimal range)> ranges)
    {
        throw new NotImplementedException();
    }
}
