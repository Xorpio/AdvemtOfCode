using System.Reflection;

namespace PuzzleConsole.Year2022.Day24;

public class Day24 : ISolver
{
    private static bool? _runningFromNUnit = null;

    public static bool IsRunningFromNUnit
    {
        get
        {
            if (_runningFromNUnit is null)
            {
                _runningFromNUnit = false;
                foreach (Assembly assem in AppDomain.CurrentDomain.GetAssemblies())
                {
                    // Can't do something like this as it will load the nUnit assembly
                    // if (assem == typeof(NUnit.Framework.Assert))

                    if (assem.FullName.ToLowerInvariant().StartsWith("xunit.core"))
                    {
                        _runningFromNUnit = true;
                        break;
                    }
                }

            }

            return _runningFromNUnit.Value;
        }
    }

    private int _maxCol;
    private int _maxRow;
    private List<Blizzard> _blizzards;

    public string[] Solve(string[] puzzle)
    {
        _blizzards = new List<Blizzard>();

        for (var row = 1; row < puzzle.Length - 1; row++)
        {
            var line = puzzle[row];
            for (var col = 1; col < line.Length - 1; col++)
            {
                var spot = line[col];
                if (spot != '.')
                {
                    var dir = spot switch
                    {
                        '>' => Direction.right,
                        '<' => Direction.left,
                        '^' => Direction.up,
                        'v' => Direction.down,
                        _ => throw new Exception($"{spot} not supported"),
                    };

                    _blizzards.Add(new Blizzard(new Coord(row - 1, col - 1), dir));
                }
            }
        }

        _maxCol = puzzle[0].Length - 2;
        _maxRow = puzzle.Length - 2;

        var start = new Coord(-1, 0);
        var end = new Coord(_maxRow, _maxCol - 1);
        var pathCount = FindPath(start, end, 0);
        var part2 = pathCount;
        part2 = FindPath(end, start, part2);
        part2 = FindPath(start, end, part2);

        return new[]
        {
            pathCount.ToString(),
            part2.ToString(),
        };
    }

    private int FindPath(Coord from, Coord to, int round)
    {
        var found = true;

        var elfs = new List<(Coord Pos, int Round)>();
        elfs.Add((from, round));

        do
        {
            //select elf
            var elf = elfs
                .Select(e => (e, e.Round + (e.Pos + to)))
                .OrderBy(e => e.Item2)
                .Select(e => e.Item1)
                .First();
            elfs.Remove(elf);

            if (elf.Pos == to)
            {
                Console.WriteLine("Found path");
                return elf.Round - 1;
            }

            var storm = ProgressStorm(elf.Round);

            var directions = new Coord[]
            {
                elf.Pos with { Row = elf.Pos.Row - 1 },
                elf.Pos with { Row = elf.Pos.Row + 1 },
                elf.Pos with { Col = elf.Pos.Col - 1 },
                elf.Pos with { Col = elf.Pos.Col + 1 },
                elf.Pos
            };

            var posible = directions.Where(e =>
                (e.Row >= 0 && e.Row < _maxRow && e.Col >= 0 && e.Col < _maxCol)
                // can go back to start or end
                || (e.Row == -1 && e.Col == 0)
                || (e.Row == _maxRow && e.Col == _maxCol - 1)
            );

            elfs.AddRange(posible.Where(p => !storm.Contains(p))
                .Select(p => (p, elf.Round + 1))
            );
            elfs = elfs.Distinct().ToList();

            if (!IsRunningFromNUnit)
            {
                Console.SetCursorPosition(0,Console.GetCursorPosition().Top);
            }
            Console.Write($"{elfs.Count} - {elf.Round}".PadRight(10, ' '));

        } while (elfs.Any());

        throw new Exception("Path not found");
    }

    public Coord[] ProgressStorm(int round)
    {
        var storm = new HashSet<Coord>();
        foreach (var bliz in _blizzards)
        {
            storm.Add(bliz.Dir switch
            {
                Direction.down => bliz.Pos with { Row = (bliz.Pos.Row + round) % _maxRow },
                Direction.up => bliz.Pos with { Row = (((bliz.Pos.Row - round) % _maxRow) + _maxRow) % _maxRow },
                Direction.right => bliz.Pos with { Col = (bliz.Pos.Col + round) % _maxCol },
                Direction.left => bliz.Pos with { Col = (((bliz.Pos.Col - round) % _maxCol) + _maxCol) % _maxCol },
            });
        }

        return storm.ToArray();
    }
}
public record Coord(int Row, int Col)
{
    public static int operator +(Coord a, Coord b)
    {
        return Math.Abs((a.Row - b.Row)) +
               Math.Abs((a.Col - b.Col));
    }
}

public record Blizzard(Coord Pos, Direction Dir);

public enum Direction
{
    up,down,left,right
}