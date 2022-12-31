namespace PuzzleConsole.Year2022.Day23;

public class Day23 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
        var elfes = new List<Elf>();

        var part1 = "";

        for (var row = 0; row < puzzle.Length; row++)
        {
            var line = puzzle[row];
            for (var col = 0; col < line.Length; col++)
            {
                var c = line[col];
                if (c == '#')
                {
                    elfes.Add(new Elf(row, col));
                }
            }
        }

        var round = 0;
        var sw = new System.Diagnostics.Stopwatch();
        List<IGrouping<Elf, (Elf from, Elf to)>> moves;
        do
        {
            sw.Start();
            var propsedMoves = new List<(Elf from, Elf to)>();
            foreach (var elf in elfes)
            {
                Elf? tryMoveResult = tryMove(elfes, elf, round);
                if (tryMoveResult != null)
                {
                    propsedMoves.Add((elf, tryMoveResult));
                }
            }

            moves = propsedMoves
                .GroupBy(e => e.to)
                .ToList();
            moves.RemoveAll(g => g.Count() > 1);

            foreach (var move in moves.Select(m => m.First()))
            {
                elfes.Remove(move.from);
                elfes.Add(move.to);
            }

            round++;

            if (round == 10)
            {
                part1 = CountBlank(elfes).ToString();
                Console.WriteLine(part1);
            }

            Console.WriteLine($"round {round} in {sw.Elapsed} {moves.Count()} moved");
            sw.Reset();
        } while (moves.Any());

        return new string[]
        {
            part1,
            round.ToString()
        };
    }

    private static int CountBlank(List<Elf> elfes)
    {
        var countBlank = 0;
        var minRow = elfes.Select(e => e.Row).Min();
        var maxRow = elfes.Select(e => e.Row).Max();
        var minCol = elfes.Select(e => e.Col).Min();
        var maxCol = elfes.Select(e => e.Col).Max();

        for (var row = minRow; row <= maxRow; row++)
        {
            for (var col = minCol; col <= maxCol; col++)
            {
                if (!elfes.Contains(new Elf(row, col)))
                {
                    countBlank++;
                }
            }
        }

        return countBlank;
    }

    private Elf? tryMove(List<Elf> elfes, Elf elf, int round)
    {
        var n = elf with { Row = elf.Row - 1 };
        var ne = n with { Col = n.Col + 1 };
        var e = ne with { Row = ne.Row + 1 };
        var se = e with { Row = e.Row + 1 };
        var s = se with { Col = se.Col - 1 };
        var sw = se with { Col = s.Col - 1 };
        var w = sw with { Row = sw.Row - 1 };
        var nw = w with { Row = w.Row - 1 };

        if (new Elf[] { n, ne, e, se, s, sw, w, nw }.All(e => !elfes.Contains(e)))
            return null;

        for (int i = 0; i < 4; i++)
        {
            var dir = (Direction)((i + round) % 4);

            switch (dir)
            {
                case Direction.north:
                    if (new Elf[] { n, ne, nw }.All(e => !elfes.Contains(e)))
                        return n;
                    break;
                case Direction.south:
                    if (new Elf[] {s,se,sw}.All(e => !elfes.Contains(e)))
                        return s;
                    break;
                case Direction.west:
                    if (new Elf[] {w,nw,sw}.All(e => !elfes.Contains(e)))
                        return w;
                    break;
                case Direction.east:
                    if (new Elf[] {e,ne,se}.All(e => !elfes.Contains(e)))
                        return e;
                    break;
            }
        }

        return null;
    }
}

public record Elf(int Row, int Col);

public enum Direction
{
    north,
    south,
    west,
    east
}