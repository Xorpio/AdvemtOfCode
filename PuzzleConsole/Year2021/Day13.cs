namespace PuzzleConsole.Year2021.Day13;

public class Day13 : ISolver
{
    public int Debug { get; set; }
    public string[] Solve(string[] puzzle)
    {
        var splittedPuzzle = splitPuzzle(puzzle);

        var paper = ToPaper(splittedPuzzle.paper);

        int numberOfDots = CountDots(paper);

        paper = fold(paper, splittedPuzzle.foldInstructions.First());

        int numberOfDots2 = CountDots(paper);

        foreach (var f in splittedPuzzle.foldInstructions[1..])
        {
            paper = fold(paper, f);
        }

        var ret = new string[] { $"Before fold: {numberOfDots}", $"After first fold: {numberOfDots2}" };
        ret = ret.Concat(paperToString(paper)).ToArray();
        return ret;
    }

    public List<List<bool>> ToPaper(string[] splittedPuzzle)
    {
        var xylist = splittedPuzzle.Select(s => s.Split(','))
            .Select(sa => sa.Select(s => int.Parse(s)).ToArray())
            .Select(ia => new XY { X = ia[0], Y = ia[1] }).ToList();

        var maxX = xylist.MaxBy(xy => xy.X).X + 1;
        var maxY = xylist.MaxBy(xy => xy.Y).Y + 1;

        var paper = new List<List<bool>>();
        for (int y = 0; y < maxY; y++)
        {
            paper.Add(new List<bool>());
            for(int x = 0; x < maxX; x++)
            {
                paper[y].Add(false);
            }
        }

        xylist.ForEach(xy => paper[xy.Y][xy.X] = true);

        return paper;
    }

    public string[] paperToString(List<List<bool>> paper)
    {
        return paper.Select(line => line.Select(l => l ? "#" : "."))
            .Select(la => la.Aggregate((acc, cum) => acc + cum))
            .ToArray();
    }

    public List<List<bool>> fold(List<List<bool>> paper, string instruction)
    {
        var foldInstructions = DeterminFold(instruction);

        var newPaper = new List<List<bool>>();
        if (foldInstructions.isX)
        {
            for (int y = 0; y < paper.Count; y++)
            {
                var newX = foldInstructions.line - 1;
                
                newPaper.Add(new List<bool>());

                for(int x = 0; x < paper[y].Count(); x++)
                {
                    if (x == foldInstructions.line)
                        continue;

                    if (newX >= x)
                    {
                        newPaper[y].Add(paper[y][x]);
                    }
                    else
                    {
                        newPaper[y][newX] = newPaper[y][newX] || paper[y][x];
                        newX--;
                    }
                }
            }
        }
        else
        {
            var newY = foldInstructions.line - 1;
            for (int y = 0; y < paper.Count && newY >= 0; y++)
            {
                if (y == foldInstructions.line)
                    continue;
                
                if (newY >= y)
                    newPaper.Add(new List<bool>());
                for(int x = 0; x < paper[y].Count(); x++)
                {
                    if (newY >= y)
                    {
                        newPaper[y].Add(paper[y][x]);
                    }
                    else
                    {
                        newPaper[newY][x] = newPaper[newY][x] || paper[y][x];
                    }
                }
                if (newY < y)
                    newY--;
            }
        }

        return newPaper;
    }

    public (bool isX, int line) DeterminFold(string insctruction)
    {
        var ins = insctruction[11..].Split('=');
        return new(ins[0] == "x", int.Parse(ins[1]));
    }

    private int CountDots(List<List<bool>> paper)
    {
        return paper.Select(l => l.Where(b => b).Count()).Sum();
    }

    public (string[] paper, string[] foldInstructions) splitPuzzle(string[] puzzle)
    {
        var paperInput = puzzle.TakeWhile(s => !string.IsNullOrEmpty(s)).ToArray();
        
        var emptyLine = puzzle.ToList().IndexOf("") + 1;

        var foldInstructions = puzzle[emptyLine..];
        return new (paperInput, foldInstructions);
    }
}

public record XY
{
    public int X { get; set; }
    public int Y { get; set; }
}
