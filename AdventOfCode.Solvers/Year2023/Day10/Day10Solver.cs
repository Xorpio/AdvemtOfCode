using System.Collections;
using System.Data;

namespace AdventOfCode.Solvers.Year2023.Day10;

public class Day10Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        var map = new Dictionary<Point, char>();
        for(var row = 0; row < puzzle.Length; row++)
        {
            for(var col = 0; col < puzzle[row].Length; col++)
            {
                map.Add(new Point(row, col), puzzle[row][col]);
            }
        }

        // | is a vertical pipe connecting north and south.
        // - is a horizontal pipe connecting east and west.
        // L is a 90-degree bend connecting north and east.
        // J is a 90-degree bend connecting north and west.
        // 7 is a 90-degree bend connecting south and west.
        // F is a 90-degree bend connecting south and east.

        var hasNorthConnection = new List<char> { 'S', 'L', 'J', '|' };
        var hasEastConnection = new List<char> { 'S', '-', 'L', 'F' };
        var hasSouthConnection = new List<char> { 'S', '|' , '7', 'F' };
        var hasWestConnection = new List<char> { 'S', '-', 'J', '7' };

        var start = map.First(c => c.Value == 'S').Key;
        Point next;
        if (map.ContainsKey(new Point(start.row + 1, start.col)) && hasNorthConnection.Contains(map[new Point(start.row + 1, start.col)]))
        {
            next = new Point(start.row + 1, start.col);
        }
        else if (map.ContainsKey(new Point(start.row, start.col + 1)) && hasWestConnection.Contains(map[new Point(start.row, start.col + 1)]))
        {
            next = new Point(start.row, start.col + 1);
        }
        else if (map.ContainsKey(new Point(start.row - 1, start.col)) && hasSouthConnection.Contains(map[new Point(start.row - 1, start.col)]))
        {
            next = new Point(start.row - 1, start.col);
        }
        else if (map.ContainsKey(new Point(start.row, start.col - 1)) && hasEastConnection.Contains(map[new Point(start.row, start.col - 1)]))
        {
            next = new Point(start.row, start.col - 1);
        }
        else
        {
            throw new Exception("No next point");
        }

        var loop = new Dictionary<Point, char>
        {
            { next, map[next] },
        };
        var previos = start;
        do
        {
            if (map[next] == '|' && next.row > previos.row) {
                previos = next;
                next = new Point(next.row + 1, next.col);
                loop.Add(next, map[next]);
            }
            else if (map[next] == '|' && next.row < previos.row) {
                previos = next;
                next = new Point(next.row - 1, next.col);
                loop.Add(next, map[next]);
            }
            else if (map[next] == '-' && next.col > previos.col) {
                previos = next;
                next = new Point(next.row, next.col + 1);
                loop.Add(next, map[next]);
            }
            else if (map[next] == '-' && next.col < previos.col) {
                previos = next;
                next = new Point(next.row, next.col - 1);
                loop.Add(next, map[next]);
            }
            else if (map[next] == 'L' && next.row > previos.row) {
                previos = next;
                next = new Point(next.row, next.col + 1);
                loop.Add(next, map[next]);
            }
            else if (map[next] == 'L' && next.col < previos.col) {
                previos = next;
                next = new Point(next.row - 1, next.col);
                loop.Add(next, map[next]);
            }
            else if (map[next] == 'J' && next.row > previos.row) {
                previos = next;
                next = new Point(next.row, next.col - 1);
                loop.Add(next, map[next]);
            }
            else if (map[next] == 'J' && next.col > previos.col) {
                previos = next;
                next = new Point(next.row - 1, next.col);
                loop.Add(next, map[next]);
            }
            else if (map[next] == '7' && next.row < previos.row) {
                previos = next;
                next = new Point(next.row, next.col - 1);
                loop.Add(next, map[next]);
            }
            else if (map[next] == '7' && next.col > previos.col) {
                previos = next;
                next = new Point(next.row + 1, next.col);
                loop.Add(next, map[next]);
            }
            else if (map[next] == 'F' && next.row < previos.row) {
                previos = next;
                next = new Point(next.row, next.col + 1);
                loop.Add(next, map[next]);
            }
            else if (map[next] == 'F' && next.col < previos.col) {
                previos = next;
                next = new Point(next.row + 1, next.col);
                loop.Add(next, map[next]);
            }
            else
                throw new Exception("Unknown direction");
        } while (map[next] != 'S');

        foreach(var (p, c) in map)
        {
            if (!loop.ContainsKey(p))
            {
                map[p] = '.';
            }
        }
        Print(map, 0);

        var newMap = new Dictionary<Point, char>();
        foreach(var (p, c) in map)
        {
            newMap.Add(p with { row = p.row * 2, col = p.col * 2 }, c);
        }
        foreach(var (p, c) in loop)
        {
            if (c == 'S') continue;
            if (hasNorthConnection.Contains(c))
            {
                newMap.TryAdd(p with { row = (p.row * 2) - 1, col = p.col * 2 }, '|');
            }
            if (hasEastConnection.Contains(c))
            {
                newMap.TryAdd(p with { row = p.row * 2, col = (p.col * 2) + 1 }, '-');
            }
            if (hasSouthConnection.Contains(c))
            {
                newMap.TryAdd(p with { row = (p.row * 2) + 1, col = p.col * 2 }, '|');
            }
            if (hasWestConnection.Contains(c))
            {
                newMap.TryAdd(p with { row = p.row * 2, col = (p.col * 2) - 1 }, '-');
            }
        }

        var points = map.Where(c => c.Value == '.').Select(c => c.Key).ToList();

        for(var row = 0; row < newMap.Max(c => c.Key.row); row++)
        {
            for(var col = 0; col < newMap.Max(c => c.Key.col); col++)
            {
                newMap.TryAdd(new Point(row, col), '.');
            }
        }

        var inside = new List<Point>();
        var checkedPoints = new List<Point>();

        Print(newMap, 1);
        var copy = new Dictionary<Point, char>();
        map.ToList().ForEach(c => copy.Add(c.Key, c.Value));

        foreach(var point in points)
        {
            if (checkedPoints.Contains(point)) continue;

            var canReachOutside = CheckPathToOutside(newMap, point with { row = point.row * 2, col = point.col * 2 });
            logger.OnNext($"Can reach outside: {canReachOutside.canReachOutside} from {point.row},{point.col}");

            var reducedPoints = canReachOutside.points.Where(p => p.row % 2 == 0 && p.col % 2 == 0).Select(p => p with { row = p.row / 2, col = p.col / 2 });


            foreach(var p in reducedPoints)
            {
                copy[p] = canReachOutside.canReachOutside ? 'O' : 'I';
                logger.OnNext($"can reach outside: {canReachOutside.canReachOutside} from {p.row},{p.col}");
            }

            if(!canReachOutside.canReachOutside)
            {
                inside.AddRange(reducedPoints);
            }

            checkedPoints.AddRange(reducedPoints);
        }
        Print(copy, 9);

        GiveAnswer1(Math.Floor(loop.Count() / 2.0));
        GiveAnswer2(inside.Count());
    }

    private (bool canReachOutside, IEnumerable<Point> points) CheckPathToOutside(Dictionary<Point, char> map, Point key)
    {
        //flood fill
        var points = new List<Point>();
        var queue = new Queue<Point>();
        queue.Enqueue(key);
        while(queue.Count > 0)
        {
            var point = queue.Dequeue();
            if (points.Contains(point)) continue;
            points.Add(point);

            if (map.ContainsKey(new Point(point.row + 1, point.col)) && map[new Point(point.row + 1, point.col)] == '.')
                queue.Enqueue(new Point(point.row + 1, point.col));
            if (map.ContainsKey(new Point(point.row - 1, point.col)) && map[new Point(point.row - 1, point.col)] == '.')
                queue.Enqueue(new Point(point.row - 1, point.col));
            if (map.ContainsKey(new Point(point.row, point.col + 1)) && map[new Point(point.row, point.col + 1)] == '.')
                queue.Enqueue(new Point(point.row, point.col + 1));
            if (map.ContainsKey(new Point(point.row, point.col - 1)) && map[new Point(point.row, point.col - 1)] == '.')
                queue.Enqueue(new Point(point.row, point.col - 1));
        }

        if (points.Any(p => p.row == 0 || p.col == 0 || p.row == map.Max(c => c.Key.row) || p.col == map.Max(c => c.Key.col)))
            return (true, points);
        
        return (false, points);
    }

    private void Print(Dictionary<Point, char> x, int step)
    {
        var maxRow = x.Max(c => c.Key.row);
        var maxCol = x.Max(c => c.Key.col);

        var line = "";
        logger.OnNext(line + $"Step {step}");

        for (int row = 0; row <= maxRow; row++)
        {
            for (int col = 0; col <= maxCol; col++)
            {
                if (x.ContainsKey(new Point(row, col)))
                {
                    line += x[new Point(row, col)];
                }
                else
                {
                    line += " ";
                }
            }
            logger.OnNext(line);
            line = "";
        }

        logger.OnNext("");
    }
}
public record Point(int row, int col);
