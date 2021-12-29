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
        var answer2 = result.Select(c => ((c.x.to - c.x.from) + 1) * ((c.y.to - c.y.from) + 1) * ((c.z.to - c.z.from) + 1))
            .Sum();

        return new string[] { answer.ToString(), answer2.ToString() };
    }

    private List<Cube> AppendAll(Stack<(bool on, FromTo x, FromTo y, FromTo z)> input)
    {
        var cubeList = new List<Cube>();
        while (input.Any())
        {
            var line = input.Pop();

            if (line.on)
            {
                var cube = cubeList.FirstOrDefault(c => 
                    (
                    c.x.from.Between(line.x.from, line.x.to) ||
                    c.x.to.Between(line.x.from, line.x.to) ||
                    line.x.to.Between(c.x.to, c.x.from) ||
                    line.x.from.Between(c.x.to, c.x.from)
                    ) &&(
                    c.y.from.Between(line.y.from, line.y.to) ||
                    c.y.to.Between(line.y.from, line.y.to) ||
                    line.y.to.Between(c.y.to, c.y.from) ||
                    line.y.from.Between(c.y.to, c.y.from)
                    ) &&(
                    c.z.from.Between(line.z.from, line.z.to) ||
                    c.z.to.Between(line.z.from, line.z.to) ||
                    line.z.to.Between(c.z.to, c.z.from) ||
                    line.z.from.Between(c.z.to, c.z.from)
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
                        input.Push(new(true, new FromTo(line.x.from, cube.x.from - 1), line.y, line.z));
                        input.Push(new(true, new FromTo(cube.x.from, line.x.to), line.y, line.z));
                    }
                    else if (line.x.to > cube.x.to)
                    {
                        input.Push(new(true, new FromTo(line.x.from, cube.x.to), line.y, line.z));
                        input.Push(new(true, new FromTo(cube.x.to + 1, line.x.to), line.y, line.z));
                    }
                    else if (line.y.from < cube.y.from)
                    {
                        input.Push(new(true, line.x, new FromTo(line.y.from, cube.y.from - 1), line.z));
                        input.Push(new(true, line.x, new FromTo(cube.y.from, line.y.to), line.z));
                    }
                    else if (line.y.to > cube.y.to)
                    {
                        input.Push(new(true, line.x, new FromTo(line.y.from, cube.y.to), line.z));
                        input.Push(new(true, line.x, new FromTo(cube.y.to + 1, line.y.to), line.z));
                    }
                    else if (line.z.from < cube.z.from)
                    {
                        input.Push(new(true, line.x, line.y, new FromTo(line.z.from, cube.z.from - 1)));
                        input.Push(new(true, line.x, line.y, new FromTo(cube.z.from, line.z.to)));
                    }
                    else if (line.z.to > cube.z.to)
                    {
                        input.Push(new(true, line.x, line.y, new FromTo(line.z.from, cube.z.to)));
                        input.Push(new(true, line.x, line.y, new FromTo(cube.z.to + 1, line.z.to)));
                    }
                    else if (
                        line.x.from.Between(cube.x.from, cube.x.to) &&
                        line.x.to.Between(cube.x.from, cube.x.to) &&
                        line.y.from.Between(cube.y.from, cube.y.to) &&
                        line.y.to.Between(cube.y.from, cube.y.to) &&
                        line.z.from.Between(cube.z.from, cube.z.to) &&
                        line.z.to.Between(cube.z.from, cube.z.to)
                    ) {
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
                    line.x.to.Between(c.x.to, c.x.from) ||
                    line.x.from.Between(c.x.to, c.x.from)
                    ) && (
                    c.y.from.Between(line.y.from, line.y.to) ||
                    c.y.to.Between(line.y.from, line.y.to) ||
                    line.y.to.Between(c.y.to, c.y.from) ||
                    line.y.from.Between(c.y.to, c.y.from)
                    ) && (
                    c.z.from.Between(line.z.from, line.z.to) ||
                    c.z.to.Between(line.z.from, line.z.to) ||
                    line.z.to.Between(c.z.to, c.z.from) ||
                    line.z.from.Between(c.z.to, c.z.from)
                    )
                ).ToList();

                foreach (var cube in cubes)
                {
                    cubeList.Remove(cube);
                    cubeList.AddRange(RemoveCube(cube, line));
                }
            }
        }

        return cubeList;
    }

    private IEnumerable<Cube> RemoveCube(Cube cube, (bool on, FromTo x, FromTo y, FromTo z) line)
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
            line.x.to.Between(c.x.to, c.x.from) ||
            line.x.from.Between(c.x.to, c.x.from)
            ) && (
            c.y.from.Between(line.y.from, line.y.to) ||
            c.y.to.Between(line.y.from, line.y.to) ||
            line.y.to.Between(c.y.to, c.y.from) ||
            line.y.from.Between(c.y.to, c.y.from)
            ) && (
            c.z.from.Between(line.z.from, line.z.to) ||
            c.z.to.Between(line.z.from, line.z.to) ||
            line.z.to.Between(c.z.to, c.z.from) ||
            line.z.from.Between(c.z.to, c.z.from)
            )
        );

        return ret;
    }

    private IEnumerable<Cube> SplitZ(IEnumerable<Cube> cubes, int z)
    {
        var splitCubes = new List<Cube>();

        foreach (var cube in cubes)
        {
            if (!z.Between(cube.z.from, cube.z.to))
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

    private IEnumerable<Cube> SplitY(IEnumerable<Cube> cubes, int y)
    {
        var splitCubes = new List<Cube>();

        foreach (var cube in cubes)
        {
            if (!y.Between(cube.y.from, cube.y.to))
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

    private IEnumerable<Cube> SplitX(IEnumerable<Cube> cubes, int x)
    {
        var splitCubes = new List<Cube>();

        foreach (var cube in cubes)
        {
            if (!x.Between(cube.x.from, cube.x.to))
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

    private (bool on, FromTo x, FromTo y, FromTo z) ParseLine(string line)
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
