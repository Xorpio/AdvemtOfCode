namespace AdventOfCode.Solvers.Year2023.Day5;

public class Day5Solver : BaseSolver
{
    string[] _puzzle = Array.Empty<string>();
    public override void Solve(string[] puzzle)
    {
        _puzzle = puzzle;
        var ranges = new List<(decimal start, decimal count)>();

        var seeds = puzzle[0].Split(" ").Skip(1).Select(decimal.Parse).ToList();

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

    private decimal FindLowestInRange(List<(decimal start, decimal count)> ranges)
    {
        var newRanges = new List<(decimal start, decimal count)>();
        //loop over puzzle
        foreach (var line in _puzzle.Skip(2))
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                newRanges.AddRange(ranges);
                ranges = newRanges;
                newRanges = new();
                continue;
            }

            if (line.Contains("-"))
            {
                logger.OnNext(line);
                continue;
            }

            var starts = ranges.Select(r => r.start).ToArray();
            var parts = line.Split(' ').Select(decimal.Parse).ToList();
            foreach (var start in starts)
            {
                //get start from ranges and remove it
                var range = ranges.First(r => r.start == start);
                ranges.Remove(range);

                var (newRange, oldRanges) = AdjustRanges(parts, range);

                if (newRange != null)
                {
                    newRanges.Add(newRange.Value);
                }

                if (oldRanges != null)
                {
                    ranges.AddRange(oldRanges);
                }
            }
        }

        newRanges.AddRange(ranges);

        return newRanges.Select(r => r.start).Min();
    }

    public ((decimal start, decimal count)? newRange, List<(decimal start, decimal count)>? oldRanges) AdjustRanges(List<decimal> parts, (decimal start, decimal count) range)
    {
        logger.OnNext($"Adjusting {range.start} {range.count} with {parts[0]} {parts[1]} {parts[2]}");
        var rangeStart = range.start;
        var rangeEnd = range.start + range.count - 1;
        var adjustStart = parts[1];
        var adjustEnd = parts[1] + parts[2] - 1;
        var adjust = parts[0] - parts[1];

        if (adjustEnd < rangeStart || adjustStart > rangeEnd)
        {
            return (null, new List<(decimal start, decimal count)>() { range });
        }

        if (adjustStart <= rangeStart && adjustEnd >= rangeEnd)
        {
            return ((rangeStart + adjust, range.count), null);
        }

        if (adjustStart > rangeStart && adjustEnd < rangeEnd)
        {
            return ((adjustStart + adjust, parts[2]),
                new List<(decimal start, decimal count)>()
                {
                    (rangeStart, adjustStart - rangeStart),
                    (adjustEnd + 1, rangeEnd - adjustEnd)
                });
        }

        if (adjustStart <= rangeStart && adjustEnd < rangeEnd)
        {
            var overlap = adjustEnd - rangeStart + 1;
            return ((rangeStart + adjust, overlap),
                new List<(decimal start, decimal count)>()
                {
                    (rangeStart + overlap, range.count - overlap)
                });
        }

        if (adjustStart > rangeStart && adjustEnd >= rangeEnd)
        {
            var overlap = rangeEnd - adjustStart + 1;
            return ((adjustStart + adjust, overlap),
                new List<(decimal start, decimal count)>()
                {
                    (rangeStart, range.count - overlap)
                });
        }

        throw new Exception("Should not be here");
    }
}
