using System.Text.RegularExpressions;

namespace PuzzleConsole.Year2015.Day1;

public class Day1 : ISolver
{
    private const decimal Start = 20151125;
    private const decimal Multiply = 252533;
    private const decimal Modulo = 33554393;

    private int _row = 0;
    private int _col = 0;

    private IList<IList<decimal>> _grid;
    public string[] Solve(string[] puzzle)
    {
        string pattern = @"\d+,\d+";
        Regex puzzleInputRegex = new Regex(pattern);
        if (puzzle == null || puzzle.Length < 1 || string.IsNullOrWhiteSpace(puzzle[0]) || !puzzleInputRegex.IsMatch(puzzle[0]))
        {
            throw new InvalidParameterException("Puzzle input not valid. Expected '(decimal),(decimal)'");
        }

        _grid = new List<IList<decimal>>();
        _grid.Add(new List<decimal>()
        {
           Start
        });

        var row = int.Parse(puzzle[0].Split(',')[0]) - 1;
        var column = int.Parse(puzzle[0].Split(',')[1]) - 1;

        while (_grid.ElementAtOrDefault(row)?.ElementAtOrDefault(column) is null)
        {
            addToGrid();
        }

        return new string[] { _grid[_row][_col].ToString() };
    }

    private void addToGrid()
    {
        var cur = _grid[_row][_col];
        var next = (cur * Multiply) % Modulo;

        var newRow = _row - 1;
        var newCol = _col + 1;

        if (newRow < 0)
        {
            newRow = _grid.Count;
            newCol = 0;
        }

        _row = newRow;
        _col = newCol;

        if (_grid.ElementAtOrDefault(_row) is null)
        {
            _grid.Add(new List<decimal>());
        }
        _grid[_row].Add(next);
    }
}
public class InvalidParameterException : Exception
{
    public InvalidParameterException(string message) : base(message)
    {
    }
}
