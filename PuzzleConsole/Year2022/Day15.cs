namespace PuzzleConsole.Year2022.Day15;

public class Day15 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
        var beaconsSensors = new List<(Coord Sensor, Coord Beacon)>();

        foreach (var line in puzzle)
        {
            var words = line.Split(',');
            var sensorX = words[0].Split('=')[1];
            var sensorY = words[1].Split(':')[0].Split('=')[1];

            var beaconX = words[1].Split(':')[1].Split('=')[1];
            var beaconY = words[2].Split('=')[1];

            beaconsSensors.Add(new (
                new Coord(int.Parse(sensorX), int.Parse(sensorY)),
                new Coord(int.Parse(beaconX), int.Parse(beaconY))
            ));
        }

        var checkLine = beaconsSensors.Select(bs => bs.Sensor.Y)
            .Max() > 1000
            ? 2_000_000
            : 10;

        var positions = new HashSet<Coord>();

        foreach (var bs in beaconsSensors)
        {
            (var sensor, var beacon) = bs;
            var distance = sensor + beacon;

            var test = sensor with { Y = checkLine };

            var left = test with { };
            var right = test with { };

            while (((left + sensor) <= distance) && ((right + sensor) <= distance))
            {
                if ((left + sensor) <= distance)
                {
                    positions.Add(left);
                    left = left with { X = left.X - 1 };
                }
                if ((right + sensor) <= distance)
                {
                    positions.Add(right);
                    right = right with { X = right.X + 1 };
                }
            }
        }

        var others= beaconsSensors.Select(bs => bs.Beacon);
        positions.ExceptWith(others);

        return new[] { positions.Count().ToString() };
    }
}
public record Coord (int X, int Y)
{
    public int Y { get; init; } = Y;

    public static int operator +(Coord a, Coord b)
    {
        return Math.Abs((a.X - b.X)) +
               Math.Abs((a.Y - b.Y));
    }
}