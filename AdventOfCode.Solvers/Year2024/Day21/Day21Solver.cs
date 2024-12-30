using AdventOfCode.Solvers.Year2023.Day18;
using System.Data;
using System.Reflection;

namespace AdventOfCode.Solvers.Year2024.Day21;

public class Day21Solver : BaseSolver
{
    public Dictionary<char, (int row, int col)> numPad = new Dictionary<char, (int row, int col)>{
        { '7', (0, 0) },
        { '8', (0, 1) },
        { '9', (0, 2) },
        { '4', (1, 0) },
        { '5', (1, 1) },
        { '6', (1, 2) },
        { '1', (2, 0) },
        { '2', (2, 1) },
        { '3', (2, 2) },
        { '0', (3, 1) },
        { 'A', (3, 2) },
    };

    public Dictionary<char, (int row, int col)> dirPad = new Dictionary<char, (int row, int col)>
    {
        {'^', (0, 1) },
        {'A', (0, 2) },
        {'<', (1, 0) },
        {'v', (1, 1) },
        {'>', (1, 2) },
    };

    Dictionary<((int row, int col) start, (int row, int col) end, bool pad, string path), string[]> _pathCache = new();
    Dictionary<string, string> _movesCache = new();

    private Dictionary<(char from, char to), string[]> _numPaths = [];
    private Dictionary<(char from, char to), string[]> _dirPaths = [];
    public override void Solve(string[] puzzle)
    {
        foreach (var from in numPad.Keys)
        {
            foreach (var to in numPad.Keys)
            {
                _numPaths[(from, to)] = findPath2(numPad[from], numPad[to], false);
            }
        }
        foreach (var from in dirPad.Keys)
        {
            foreach (var to in dirPad.Keys)
            {
                _dirPaths[(from, to)] = findPath2(dirPad[from], dirPad[to], true);
            }
        }

        decimal score = 0;
        foreach (var line in puzzle)
        {
            var moves = calcMoves('A', line.ToCharArray(), 2, true);
            logger.OnNext($"{line}: {moves}");
            var num = decimal.Parse(line[..^1]);
            score += num * moves;
        }

        GiveAnswer1(score);
        score = 0;
        foreach (var line in puzzle)
        {
            var moves = calcMoves('A', line.ToCharArray(), 25, true);
            var num = decimal.Parse(line[..^1]);
            score += num * moves;
        }

        GiveAnswer2(score);
    }

    Dictionary<(char, string, int, bool), decimal> _cacheCalcMoves = [];
    private decimal calcMoves(char from, char[] pathleft, int robots, bool isNumPad)
    {
        if (_cacheCalcMoves.TryGetValue((from, new string(pathleft), robots, isNumPad), out var cached))
        {
            return cached;
        }

        if (pathleft.Length == 0)
        {
            return 0;
        }

        var cur = (isNumPad) ? numPad[from] : dirPad[from];
        var target = (isNumPad) ? numPad[pathleft[0]] : dirPad[pathleft[0]];

        var paths = isNumPad ? _numPaths[(from, pathleft[0])] : _dirPaths[(from, pathleft[0])];

        decimal min = decimal.MaxValue;
        if (robots == 0)
        {
            min = paths[0].Length;
        }
        else
        {
            foreach (var path in paths)
            {
                var moves = calcMoves('A', path.ToCharArray(), robots - 1, false);
                if (moves < min)
                {
                    min = moves;
                }
            }
        }

        var answer = min + calcMoves(pathleft[0], pathleft[1..], robots, isNumPad);
        _cacheCalcMoves[(from, new string(pathleft), robots, isNumPad)] = answer;
        return answer;
    }


    private string[] findPath2((int row, int col) start, (int row, int col) end, bool dirPad)
    {
        if (start == end)
        {
            return ["A"];
        }

        (int row, int col) m = (end.row - start.row, end.col - start.col);
        if (dirPad)
            return (m, start, end) switch
            {
                { m.row: > 0, m.col: 0 } => [new string('v', m.row) + "A"],
                { m.row: < 0, m.col: 0 } => [new string('^', Math.Abs(m.row)) + "A"],
                { m.row: 0, m.col: > 0 } => [new string('>', m.col) + "A"],
                { m.row: 0, m.col: < 0 } => [new string('<', Math.Abs(m.col)) + "A"],
                { start.row: 0, end.col: 0 } => [new string('v', Math.Abs(m.row)) + new string('<', Math.Abs(m.col)) + "A"],
                { start.col: 0, end.row: 0 } => [new string('>', Math.Abs(m.col)) + new string('^', Math.Abs(m.row)) + "A"],
                { m.col: < 0, m.row: < 0 } => [
                    new string('^', Math.Abs(m.row)) + new string('<', Math.Abs(m.col)) + "A",
                    new string('<', Math.Abs(m.col)) + new string('^', Math.Abs(m.row)) + "A"
                ],
                { m.col: > 0, m.row: < 0 } => [
                    new string('^', Math.Abs(m.row)) + new string('>', Math.Abs(m.col)) + "A",
                    new string('>', Math.Abs(m.col)) + new string('^', Math.Abs(m.row)) + "A"
                ],
                { m.col: < 0, m.row: > 0 } => [
                    new string('v', Math.Abs(m.row)) + new string('<', Math.Abs(m.col)) + "A",
                    new string('<', Math.Abs(m.col)) + new string('v', Math.Abs(m.row)) + "A"
                ],
                { m.col: > 0, m.row: > 0 } => [
                    new string('v', Math.Abs(m.row)) + new string('>', Math.Abs(m.col)) + "A",
                    new string('>', Math.Abs(m.col)) + new string('v', Math.Abs(m.row)) + "A"
                ],
                _ => throw new NotImplementedException(),
            };

        return (m, start, end) switch
        {
            { m.row: > 0, m.col: 0 } => [new string('v', m.row) + "A"],
            { m.row: < 0, m.col: 0 } => [new string('^', Math.Abs(m.row)) + "A"],
            { m.row: 0, m.col: > 0 } => [new string('>', m.col) + "A"],
            { m.row: 0, m.col: < 0 } => [new string('<', Math.Abs(m.col)) + "A"],
            { start.row: 3, end.col: 0 } => [new string('^', Math.Abs(m.row)) + new string('<', Math.Abs(m.col)) + "A"],
            { start.col: 0, end.row: 3 } => [new string('>', Math.Abs(m.col)) + new string('v', Math.Abs(m.row)) + "A"],
            { m.col: < 0, m.row: < 0 } => [
                new string('^', Math.Abs(m.row)) + new string('<', Math.Abs(m.col)) + "A",
                new string('<', Math.Abs(m.col)) + new string('^', Math.Abs(m.row)) + "A"
            ],
            { m.col: > 0, m.row: < 0 } => [
                new string('^', Math.Abs(m.row)) + new string('>', Math.Abs(m.col)) + "A",
                new string('>', Math.Abs(m.col)) + new string('^', Math.Abs(m.row)) + "A"
            ],
            { m.col: < 0, m.row: > 0 } => [
                new string('v', Math.Abs(m.row)) + new string('<', Math.Abs(m.col)) + "A",
                new string('<', Math.Abs(m.col)) + new string('v', Math.Abs(m.row)) + "A"
            ],
            { m.col: > 0, m.row: > 0 } => [
                new string('v', Math.Abs(m.row)) + new string('>', Math.Abs(m.col)) + "A",
                new string('>', Math.Abs(m.col)) + new string('v', Math.Abs(m.row)) + "A"
            ],
            _ => throw new NotImplementedException(),
        };
    }

}
