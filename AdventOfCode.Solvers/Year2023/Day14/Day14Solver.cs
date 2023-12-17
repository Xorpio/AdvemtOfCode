
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace AdventOfCode.Solvers.Year2023.Day14;

public class Day14Solver : BaseSolver
{
    List<(Point p, char c)> _map = [];

    public override void Solve(string[] puzzle)
    {
        int maxRow = puzzle.Length;

        for (int row = 0; row < puzzle.Length; row++)
        {
            for (int col = 0; col < puzzle[row].Length; col++)
            {
                if (puzzle[row][col] == '.')
                    continue;

                _map.Add((new Point(row, col), puzzle[row][col]));
            }
        }

        printmap();

        TiltNorth();

        printmap();

        var answer1 = _map.Where(kv => kv.c == 'O')
            .Select(kvp => maxRow - kvp.p.row).Sum();

        GiveAnswer1(answer1);
        GiveAnswer2("");
    }

    private void printmap()
    {
        var maxRow = _map.Max(kvp => kvp.p.row);
        var maxCol = _map.Max(kvp => kvp.p.col);

        for(int row = 0; row <= maxRow; row++)
        {
            var line = "";
            for(int col = 0; col <= maxCol; col++)
            {
                var c = _map.FirstOrDefault(kvp => kvp.p.row == row && kvp.p.col == col);
                if (c == default)
                {
                    line += ".";
                }
                else
                {
                    line += c.c;
                }
            }
            logger.OnNext(line);
        }

        logger.OnNext("");
    }

    private void TiltNorth()
    {
        var mapCopy = _map.Select(kvp => kvp).ToList().OrderBy(kvp => kvp.p.row).ThenBy(kvp => kvp.p.col).ToArray();
        var length = mapCopy.Length;

        for (int i = 0; i < mapCopy.Length; i++)
        {
            var (p, c) = mapCopy[i];

            if (c == '#')
                continue;

            var newP = new Point(p.row, p.col);

            do
            {
                if (mapCopy.Any(kvp => kvp.p == newP with {row= newP.row - 1}) || newP.row == 0)
                {
                    break;
                }
                newP = new Point(newP.row - 1, newP.col);
            } while(true);

            mapCopy[i].p = newP;
        }

        _map = mapCopy.ToList();
    }
}

public record Point(int row, int col);
