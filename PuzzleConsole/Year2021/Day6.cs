using System.Reactive.Linq;

namespace PuzzleConsole.Year2021;

public class Day6 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
        var fishList = puzzle[0].Split(',')
            .Select(s => int.Parse(s))
            .ToList();

        var fishes = new Dictionary<int, decimal>();
        for (int i = 0; i < 9; i++)
        {
            fishes.Add(i, 0);
        }

        foreach (var sourceFish in fishList)
        {
            fishes[sourceFish]++;
        }

        decimal total18 = 0;
        decimal total80 = 0;
        decimal total256 = 0;
        for (int i = 0; i < 256; i++)
        {
            var newFishes = new Dictionary<int, decimal>();
            for (int c = 0; c < 9; c++)
            {
                newFishes.Add(c, 0);
            }

            foreach (var fish in fishes)
            {
                if (fish.Key == 0)
                {
                    newFishes[6] += fish.Value;
                    newFishes[8] += fish.Value;
                }
                else
                {
                    newFishes[fish.Key -1] += fish.Value;
                }
            }

            fishes = newFishes;
            if (i == 17)
            { 
                foreach (var fish in fishes)
                {
                    total18 += fish.Value;
                }
            }
            if (i == 79)
            { 
                foreach (var fish in fishes)
                {
                    total80 += fish.Value;
                }
            }
        }

        foreach (var fish in fishes)
        {
            total256 += fish.Value;
        }


        return new string[] {
            $"Day 18: {total18}",
            $"Day 80: {total80}",
            $"Day 256: {total256}"
        };
    }
}
