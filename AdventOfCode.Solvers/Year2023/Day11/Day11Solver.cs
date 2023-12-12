
namespace AdventOfCode.Solvers.Year2023.Day11;

public class Day11Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        var maxRow = puzzle.Length;
        var maxCol = puzzle[0].Length;

        bool[] nonEmptyRows = new bool[maxRow];
        bool[] nonEmptyCols = new bool[maxCol];

        var map = new Dictionary<Point, (Point p, char c)>();
        var galaxy = new List<Point>();

        for(var row = 0; row < maxRow; row++)
        {
            for(var col = 0; col < maxCol; col++)
            {
                var c = puzzle[row][col];
                var p = new Point(row, col);
                map[p] = (p, c);

                if(c == '#')
                {
                    //contains galaxy
                    galaxy.Add(p);
                    nonEmptyCols[col] = true;
                    nonEmptyRows[row] = true;
                }
            }
        }

        var rowsExpanse = new double[maxRow];
        var colsExpanse = new double[maxCol];
        double count = 0;
        double expansion = 1;
        for(var row = 0; row < maxRow; row++)
        {
            if(!nonEmptyRows[row])
            {
                count += expansion;
            }
            rowsExpanse[row] += count;
        }
        count = 0;
        for(var col = 0; col < maxCol; col++)
        {
            if(!nonEmptyCols[col])
            {
                count += expansion;
            }
            colsExpanse[col] += count;
        }

        var newgalaxy = galaxy.Select(p => p with { Row = p.Row + rowsExpanse[(int)p.Row], Col = p.Col + colsExpanse[(int)p.Col] }).ToList();

        double answer = 0;
    count = 0;
        for(int i = 0; i < newgalaxy.Count(); i++)
        {
            for(int j = i + 1; j < newgalaxy.Count(); j++)
            {
                answer += newgalaxy[i].AddPoint(newgalaxy[j]);
            }
        }

        GiveAnswer1(answer);

        rowsExpanse = new double[maxRow];
        colsExpanse = new double[maxCol];
        count = 0;
        expansion = (1_000_000) - 1;
        for(var row = 0; row < maxRow; row++)
        {
            if(!nonEmptyRows[row])
            {
                count += expansion;
            }
            rowsExpanse[row] += count;
        }
        count = 0;
        for(var col = 0; col < maxCol; col++)
        {
            if(!nonEmptyCols[col])
            {
                count += expansion;
            }
            colsExpanse[col] += count;
        }

        newgalaxy = galaxy.Select(p => p with { Row = p.Row + rowsExpanse[(int)p.Row], Col = p.Col + colsExpanse[(int)p.Col] }).ToList();

        answer = 0;
        for(int i = 0; i < newgalaxy.Count(); i++)
        {
            for(int j = i + 1; j < newgalaxy.Count(); j++)
            {
                answer += newgalaxy[i].AddPoint(newgalaxy[j]);
            }
        }
        GiveAnswer2(answer);
    }

    private void Print(List<Point> map)
    {
        var maxRow = map.Max(c => c.Row);
        var maxCol = map.Max(c => c.Col);

        var line = "";
        for (int row = 0; row <= maxRow; row++)
        {
            for (int col = 0; col <= maxCol; col++)
            {
                if (map.Contains(new Point(row, col)))
                {
                    line += '#';
                }
                else
                {
                    line += ".";
                }
            }
            logger.OnNext(line);
            line = "";
        }

        logger.OnNext("");
    }
}

public record Point (double Row, double Col)
{
    public double AddPoint(Point b)
    {
        return Math.Abs(Row - b.Row) + Math.Abs(Col - b.Col);
    }
}
