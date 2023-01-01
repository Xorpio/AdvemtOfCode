using Spectre.Console;

namespace PuzzleConsole.Year2022.Day24;

public class Day24 : ISolver
{
    private int _maxCol;
    private int _maxRow;

    public string[] Solve(string[] puzzle)
    {
        var blizzards = new Dictionary<int, Blizzard>();
        var blizardCount = 0;

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

                    blizzards.Add(blizardCount, new Blizzard(new Coord(row - 1, col - 1), dir));
                    blizardCount++;
                }
            }
        }

        _maxCol = puzzle[0].Length - 2;
        _maxRow = puzzle.Length - 2;

        var elfs = new Queue<Coord>();
        elfs.Enqueue(new Coord(-1, 0));
        var running = true;
        var round = 0;
        int newCol = 0;
        int newRow = 0;

        Show(blizzards, elfs);

        do
        {
            var positions = new HashSet<Coord>();
            //move all blizzards
            foreach (var bliz in blizzards.Select(b => b.Value))
            {
                switch (bliz.Dir)
                {
                    case Direction.down:
                        newRow = bliz.Pos.Row == _maxRow - 1
                            ? 0
                            : bliz.Pos.Row + 1;
                        bliz.Pos = bliz.Pos with { Row = newRow };
                        break;
                    case Direction.up:
                        newRow = bliz.Pos.Row == 0
                            ? _maxRow - 1
                            : bliz.Pos.Row - 1;
                        bliz.Pos = bliz.Pos with { Row = newRow };
                        break;
                    case Direction.right:
                        newCol = bliz.Pos.Col == _maxCol - 1
                            ? 0
                            : bliz.Pos.Col + 1;
                        bliz.Pos = bliz.Pos with { Col = newCol };
                        break;
                    case Direction.left:
                        newCol = bliz.Pos.Col == 0
                            ? _maxCol - 1
                            : bliz.Pos.Col - 1;
                        bliz.Pos = bliz.Pos with { Col = newCol };
                        break;
                }
            }

            var blizPos = blizzards.Select(b => b.Value.Pos).Distinct();

            while (elfs.Any())
            {
                var elf = elfs.Dequeue();

                if (elf == new Coord(_maxRow - 1, _maxCol - 1))
                    running = false;

                //try all directions
                var directions = new Coord[]
                {
                    elf with { Row = elf.Row - 1 },
                    elf with { Row = elf.Row + 1 },
                    elf with { Col = elf.Col - 1 },
                    elf with { Col = elf.Col + 1 },
                    elf
                };

                var posible = directions.Where(e =>
                    (e.Row >= 0 && e.Row < _maxRow && e.Col >= 0 && e.Col < _maxCol)
                    || (e.Row == -1 && e.Col == 0)
                );
                posible = posible.Where(e => !blizPos.Contains(e));

                foreach (var p in posible)
                {
                    positions.Add(p);
                }
            }

            foreach (var pos in positions)
            {
                elfs.Enqueue(pos);
            }

            if (!elfs.Any() || elfs.Any(e => blizPos.Contains(e)))
            {
                throw new Exception("no solution found");
            }

            round++;

            // Console.WriteLine();
            Console.WriteLine($"Minute {round}");
            // Show(blizzards, elfs);

        } while (running);

        Show(blizzards, elfs);

        return new[]
        {
            round.ToString(),
        };
    }

    private void Show(Dictionary<int,Blizzard> storm, Queue<Coord> elfs)
    {
        // Console.SetCursorPosition(0,0);
        Console.WriteLine("".PadRight(_maxCol + 2, '#'));
        for (int row = 0; row < _maxRow; row++)
        {
            var tableRow = $"#";
            for (int col = 0; col < _maxCol; col++)
            {
                string cell = "";
                var blizs = storm
                    .Where(b => b.Value.Pos == new Coord(row, col));
                if (blizs.Count() > 0)
                {
                    cell = blizs.Count() > 1
                        ? blizs.Count().ToString()
                        : blizs.First().Value.Dir switch
                        {
                            Direction.down => "v",
                            Direction.left => "<",
                            Direction.right => ">",
                            Direction.up => "^",
                        };
                }
                else if (elfs.Contains(new Coord(row, col)))
                {
                    cell = "E";
                }
                else
                {
                    cell = " ";
                }

                tableRow += cell;
            }

            tableRow += "#";

            Console.WriteLine(tableRow);
        }
        Console.WriteLine("".PadRight(_maxCol + 2, '#'));
        Console.WriteLine();
    }
}
public record Coord(int Row, int Col);

public class Blizzard
{
    public Blizzard(Coord Pos, Direction Dir)
    {
        this.Pos = Pos;
        this.Dir = Dir;
    }

    public Coord Pos { get; set; }
    public Direction Dir { get; init; }

    public void Deconstruct(out Coord Pos, out Direction Dir)
    {
        Pos = this.Pos;
        Dir = this.Dir;
    }
}

public enum Direction
{
    up,down,left,right
}