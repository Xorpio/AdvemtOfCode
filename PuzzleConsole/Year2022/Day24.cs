using System.Reflection;
using Spectre.Console;

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

        // var elfs = new Queue<Coord>();
        // elfs.Enqueue(new Coord(-1, 0));
        // var running = true;
        // var round = 0;
        // int newCol = 0;
        // int newRow = 0;
        //
        // Show(blizzards, elfs);
        //
        // do
        // {
        //     var positions = new HashSet<Coord>();
        //     //move all blizzards
        //     foreach (var bliz in blizzards.Select(b => b.Value))
        //     {
        //         switch (bliz.Dir)
        //         {
        //             case Direction.down:
        //                 newRow = bliz.Pos.Row == _maxRow - 1
        //                     ? 0
        //                     : bliz.Pos.Row + 1;
        //                 bliz.Pos = bliz.Pos with { Row = newRow };
        //                 break;
        //             case Direction.up:
        //                 newRow = bliz.Pos.Row == 0
        //                     ? _maxRow - 1
        //                     : bliz.Pos.Row - 1;
        //                 bliz.Pos = bliz.Pos with { Row = newRow };
        //                 break;
        //             case Direction.right:
        //                 newCol = bliz.Pos.Col == _maxCol - 1
        //                     ? 0
        //                     : bliz.Pos.Col + 1;
        //                 bliz.Pos = bliz.Pos with { Col = newCol };
        //                 break;
        //             case Direction.left:
        //                 newCol = bliz.Pos.Col == 0
        //                     ? _maxCol - 1
        //                     : bliz.Pos.Col - 1;
        //                 bliz.Pos = bliz.Pos with { Col = newCol };
        //                 break;
        //         }
        //     }
        //
        //     var blizPos = blizzards.Select(b => b.Value.Pos).Distinct();
        //
        //     while (elfs.Any())
        //     {
        //         var elf = elfs.Dequeue();
        //
        //         if (elf == new Coord(_maxRow - 1, _maxCol - 1))
        //             running = false;
        //
        //         //try all directions
        //         var directions = new Coord[]
        //         {
        //             elf with { Row = elf.Row - 1 },
        //             elf with { Row = elf.Row + 1 },
        //             elf with { Col = elf.Col - 1 },
        //             elf with { Col = elf.Col + 1 },
        //             elf
        //         };
        //
        //         var posible = directions.Where(e =>
        //             (e.Row >= 0 && e.Row < _maxRow && e.Col >= 0 && e.Col < _maxCol)
        //             || (e.Row == -1 && e.Col == 0)
        //         );
        //         posible = posible.Where(e => !blizPos.Contains(e));
        //
        //         foreach (var p in posible)
        //         {
        //             positions.Add(p);
        //         }
        //     }
        //
        //     foreach (var pos in positions)
        //     {
        //         elfs.Enqueue(pos);
        //     }
        //
        //     if (!elfs.Any() || elfs.Any(e => blizPos.Contains(e)))
        //     {
        //         throw new Exception("no solution found");
        //     }
        //
        //     round++;
        //
        //     // Console.WriteLine();
        //     Console.WriteLine($"Minute {round}");
        //     // Show(blizzards, elfs);
        //
        // } while (running);
        //
        // Show(blizzards, elfs);
        //
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

    // private void Show(Dictionary<int,Blizzard> storm, Queue<Coord> elfs)
    // {
    //     // console.setcursorposition(0,0);
    //     console.writeline("".padright(_maxcol + 2, '#'));
    //     for (int row = 0; row < _maxRow; row++)
    //     {
    //         var tableRow = $"#";
    //         for (int col = 0; col < _maxCol; col++)
    //         {
    //             string cell = "";
    //             var blizs = storm
    //                 .Where(b => b.Value.Pos == new Coord(row, col));
    //             if (blizs.Count() > 0)
    //             {
    //                 cell = blizs.Count() > 1
    //                     ? blizs.Count().ToString()
    //                     : blizs.First().Value.Dir switch
    //                     {
    //                         Direction.down => "v",
    //                         Direction.left => "<",
    //                         Direction.right => ">",
    //                         Direction.up => "^",
    //                     };
    //             }
    //             else if (elfs.Contains(new Coord(row, col)))
    //             {
    //                 cell = "E";
    //             }
    //             else
    //             {
    //                 cell = " ";
    //             }
    //
    //             tableRow += cell;
    //         }
    //
    //         tableRow += "#";
    //
    //         Console.WriteLine(tableRow);
    //     }
    //     Console.WriteLine("".PadRight(_maxCol + 2, '#'));
    //     Console.WriteLine();
    // }
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