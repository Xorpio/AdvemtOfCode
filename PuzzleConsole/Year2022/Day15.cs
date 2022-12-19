using System.Diagnostics.CodeAnalysis;

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

        foreach (var bs in beaconsSensors)
        {
            (var sensor, var beacon) = bs;
            var distance = sensor + beacon;

            //part 2
            if (
                beaconsSensors.Any(_bs => _bs != bs && _bs.Sensor.X > sensor.X)
                && beaconsSensors.Any(_bs => _bs != bs && _bs.Sensor.X < sensor.X)
                && beaconsSensors.Any(_bs => _bs != bs && _bs.Sensor.Y > sensor.Y)
                && beaconsSensors.Any(_bs => _bs != bs && _bs.Sensor.Y < sensor.Y)
            )
            {

                var fromY = sensor.Y - (distance + 1);
                if (fromY < 0)
                {
                    fromY = 0;
                }
                var toY = sensor.Y + (distance + 1);
                if (toY > checkLine * 2)
                {
                    toY = checkLine * 2;
                }

                for (int y = fromY; y < toY; y++)
                {
                    var s = sensor with { Y = y };
                    var lr = Math.Abs(((sensor + s) - 1) - distance);
                    var l = s with { X = s.X - lr };
                    var r = s with { X = s.X + lr };

                    var answers = beaconsSensors
                        .Where(_bs => _bs != bs)
                        .Select(_bs => (_bs.Sensor, _bs.Sensor + _bs.Beacon))
                        .Select(sd => (sd.Sensor, sd.Sensor + l > sd.Item2));

                    if (l.X > 0 && l.X < (checkLine * 2) &&
                        beaconsSensors
                            .Where(_bs => _bs != bs)
                            .Select(_bs => (_bs.Sensor, _bs.Sensor + _bs.Beacon))
                            .All<(Coord Sensor, int Distance)>(sd => sd.Sensor + l > sd.Distance)
                       )
                    {
                        double dx = l.X;
                        double dy = l.Y;
                        double mul = 4_000_000;
                        double ans = (dx * mul) + dy;

                        return new[] { positions.Count().ToString(), ans.ToString() };
                    }
                    if (r.X > 0 && r.X < (checkLine * 2) &&
                        beaconsSensors
                            .Where(_bs => _bs != bs)
                            .Select(_bs => (_bs.Sensor, _bs.Sensor + _bs.Beacon))
                            .All<(Coord Sensor, int Distance)>(sd => sd.Sensor + r > sd.Distance)
                       ) {
                        double dx = r.X;
                        double dy = r.Y;
                        double mul = 4_000_000;
                        double ans = (dx * mul) + dy;

                        return new[] { positions.Count().ToString(), ans.ToString() };
                    }

                    //     .
                    //
                    //     if (beaconsSensors.
                    //         .Where(_bs => _bs != bs)
                    //        )
                    //     {
                    //         return new[] { positions.Count().ToString(), "found" };
                    //     }
                }
            }
            else
            {
                Console.WriteLine($"Rand: {sensor}");
            }
        }

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