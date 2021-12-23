namespace PuzzleConsole.Year2021.Day19;

public class Day19 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
        var scanners = new List<Scanner>();
        Scanner scanner = null;
        foreach (var line in puzzle)
        {
            if (line[0..2] == "---")
            {
                scanner = new Scanner() { Name = line };
            }
            if (string.IsNullOrEmpty(line))
            {
                scanners.Add(scanner);
            }
            else
            {
                var coordinates = new Coordinates(line);
                scanner.Beacons.Add(coordinates);
            }
        }

        scanners.Add(scanner);

	    return puzzle;
    }
}

public class Scanner
{
    public string Name { get; set; }

    public List<Coordinates> Beacons { get; set; } = new List<Coordinates>();
}

public class Coordinates
{
    public Coordinates(string line)
    {
        var coords = line.Split(',');
        X = int.Parse(coords[0]);
        Y = int.Parse(coords[1]);
        Z = int.Parse(coords[1]);
    }

    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }
}
