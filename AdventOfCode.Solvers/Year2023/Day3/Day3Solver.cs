using System.Reactive.Subjects;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using AdventOfCode.Lib;

namespace AdventOfCode.Solvers.Year2023.Day3;

public class Day3Solver : BaseSolver
{
    List<Point> _symbols = new();
    Dictionary<Point, char> _map = new();
    char[] _numbers = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
    Dictionary<Point, char> _partNumbers = new();

    public override void Solve(string[] puzzle)
    {
        int row = 0;
        int col = 0;
        char[] numbersAndDot = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.'};
        foreach(var line in puzzle)
        {
            foreach (var c in line.Trim())
            {
                _map.Add(new Point(row, col), c);

                if (!numbersAndDot.Contains(c))
                {
                    _symbols.Add(new Point(row, col));
                }

                col++;
            }
            col = 0;
            row++;
        }

        List<(Point point, char num)> gearParts = new();

        var previous = new Point(int.MinValue, int.MinValue);
        int answer2 = 0;
        foreach(var symbol in _symbols)
        {
            var gearPart = 0;
            gearParts.Clear();
            var isGear = _map[symbol] == '*';
            for(int r = -1; r <=1; r++)
            {
                for(int c = -1; c <= 1; c++)
                {
                    if (r == 0 && c == 0)
                    {
                        continue;
                    }

                    var point = new Point(symbol.Row + r, symbol.Col + c);
                    var number = GetNumber(point);
                    if (number.HasValue)
                    {
                        if (!_partNumbers.ContainsKey(point))
                        {
                            _partNumbers.Add(point, number.Value);
                            var neighbors = FindNeighbours(point);
                            foreach(var (p, n) in neighbors)
                            {
                                _partNumbers.Add(p, n);
                            }

                            if (isGear)
                            {
                                gearPart++;
                                gearParts.Add((point, number.Value));
                                gearParts.AddRange(FindNeighbours(point));
                            }
                        }
                    }
                }
            }

            if (gearPart == 2)
            {
                previous = new Point(int.MinValue, int.MinValue);
                string gear1 = "";
                string gear2 = "";

                foreach(var (point, c) in gearParts.OrderBy(p => p.point.Row).ThenBy(p => p.point.Col))
                {
                    if (point.Row == previous.Row && point.Col == previous.Col + 1)
                        gear1 += $"{c}";
                    else
                    {
                        gear2 = gear1;
                        gear1 = "";
                        gear1 += $"{c}";
                    }

                    previous = point;
                }

                answer2 += int.Parse(gear1) * int.Parse(gear2);
            }
        }

        GiveAnswer2($"{answer2}");

        string num = "";
        var answers = new List<string>();
        previous = new Point(int.MinValue, int.MinValue);
        var partNumbers = _partNumbers.OrderBy(p => p.Key.Row).ThenBy(p => p.Key.Col);

        foreach(var (point, c) in partNumbers)
        {
            if (point.Row == previous.Row && point.Col == previous.Col + 1)
                num += $"{c}";
            else
            {
                logger.OnNext(num);
                answers.Add(num);
                num = "";
                num += $"{c}";
            }

            previous = point;
        }

                logger.OnNext(num);
        answers.Add(num);

        GiveAnswer1($"{answers.Where(a => !string.IsNullOrEmpty(a)).Select(a => int.Parse(a)).Sum()}");
    }

    private List<(Point point, char num)> FindNeighbours(Point symbol)
    {
        var list = new List<(Point point, char num)>();
        // find left
        char? number = null;
        var left = symbol with { };
        do
        {
            left = left with { Col = left.Col - 1 };
            number = GetNumber(left);
            if (number != null)
            {
                list.Add((left, number.Value));
            }
        } while (number != null);

        // find right
        number = null;
        var right = symbol with { };
        do
        {
            right = right with { Col = right.Col + 1 };
            number = GetNumber(right);
            if (number != null)
            {
                list.Add((right, number.Value));
            }
        } while (number != null);

        return list;
    }

    private char? GetNumber(Point point)
    {
        if (!_map.ContainsKey(point))
        {
            return null;
        }

        var c = _map[point];
        if (_numbers.Contains(c))
        {
            return c;
        }

        return null;
    }
}

public record Point(int Row, int Col);
