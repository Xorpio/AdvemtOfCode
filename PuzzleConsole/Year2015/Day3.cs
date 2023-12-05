namespace PuzzleConsole.Year2015.Day3;

public class Day3 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
        var location = new Location(0, 0);
        var locationSanta = new Location(0, 0);
        var locationRobot = new Location(0, 0);

        var locations = new List<Location>();
        var locations2 = new List<Location>();

        locations.Add(location);
        locations2.Add(location);

        for (var index = 0; index < puzzle[0].Length; index++)
        {
            var c = puzzle[0][index];
            location = ChangeLocation(c, location);

            if (index % 2 == 0)
            {
                locationSanta = ChangeLocation(c, locationSanta);
                if (!locations2.Contains(locationSanta))
                {
                    locations2.Add(locationSanta);
                }
            }
            else
            {
                locationRobot = ChangeLocation(c, locationRobot);
                if (!locations2.Contains(locationRobot))
                {
                    locations2.Add(locationRobot);
                }
            }

            if (!locations.Contains(location))
            {
                locations.Add(location);
            }
        }

        return new[]
        {
            $"{locations.Count()}",
            $"{locations2.Count()}"
        };
    }

    private static Location ChangeLocation(char c, Location location)
    {
        return c switch
        {
            '>' => location with { X = location.X + 1 },
            '<' => location with { X = location.X - 1 },
            '^' => location with { Y = location.Y + 1 },
            'v' => location with { Y = location.Y - 1 },
            _ => throw new NotSupportedException()
        };
    }
}

public record Location(int X, int Y);