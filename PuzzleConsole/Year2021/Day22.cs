using Spectre.Console;

namespace PuzzleConsole.Year2021.Day22;

public class Day22 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
        var input = new Stack<(bool on, FromTo x, FromTo y, FromTo z)>(
            puzzle.Select(line => ParseLine(line))
            .Where(i => i.x.from.Between(-50, 50) &&
                i.x.to.Between(-50, 50) &&
                i.y.from.Between(-50, 50) &&
                i.y.to.Between(-50, 50) &&
                i.z.from.Between(-50, 50) &&
                i.z.to.Between(-50, 50))
            .Reverse()
        );

        var result = AppendAll(input);

        var answer = result.Select(c => ((c.x.to - c.x.from) + 1) * ((c.y.to - c.y.from) + 1) * ((c.z.to - c.z.from) + 1))
            .Sum();

        input = new Stack<(bool on, FromTo x, FromTo y, FromTo z)>(
            puzzle.Select(line => ParseLine(line))
            .Reverse()
        );
        result = AppendAll(input);
        var answer2 = result
            .Select(c => new { x =  (decimal)(c.x.to - c.x.from) + 1, y = (decimal)(c.y.to - c.y.from) + 1, z = (decimal)(c.z.to - c.z.from) + 1})
            .Select(c => c.x * c.y * c.z)
            .Sum();

        return new string[] { answer.ToString(), answer2.ToString() };
    }

    public List<Cube> AppendAll(Stack<(bool on, FromTo x, FromTo y, FromTo z)> input)
    {
        var cubeList = new List<Cube>();
        var SplitInput = new Stack<(bool @on, FromTo x, FromTo y, FromTo z)>();
        var current = input.Peek();
        AnsiConsole.Status()
            .Start("Working..,", ctx =>
            {
                while (input.Any() || SplitInput.Any())
                {
                    if (input.Any())
                    {
                        current = input.Peek();
                    }

                    var status = $"Lines: {input.Count}, Cubes: {cubeList.Count}, working on {current}";
                    ctx.Status(status);

                    var line = SplitInput.Any() ? SplitInput.Pop() : input.Pop();

                    if (line.on)
                    {
                        var cube = cubeList.FirstOrDefault(c =>
                            (
                                c.x.from.Between(line.x.from, line.x.to) ||
                                c.x.to.Between(line.x.from, line.x.to) ||
                                line.x.to.Between(c.x.from, c.x.to) ||
                                line.x.from.Between(c.x.from, c.x.to)
                            ) && (
                                c.y.from.Between(line.y.from, line.y.to) ||
                                c.y.to.Between(line.y.from, line.y.to) ||
                                line.y.to.Between(c.y.from, c.y.to) ||
                                line.y.from.Between(c.y.from, c.y.to)
                            ) && (
                                c.z.from.Between(line.z.from, line.z.to) ||
                                c.z.to.Between(line.z.from, line.z.to) ||
                                line.z.to.Between(c.z.from, c.z.to) ||
                                line.z.from.Between(c.z.from, c.z.to)
                            )
                        );

                        if (cube == null)
                        {
                            cubeList.Add(new Cube(line.x, line.y, line.z));
                        }
                        else
                        {
                            if (line.x.from < cube.x.from)
                            {
                                SplitInput.Push(new(true, new FromTo(line.x.from, cube.x.from - 1), line.y, line.z));
                                SplitInput.Push(new(true, new FromTo(cube.x.from, line.x.to), line.y, line.z));
                            }
                            else if (line.x.to > cube.x.to)
                            {
                                SplitInput.Push(new(true, new FromTo(line.x.from, cube.x.to), line.y, line.z));
                                SplitInput.Push(new(true, new FromTo(cube.x.to + 1, line.x.to), line.y, line.z));
                            }
                            else if (line.y.from < cube.y.from)
                            {
                                SplitInput.Push(new(true, line.x, new FromTo(line.y.from, cube.y.from - 1), line.z));
                                SplitInput.Push(new(true, line.x, new FromTo(cube.y.from, line.y.to), line.z));
                            }
                            else if (line.y.to > cube.y.to)
                            {
                                SplitInput.Push(new(true, line.x, new FromTo(line.y.from, cube.y.to), line.z));
                                SplitInput.Push(new(true, line.x, new FromTo(cube.y.to + 1, line.y.to), line.z));
                            }
                            else if (line.z.from < cube.z.from)
                            {
                                SplitInput.Push(new(true, line.x, line.y, new FromTo(line.z.from, cube.z.from - 1)));
                                SplitInput.Push(new(true, line.x, line.y, new FromTo(cube.z.from, line.z.to)));
                            }
                            else if (line.z.to > cube.z.to)
                            {
                                SplitInput.Push(new(true, line.x, line.y, new FromTo(line.z.from, cube.z.to)));
                                SplitInput.Push(new(true, line.x, line.y, new FromTo(cube.z.to + 1, line.z.to)));
                            }
                            else if (
                                line.x.from.Between(cube.x.from, cube.x.to) &&
                                line.x.to.Between(cube.x.from, cube.x.to) &&
                                line.y.from.Between(cube.y.from, cube.y.to) &&
                                line.y.to.Between(cube.y.from, cube.y.to) &&
                                line.z.from.Between(cube.z.from, cube.z.to) &&
                                line.z.to.Between(cube.z.from, cube.z.to)
                            )
                            {
                                //do nothing. cube is in other on cube
                            }
                            else
                                throw new Exception("mag niet komen");
                        }
                    }
                    else
                    {
                        var cubes = cubeList.Where(c =>
                            (
                                c.x.from.Between(line.x.from, line.x.to) ||
                                c.x.to.Between(line.x.from, line.x.to) ||
                                line.x.to.Between(c.x.from, c.x.to) ||
                                line.x.from.Between(c.x.from, c.x.to)
                            ) && (
                                c.y.from.Between(line.y.from, line.y.to) ||
                                c.y.to.Between(line.y.from, line.y.to) ||
                                line.y.to.Between(c.y.from, c.y.to) ||
                                line.y.from.Between(c.y.from, c.y.to)
                            ) && (
                                c.z.from.Between(line.z.from, line.z.to) ||
                                c.z.to.Between(line.z.from, line.z.to) ||
                                line.z.to.Between(c.z.from, c.z.to) ||
                                line.z.from.Between(c.z.from, c.z.to)
                            )
                        ).ToList();

                        foreach (var cube in cubes)
                        {
                            cubeList.Remove(cube);
                            cubeList.AddRange(RemoveCube(cube, line));
                        }
                    }
                }
            });
        return cubeList;

    }

    private string ToScad(Cube cube)
    {
        return ToScad((true, cube.x, cube.y, cube.z));
    }

    private string ToScad((bool @on, FromTo x, FromTo y, FromTo z) line)
    {
        var scad = $"translate ([{line.x.from}, {line.y.from} ,{line.z.from}])";
        scad += "{";
        scad += $"cube([{(line.x.to - line.x.from) + 1}, {(line.y.to - line.y.from) + 1}, {(line.z.to - line.z.from) + 1}]);";
        scad += "}";
        return scad;
    }

    public IEnumerable<Cube> RemoveCube(Cube cube, (bool on, FromTo x, FromTo y, FromTo z) line)
    {
        var cubes = SplitX(new[] { cube }, line.x.from);
        cubes = SplitX(cubes, line.x.to + 1);
        
        cubes = SplitY(cubes, line.y.from);
        cubes = SplitY(cubes, line.y.to + 1);
       
        cubes = SplitZ(cubes, line.z.from);
        cubes = SplitZ(cubes, line.z.to + 1);

        var ret = cubes.ToList();
        ret.RemoveAll(c =>
            (
            c.x.from.Between(line.x.from, line.x.to) ||
            c.x.to.Between(line.x.from, line.x.to) ||
            line.x.to.Between(c.x.from, c.x.to) ||
            line.x.from.Between(c.x.from, c.x.to)
            ) && (
            c.y.from.Between(line.y.from, line.y.to) ||
            c.y.to.Between(line.y.from, line.y.to) ||
            line.y.to.Between(c.y.from, c.y.to) ||
            line.y.from.Between(c.y.from, c.y.to)
            ) && (
            c.z.from.Between(line.z.from, line.z.to) ||
            c.z.to.Between(line.z.from, line.z.to) ||
            line.z.to.Between(c.z.from, c.z.to) ||
            line.z.from.Between(c.z.from, c.z.to)
            )
        );

        return ret;
    }

    public IEnumerable<Cube> SplitZ(IEnumerable<Cube> cubes, int z)
    {
        var splitCubes = new List<Cube>();

        foreach (var cube in cubes)
        {
            if (z == cube.z.from || !z.Between(cube.z.from, cube.z.to))
            {
                splitCubes.Add(cube);
            }
            else
            {
                splitCubes.Add(new Cube(cube.x, cube.y, new FromTo(cube.z.from, z - 1)));
                splitCubes.Add(new Cube(cube.x, cube.y, new FromTo(z, cube.z.to)));
            }
        }

        return splitCubes;
    }

    public IEnumerable<Cube> SplitY(IEnumerable<Cube> cubes, int y)
    {
        var splitCubes = new List<Cube>();

        foreach (var cube in cubes)
        {
            if (y == cube.y.from || !y.Between(cube.y.from, cube.y.to))
            {
                splitCubes.Add(cube);
            }
            else
            {
                splitCubes.Add(new Cube(cube.x, new FromTo(cube.y.from, y - 1), cube.z));
                splitCubes.Add(new Cube(cube.x, new FromTo(y, cube.y.to), cube.z));
            }
        }

        return splitCubes;
    }

    public IEnumerable<Cube> SplitX(IEnumerable<Cube> cubes, int x)
    {
        var splitCubes = new List<Cube>();

        foreach (var cube in cubes)
        {
            if (x == cube.x.from || !x.Between(cube.x.from, cube.x.to))
            {
                splitCubes.Add(cube);
            }
            else
            {
                splitCubes.Add(new Cube(new FromTo(cube.x.from, x - 1), cube.y, cube.z));
                splitCubes.Add(new Cube(new FromTo(x, cube.x.to), cube.y, cube.z));
            }
        }

        return splitCubes;
    }

    public (bool on, FromTo x, FromTo y, FromTo z) ParseLine(string line)
    {
        var s = line.Split(' ');
        var on = (s[0] == "on");
        var s2 = s[1].Split(',');

        var s3 = s2[0][2..].Split("..");
        var x = new FromTo(int.Parse(s3[0]), int.Parse(s3[1]));

        s3 = s2[1][2..].Split("..");
        var y = new FromTo(int.Parse(s3[0]), int.Parse(s3[1]));

        s3 = s2[2][2..].Split("..");
        var z = new FromTo(int.Parse(s3[0]), int.Parse(s3[1]));

        return (on, x, y, z);
    }
}

public record Cube (FromTo x, FromTo y, FromTo z);

public record FromTo (int from, int to);

public static class intExtension
{
    public static bool Between(this int item, int start, int end)
    {
        return item >= start && item <= end;
    }
}
