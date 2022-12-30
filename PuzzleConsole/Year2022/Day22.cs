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

        var pos = map
            .First().Key;

        var facing = Compass.Right;
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
                    pos = walk(int.Parse(numBuff), map, pos, facing);
                    facing = turn(i, facing);
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
            pos = walk(int.Parse(numBuff), map, pos, facing);
        }
        // });

        var part1 = (pos.row * 1000) + (pos.col * 4) + (int)facing;

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
            part1.ToString()
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
    private Coord walk(int steps, Dictionary<Coord, char> map, Coord pos, Compass facing)
    {
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
            }

            if (map[newPos] == '#')
            {
                steps = 0;
            }
            else
            {
                pos = newPos;
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
        return pos;
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