using System.ComponentModel;
using System.Security;
using Spectre.Console;

namespace PuzzleConsole.Year2022.Day22;

public class Day22 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
        var map = new Dictionary<Coord, char>();

        for (var row = 0; row < puzzle.Length; row++)
        {
            var line = puzzle[row];
            for (var col = 0; col < line.Length; col++)
            {
                var c = line[col];
                if (c == '.' || c == '#')
                {
                    map.Add(new Coord(row + 1,col + 1),c);
                }
            }
        }

        var instructions = puzzle[^1];

        var posPart1 = map
            .First().Key;
        var posPart2 = posPart1;

        var facingPart1 = Compass.Right;
        var facingPart2 = Compass.Right;
        //
        // var canvas = new Canvas(
        //     map.Keys.Select(k => k.col).Max() + 1,
        //     map.Keys.Select(k => k.row).Max() + 1
        // );
        //
        // foreach (var m in map)
        // {
        //     if (m.Value == '#')
        //         canvas.SetPixel(m.Key.col, m.Key.row, Color.NavyBlue);
        //     else
        //         canvas.SetPixel(m.Key.col, m.Key.row, Color.White);
        // }

        // AnsiConsole.Live(canvas)
        //     .Start(ctx => {
        var numBuff = "";
        foreach (char i in instructions)
        {
            switch (i)
            {
                case 'R':
                case 'L':
                    // pos = walk(int.Parse(numBuff), map, pos, facing, ctx, canvas);
                    (posPart1, facingPart1) = walk(int.Parse(numBuff), map, posPart1, facingPart1, true);
                    (posPart2, facingPart2) = walk(int.Parse(numBuff), map, posPart2, facingPart2, false);
                    facingPart1 = turn(i, facingPart1);
                    facingPart2 = turn(i, facingPart2);
                    numBuff = "";
                    break;
                default:
                    numBuff += i;
                    break;
            }
        }

        if (!string.IsNullOrEmpty(numBuff))
        {
            // pos = walk(int.Parse(numBuff), map, pos, facing, ctx, canvas);
            (posPart1, facingPart1) = walk(int.Parse(numBuff), map, posPart1, facingPart1, true);
            (posPart2, facingPart2) = walk(int.Parse(numBuff), map, posPart2, facingPart2, false);
        }
        // });

        var part1 = (posPart1.row * 1000) + (posPart1.col * 4) + (int)facingPart1;
        var part2 = (posPart2.row * 1000) + (posPart2.col * 4) + (int)facingPart2;

        var rline = "";
        for (int row = 1; row < map.Keys.Select(k => k.row).Max(); row++)
        {
            for (int col = 1; col < map.Keys.Select(k => k.col).Max(); col++)
            {
                var rc = new Coord(row, col);
                rline += (map.ContainsKey(rc))
                    ? map[rc]
                    : ' ';
            }

            Console.WriteLine(rline);
            rline = "";
        }

        return new[]
        {
            part1.ToString(),
            part2.ToString()
        };
    }

    private Compass turn(char direction, Compass facing) => (direction, facing) switch
    {
        ('R', Compass.Up) => Compass.Right,
        ('L', Compass.Right) => Compass.Up,
        ('R', _) => facing + 1,
        ('L', _) => facing - 1,
    };

    // private Coord walk(int steps, Dictionary<Coord, char> map, Coord pos, Compass facing, LiveDisplayContext ctx, Canvas canvas)
    private (Coord, Compass) walk(int steps, Dictionary<Coord, char> map, Coord pos, Compass facing, bool isPart1)
    {
        Compass newFacing = facing;
        while (steps > 0)
        {
            // canvas.SetPixel(pos.col, pos.row, Color.Green);
            var newPos = (facing) switch
                {
                    Compass.Down => pos with {row = pos.row + 1},
                    Compass.Right => pos with {col = pos.col + 1},
                    Compass.Up => pos with {row = pos.row - 1},
                    Compass.Left => pos with {col = pos.col - 1},
                }
                ;

            //wrap arround
            if (!map.ContainsKey(newPos))
            {
                if (isPart1)
                {
                    switch (facing)
                    {
                        case Compass.Right:
                            var rowi = map.Keys.Where(k => k.row == pos.row);
                            newPos = rowi.MinBy(r => r.col);
                            break;
                        case Compass.Left:
                            var rowm = map.Keys.Where(k => k.row == pos.row);
                            newPos = rowm.MaxBy(r => r.col);
                            break;
                        case Compass.Down:
                            var colm = map.Keys.Where(k => k.col == pos.col);
                            newPos = colm.MinBy(r => r.row);
                            break;
                        case Compass.Up:
                            var coli = map.Keys.Where(k => k.col == pos.col);
                            newPos = coli.MaxBy(r => r.row);
                            break;
                    }

                    newFacing = facing;
                }
                else
                {
                    (newPos, newFacing) = arroundEdgePart1(pos, facing, map);
                }
            }

            if (map[newPos] == '#')
            {
                steps = 0;
            }
            else
            {
                pos = newPos;
                facing = newFacing;
                steps--;
            }

            // canvas.SetPixel(pos.col, pos.row, Color.Blue);
            // ctx.Refresh();
            map[pos] = facing switch
            {
                Compass.Down => 'v',
                Compass.Up => '^',
                Compass.Left => '<',
                Compass.Right => '>',
            };
        }
        return (pos, facing);
    }

    private (Coord newPos, Compass newFacing) arroundEdgePart1(Coord pos, Compass facing, Dictionary<Coord, char> map)
    {
        Coord[] target = Array.Empty<Coord>();

        if (map.Count > 100)
        {
            //real input
            switch ((pos, facing))
            {
                //B1 > A1
                case (pos:{row: 1, col: < 101}, Compass.Up):
                    target = map.Keys.Where(k => k.row > 150 && k.col == 1)
                        .OrderBy(k => k.row)
                        .ToArray();
                    return (target[pos.col - 51], Compass.Up);

                //A2 > B2
                case (pos:{row: 200}, Compass.Down):
                    return (new Coord(1, pos.col + 100), facing);

                //A4 > B4
                case (pos:{ col: 150 }, _):
                    target = map.Keys.Where(k => k.row > 100 && k.col == 100)
                        .OrderByDescending(k => k.row)
                        .ToArray();
                    return (target[pos.row - 1], Compass.Left);

                //B5 > A5
                case (pos:{ col: 100, row: < 101 }, Compass.Right):
                    target = map.Keys.Where(k => k.row == 50 && k.col > 100)
                        .OrderBy(k => k.col)
                        .ToArray();
                    return (target[pos.row - 51], Compass.Up);


                default: throw new Exception($"{pos} - {facing} niet gedekt");
            }
        }

        switch ((pos, facing))
        {
            case (pos:{row: > 4, col: 12}, Compass.Right):
                target = map.Keys.Where(k => k.row == 9 && k.col > pos.col)
                    .OrderByDescending(k => k.col)
                    .ToArray();
                return (target[pos.row - 5], Compass.Down);
            case (pos:{row: 12, col: < 13}, Compass.Down):
                target = map.Keys.Where(k => k.row == 8 && k.col < 5)
                    .OrderByDescending(k => k.col)
                    .ToArray();
                return (target[pos.col - 9], Compass.Up);
            case (pos:{row: 5, col: > 4}, Compass.Up):
                target = map.Keys.Where(k => k.col == 9 && k.row < 5)
                    .OrderBy(k => k.row)
                    .ToArray();
                return (target[pos.col - 5], Compass.Right);
            default: throw new Exception($"{pos} - {facing} niet gedekt");
        }

        return (pos, facing);
    }
}

public record Coord(int row, int col);

public enum Compass
{
    Right,
    Down,
    Left,
    Up,
}