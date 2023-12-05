using System.Globalization;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solvers.Year2023.Day5;

public class Day5Solver : BaseSolver
{
    private Dictionary<(string source, string dest), Dictionary<(decimal start, decimal range), decimal>> _map = new();
    public override void Solve(string[] puzzle)
    {
        string destination = "";
        string source = "";
        string[] parts = puzzle[0].Split(' ');
        foreach(var line in puzzle.Skip(2))
        {
            if (destination == "")
            {
                parts = line.Split(' ');
                parts = parts[0].Split('-');
                source = parts[0];
                destination = parts[2];
                _map.Add((source, destination), new ());
                continue;
            }

            if (line == "")
            {
                destination = "";
                continue;
            }

            var intParts = line.Split(' ').Select(decimal.Parse).ToArray();
            var diff = intParts[0] - intParts[1];
            _map[(source, destination)].Add((intParts[1], intParts[2]), diff);
            logger.OnNext($"Added {source} {destination} from: {intParts[1]} range: {intParts[2]} diff: {diff}");
        }

        var seeds = puzzle[0].Split(' ').Skip(1).Select(decimal.Parse).ToArray();

        var lowestLocation = decimal.MaxValue;

        foreach(var seed in seeds)
        {
            string currentLocation = "seed";
            decimal currentValue = seed;

            currentValue = findLowestLocation(currentValue, currentLocation);

            if (lowestLocation > currentValue)
            {
                lowestLocation = currentValue;
            }
        }
        
        GiveAnswer1($"{lowestLocation}");	

        lowestLocation = decimal.MaxValue;
        for(int i = 0; i < seeds.Length; i = i + 2)
        {
            for(decimal seed = seeds[i]; seed < seeds[i] + seeds[i + 1]; seed++)
            {
                logger.OnNext($"range {i} of {seeds.Length}. Seed {seed} of {seeds[i] + seeds[i + 1]}");    
                string currentLocation = "seed";
                decimal currentValue = seed;

                currentValue = findLowestLocation(currentValue, currentLocation);

                if (lowestLocation > currentValue)
                {
                    lowestLocation = currentValue;
                }
            }
        }
        GiveAnswer2($"{lowestLocation}");
    }

    public decimal findLowestLocation(decimal currentValue, string currentLocation)
    {
        do
        {
            var nextLocation = _map.First(x => x.Key.source == currentLocation).Value;
            var found = false;
            foreach(var next in nextLocation)
            {
                if (currentValue >= next.Key.start && currentValue < next.Key.start + next.Key.range)
                {
                    currentValue = currentValue + next.Value;
                    found = true;
                    break;
                }
            }
            if (!found)
            {
            }

            currentLocation = _map.First(x => x.Key.source == currentLocation).Key.dest;
        } while (currentLocation != "location");

        return currentValue;
    }
}
