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

        foreach(var symbol in _symbols)
        {
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
                            FindNeighbours(point);
                        }
                    }
                }
            }
        }

        string num = "";
        var answers = new List<string>();
        var previous = new Point(int.MinValue, int.MinValue);

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

    private void FindNeighbours(Point symbol)
    {
        // find left
        char? number = null;
        var left = symbol with { };
        do
        {
            left = left with { Col = left.Col - 1 };
            number = GetNumber(left);
            if (number != null)
            {
                if (!_partNumbers.ContainsKey(left))
                {
                    _partNumbers.Add(left, number.Value);
                }
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
                if (!_partNumbers.ContainsKey(right))
                {
                    _partNumbers.Add(right, number.Value);
                }
            }
        } while (number != null);
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
