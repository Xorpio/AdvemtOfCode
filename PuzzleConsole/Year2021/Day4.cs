namespace PuzzleConsole.Year2021;

public class Day4 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
        var numbers = puzzle[0].Split(',');

        var boards = new List<Board>();

        int i = 2;
        while (i < puzzle.Length)
        {
            var lines = puzzle[i..(i + 5)];
            boards.Add(new Board(lines));
            i += 6;
        }

        string first = null;
        string last;
        foreach (var number in numbers)
        {
            boards.ForEach(b => b.Call(number));

            foreach (var board in boards)
            {
                if (string.IsNullOrEmpty(first) && board.Solved())
                {
                    var sum = board.Cols.SelectMany(c => c.Select(s => int.Parse(s))).Sum();
                    first = $"{sum * int.Parse(number)}";
                }
            }

            if (boards.Count() == 1 && boards.First().Solved())
            {
                var sum = boards.First().Cols.SelectMany(c => c.Select(s => int.Parse(s))).Sum();
                last = $"{sum * int.Parse(number)}";

                return new string[] { first, last };
            }

            boards.RemoveAll(b => b.Solved());
        }

        return null;
    }
}

public class Board
{
    public List<List<string>> Cols { get; private set; }
    public List<List<string>> Rows { get; private set; }

    public Board(string[] input)
    {
        Cols = new List<List<string>>();
        Rows = new List<List<string>>();

        for (int i = 0; i < 5; i++)
        {
            Cols.Add(new List<string>());
        }

        foreach (var l in input)
        {
            var line = l.Trim().Replace("  ", " ");
            var splittedLine = line.Split(' ');
            var row = new List<string>();
            row.AddRange(splittedLine);
            Rows.Add(row);

            for (int i = 0; i < splittedLine.Length; i++)
            {
                Cols[i].Add(splittedLine[i]);
            }
        }
    }

    public bool Solved()
    {
        return Rows.Any(r => !r.Any()) ||
               Cols.Any(c => !c.Any());
    }

    public void Call(string number)
    {
        Rows.ForEach(r => r.Remove(number));
        Cols.ForEach(c => c.Remove(number));
    }
}