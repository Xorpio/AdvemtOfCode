namespace PuzzleConsole.Year2022.Day18;

public class Day18 : ISolver
{
    private HashSet<Cube> _droplet;
    private HashSet<Cube> _ocean;
    public string[] Solve(string[] puzzle)
    {
        _droplet = new HashSet<Cube>();
        foreach (var line in puzzle)
        {
            var xyz = line.Split(',');
            _droplet.Add(new Cube(
                int.Parse(xyz[0]),
                int.Parse(xyz[1]),
                int.Parse(xyz[2])
            ));
        }

        var water = _droplet.OrderBy(c => c.X).First();
        var waterUnChecked = new Queue<Cube>();
        _ocean = new HashSet<Cube>();
        var waterChecked = new HashSet<Cube>();
        waterUnChecked.Enqueue(water);

        Console.WriteLine("Starting water calculation");

        var count = 0;

        while (waterUnChecked.Any())
        {
            water = waterUnChecked.Dequeue();
            waterChecked.Add(water);

            if (!_droplet.Contains(water))
            {
                _ocean.Add(water);
            }

            var newWater = new Cube[]
            {
                water with { Y = water.Y - 1 },
                water with { Y = water.Y + 1 },
                water with { X = water.X - 1 },
                water with { X = water.X + 1 },
                water with { Z = water.Z - 1 },
                water with { Z = water.Z + 1 },
            };

            foreach (var nw in newWater)
            {
                if (!waterUnChecked.Contains(nw) && !waterChecked.Contains(nw) && !_droplet.Contains(nw) && isNearDroplet(nw))
                {
                    waterUnChecked.Enqueue(nw);
                }
            }

            if (count % 1000 == 0)
            {
                Console.WriteLine($"ocean {count} of {waterUnChecked.Count}");
            }
            count++;
        }

        Console.WriteLine("Starting drop calculation");
        count = 0;

        var sides = 0;
        var sidesOcean = 0;
        foreach (var cube in _droplet)
        {
            sides += getSides(cube);
            sidesOcean += getSidesOcean(cube);

            if (count % 1000 == 0)
            {
                Console.WriteLine($"droplet {count} of {_droplet.Count}");
            }

            count++;
        }

        return new string[]{ sides.ToString(), sidesOcean.ToString() };
    }

    private int getSidesOcean(Cube cube)
    {
        var f = _ocean.Contains(cube with { Y = cube.Y - 1 })? 1 : 0;
        var b = _ocean.Contains(cube with { Y = cube.Y + 1 })? 1 : 0;

        var r = _ocean.Contains(cube with { X = cube.X - 1 })? 1 : 0;
        var l = _ocean.Contains(cube with { X = cube.X + 1 })? 1 : 0;

        var u = _ocean.Contains(cube with { Z = cube.Z - 1 })? 1 : 0;
        var d = _ocean.Contains(cube with { Z = cube.Z + 1 })? 1 : 0;

        return f + b + r + l + u + d;
    }

    private bool isNearDroplet(Cube water)
    {
        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                for (int z = -1; z < 2; z++)
                {
                    if (x == 0 && y == 0 && z == 0)
                        continue;

                    if (_droplet.Contains(water with
                        {
                            X = water.X + x,
                            Y = water.Y + y,
                            Z = water.Z + z
                        }))
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    private int getSides(Cube cube)
    {
        var f = _droplet.Contains(cube with { Y = cube.Y - 1 }) ? 0 : 1;
        var b = _droplet.Contains(cube with { Y = cube.Y + 1 }) ? 0 : 1;

        var r = _droplet.Contains(cube with { X = cube.X - 1 }) ? 0 : 1;
        var l = _droplet.Contains(cube with { X = cube.X + 1 }) ? 0 : 1;

        var u = _droplet.Contains(cube with { Z = cube.Z - 1 }) ? 0 : 1;
        var d = _droplet.Contains(cube with { Z = cube.Z + 1 }) ? 0 : 1;

        return f + b + r + l + u + d;
    }
}

public record Cube(int X, int Y, int Z);