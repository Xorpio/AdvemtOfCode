namespace PuzzleConsole.Year2022.Day7;

public class Day7 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
        puzzle = puzzle.Select(p => p.Trim()).ToArray();

        var dir = ToDir(puzzle);

        foreach (var p1 in dir)
        {
            foreach (var p2 in dir
                         .Where(p2 => p1.Key != p2.Key)
                         .Where(p2 => p2.Key.StartsWith(p1.Key)))
            {
                dir[p1.Key] += p2.Value;

            }
        }

        var answer = dir.Where(d => d.Value <= 100_000)
              .Select(d => d.Value)
              .Sum();

        //part 2
        var totalSpace = 70_000_000;
        var neededSpace = 30_000_000;

        string root = Path.GetFullPath("/");

        var freeSpace = (totalSpace - dir[root]);
        var toDelete = neededSpace - freeSpace;

        var answer2 = dir
            .Where(d => d.Value > toDelete)
            .OrderBy(d => d.Value)
            .First().Value;


        return new [] { answer.ToString(), answer2.ToString() };
    }

    public Dictionary<string, decimal> ToDir(string[] puzzle)
    {
        var path = Path.GetFullPath("/");

        var directories = new Dictionary<string, decimal>();

        var listing = false;
        decimal totalSize = 0;
        foreach (var line in puzzle)
        {
            switch ((listing, line.Split(' ')))
            {
                case (_, ["$", "cd", string location]):
                    if (listing)
                    {
                        directories.TryAdd(path, totalSize);
                        totalSize = 0;
                        listing = false;
                    }

                    path = Path.GetFullPath(Path.Combine(path, location));
                    break;

                case (_, ["$", "ls"]):
                    listing = true;
                    break;

                case (true, ["dir", _]):
                    continue;
                    break;

                case (true, [var size, var name]):
                    totalSize += decimal.Parse(size);
                    break;

                default:
                    throw new Exception($"{line} not supported");
            }
        }

        if (listing)
        {
            directories.TryAdd(path, totalSize);
            totalSize = 0;
            listing = false;
        }

        return directories;
    }
}