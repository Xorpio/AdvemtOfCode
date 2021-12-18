using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace PuzzleConsole.Year2021.Day11;

public class Day11 : ISolver
{
    public int Steps { get; set; } = 100;
    public bool Part2 { get; set; } = true;
    public string[] Solve(string[] puzzle)
	{
        var octopuses = new List<Octopus>();

        var flashesh = new Subject<(int x,int y)>();

        var y = 0;
        foreach (var line in puzzle)
        {
            var x = 0;
            foreach (var c in line) {
                var e = int.Parse(c.ToString());

                octopuses.Add(new Octopus(flashesh) {
                    X = x,
                    Y = y,
                    Energy = e
                });

                x++;
            }
            y++;
        }


        int flashCount = 0;
        flashesh.Subscribe(s => flashCount++);

        var totalSteps = 0;

        for (int i = 0; i < Steps; i++)
        {
            totalSteps++;
            octopuses.ForEach(o => { o.Energy++; o.Flashed = false; });


            var flashes = new List<(int x,int y)>();
            octopuses.Where(o => o.Energy > 9)
                .ToList().ForEach(o =>
                {
                    flashes.Add(new(o.X, o.Y));
                    o.Flashed = true;
                    o.Energy = 0;
                });

            flashes.ForEach(f => flashesh.OnNext(f));
        }

        var p = new List<string>();
        p.Add(flashCount.ToString());
        p.AddRange(getPuzzle(octopuses));

        if (!Part2)
        {
            return p.ToArray();
        }

        while (octopuses.Any(o => o.Energy > 0))
        {
            totalSteps++;
            octopuses.ForEach(o => { o.Energy++; o.Flashed = false; });

            var flashes = new List<(int x,int y)>();
            octopuses.Where(o => o.Energy > 9)
                .ToList().ForEach(o =>
                {
                    flashes.Add(new(o.X, o.Y));
                    o.Flashed = true;
                    o.Energy = 0;
                });

            flashes.ForEach(f => flashesh.OnNext(f));
        }

        var r =  new string[] { p[0], totalSteps.ToString(),  };
        r.Concat(getPuzzle(octopuses));
        return r.ToArray();
	}

    public string[] getPuzzle(List<Octopus> octopi)
    {
        // octopi = octopi.OrderBy(o => o.Y).ThenBy(o => o.X).ToList();

        var ret = new List<string>();
        int line = -1;
        foreach(var o in octopi)
        {
            if (line != o.Y) {
                ret.Add("");
                line = o.Y;
            }
            ret[line] += o.Energy.ToString();
        }
        return ret.ToArray();
    }
}

public class Octopus
{
	private Subject<(int x, int y)> flashesh;

    public Octopus(Subject<(int x, int y)> flashesh)
	{
		this.flashesh = flashesh;

        flashesh.Subscribe(xy => {
            var ax = Math.Abs(X - xy.x);
            var ay = Math.Abs(Y - xy.y);
            if ((ax < 2 && ay == 1) 
            || (ay < 2 && ax == 1))
            {
                if (!Flashed)
                {
                    Energy++;
                }

                if (!Flashed && Energy > 9) {
                    Console.WriteLine($"{X}.{Y} Flashed");
                    Energy = 0;
                    Flashed = true;
                    flashesh.OnNext(new (X, Y));
                }
            }
        });
	}

	public int X { get; set; }
    public int Y { get; set; }
    public int Energy { get; set; }
    public bool Flashed { get; set; }
}
