using Spectre.Console;

namespace PuzzleConsole.Year2022.Day14;

public class Day14 : ISolver
{
    private HashSet<Coord> _rock;
    private HashSet<Coord> _sand;
    private int _lowest;

    public string[] Solve(string[] puzzle)
    {
        _lowest = 500;
        _rock = new HashSet<Coord>();
        _sand = new HashSet<Coord>();

        //constuct maze
        foreach (var line in puzzle)
        {
            var lines = line.Split("->");
            var coords = lines.Select(c => c.Split(","))
                .Select(s => new Coord(int.Parse(s[0]), int.Parse(s[1])))
                .ToList();

            for (var index = 1; index < coords.Count; index++)
            {
                var fromCoord = coords[index -1];
                var toCoord = coords[index];

                var count = fromCoord - toCoord;

                if (count.y < 0)
                {
                    for (var i = count.y; i != 0; i++)
                    {
                        _rock.Add(fromCoord with { y = fromCoord.y - i});
                    }
                    _rock.Add(fromCoord);
                }

                if (count.y > 0)
                {
                    for (var i = 0; i < count.y; i++)
                    {
                        _rock.Add(fromCoord with { y = fromCoord.y - i});
                    }
                    _rock.Add(toCoord);
                }

                if (count.x < 0)
                {
                    for (var i = count.x; i != 0; i++)
                    {
                        _rock.Add(fromCoord with { x = fromCoord.x - i});
                    }
                    _rock.Add(fromCoord);
                }

                if (count.x > 0)
                {
                    for (var i = 0; i < count.x; i++)
                    {
                        _rock.Add(fromCoord with { x = fromCoord.x - i});
                    }
                    _rock.Add(toCoord);
                }
            }
        }

        _lowest = _rock.Select(r => r.y)
            .Max() + 1;

        Coord unit = null;
        do
        {
            unit = FallNewUnit();

            if (unit is not null)
            {
                _sand.Add(unit);
            }
        } while (unit is not null);


        //visualize Part1
        var minX = _rock.Select(r => r.x)
            .Min();
        var maxX = _rock.Select(r => r.x)
            .Max() + 1;
        var minY = _rock.Select(r => r.y)
            .Min();
        var maxY = _rock.Select(r => r.y)
            .Max() + 1;

        var canvas = new Canvas((maxX - minX) + 3, (maxY) + 3);

        for(var i = 0; i < canvas.Width; i++)
        {
            // Border
            canvas.SetPixel(i, 0, Color.Green);
            canvas.SetPixel(0, i, Color.Green);
            canvas.SetPixel(i, canvas.Height - 1, Color.Green);
            canvas.SetPixel(canvas.Width - 1, i, Color.Green);
        }

        foreach (var s in _sand)
        {
            Draw(s.x, s.y, Color.Yellow, canvas, minX, maxX, minY, maxY);
        }

        foreach (var r in _rock)
        {
            Draw(r.x, r.y, Color.White, canvas, minX, maxX, minY, maxY);
        }

        Draw(500, 0, Color.Red, canvas, minX, maxX, minY, maxY);

        AnsiConsole.Write(canvas);

        return new string[] { _sand.Count.ToString() };
    }

    public void Draw(int x, int y, Color color, Canvas canvas, int minX, int maxX, int minY, int maxY)
    {
        canvas.SetPixel(x - minX, y, color);
    }

    private Coord? FallNewUnit()
    {
        var unit = new Coord(500, 0);

        var next = true;
        while (next)
        {
            //check down
            if (!_rock.Contains(unit with { y = unit.y + 1 }) && !_sand.Contains(unit with { y = unit.y + 1 }))
            {
                unit = unit with { y = unit.y + 1 };
            }
            else if (!_rock.Contains(unit with { y = unit.y + 1, x = unit.x - 1 }) && !_sand.Contains(unit with { y = unit.y + 1, x = unit.x - 1 }))
            {
                unit = unit with { y = unit.y + 1 , x = unit.x - 1};
            }
            else if (!_rock.Contains(unit with { y = unit.y + 1, x = unit.x + 1 }) && !_sand.Contains(unit with { y = unit.y + 1, x = unit.x + 1 }))
            {
                unit = unit with { y = unit.y + 1 , x = unit.x + 1};
            }
            else
            {
                next = false;
            }

            if (unit.y > _lowest)
            {
                return null;
            }
        }

        return unit;
    }
}

public record Coord
{
    public static Coord operator -(Coord a, Coord b)
    {
        return a with
        {
            x = a.x - b.x,
            y = a.y - b.y
        };
    }
    public Coord(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public int x { get; init; }
    public int y { get; init; }

    public void Deconstruct(out int x, out int y)
    {
        x = this.x;
        y = this.y;
    }
}